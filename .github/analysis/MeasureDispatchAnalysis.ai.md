# Measure Dispatch Performance Analysis

**Entry point**: `left.Value * right.Value` in `MechanicalEngineering.cs`

## Full Call Graph

```
MechanicalEngineering: left.Value * right.Value
│
├─ left.Value / right.Value                         ← field access (~0 ns)
│
├─ Quantity.operator*(Quantity, Quantity)             ← entry
│   │
│   ├─ [2a] left.measure * right.measure             ← Measure.operator*
│   │   │
│   │   ├─ VIRTUAL CALL 1: Impl<L>.Multiply(other)   (~3-5 ns)
│   │   │   └─ return other.Multiply<L>()
│   │   │
│   │   └─ VIRTUAL CALL 2: Impl<R>.Multiply<L>()     (~10-15 ns) ◀ GENERIC VIRTUAL
│   │       └─ return ref Multiplication<L, R>.Result  ← static cache hit (~0 ns)
│   │
│   ├─ [2b] product * left.value                      ← Result.operator*(Result, Double)
│   │   └─ Polynomial.operator*(Poly, Double)          ← FMA + mul + div (~0.09 ns)
│   │       FusedMultiplyAdd(nom, val, den*offset)/den
│   │       Identity case: FMA(1, val, 0) / 1 = val    ← still pays div-by-1
│   │
│   ├─ [2c] (result) * right.value                    ← Double * Double (~0 ns)
│   │
│   └─ [2d] new Quantity(double, product→Measure)     ← struct init (~0 ns)
│
└─ Create<Area>(Quantity)                             ← static interface call (~0 ns)
    └─ Area.Create(in value) → new Area(in value)
```

## Calibration Against Existing Benchmarks

| Benchmark | What it measures | Time |
|---|---|---|
| `ProjectTrivial` | Struct-to-struct conversion, no virtual dispatch | **0.11 ns** |
| `ProjectOntoSame` | Double virtual dispatch + polynomial eval | **4.89 ns** |
| `EvaluateArithmetically` | Single `Polynomial * Double` | **0.09 ns** |

Double virtual dispatch ≈ 4.8 ns, polynomial eval ≈ 0.09 ns. The dispatch dominates by **~50×**.

## The Bottleneck: Generic Virtual Method Dispatch

The single most expensive instruction in the entire chain is the **generic virtual call** at `Measure.cs` line 58:

```csharp
protected override ref readonly Result Multiply<TOtherMeasure>() 
    => ref Multiplication<TOtherMeasure, TMeasure>.Result;
```

This call is expensive for a specific reason: the JIT cannot devirtualize generic virtual methods. Unlike regular virtual calls (which .NET 8+ can speculatively devirtualize via PGO/GDV), **generic virtual methods require a runtime generic dictionary lookup** to resolve the type argument `TOtherMeasure`. The JIT must:

1. Load the method table pointer
2. Look up the generic dictionary for `Multiply<T>` on `Impl<R>`
3. Resolve the specific instantiation for `T = TOtherMeasure`
4. Call through the resolved slot

All of this exists solely to reach a **static readonly field** that's already been computed. The cached `Result` is right there — the entire cost is the dispatch to find it.

## Three Experiments, Ordered by Impact

### Experiment 1: Per-instance result cache (avoids virtual dispatch entirely)

Since `Measure` instances are singletons (via `AllocationFree<>`), cache the `Result` directly on each instance keyed by the other measure's reference. This replaces two virtual calls with a dictionary lookup using reference-equality:

```csharp
// In Measure:
private readonly Dictionary<Measure, Result> products = new(ReferenceEqualityComparer.Instance);

public static Result operator *(Measure left, Measure right)
    => left.products.TryGetValue(right, out var cached)
        ? cached
        : left.CacheMultiply(right);

private Result CacheMultiply(Measure right)
{
    var result = this.Multiply(right);  // double virtual dispatch, once
    this.products[right] = result;
    return result;
}
```

A `Dictionary` lookup with `ReferenceEqualityComparer` is just a pointer hash + pointer equality check, which should be **~1-2 ns** — potentially a **~3 ns savings** per operation.

### Experiment 2: Identity fast-path in `Polynomial.operator*`

For the identity case (both operands in SI base), the polynomial is `Polynomial.One` but FMA + division still execute:

```csharp
// In Polynomial:
public static Double operator *(Polynomial left, Double right)
{
    // When nom == den and offset == 0, the polynomial is identity
    if (left.offset == 0d && left.nominator == left.denominator) return right;
    return Double.FusedMultiplyAdd(left.nominator, right, left.denominator * left.offset) / left.denominator;
}
```

This saves the FMA + div (~0.09 ns) for the common identity case. The branch will be well-predicted since it's always the same path for a given measure pair. Small win, but essentially free to try.

### Experiment 3: `ReferenceEquals` fast-path for same-measure `Quantity` multiplication

In `Quantity.cs` line 85, `operator*` always dispatches. For the common case where both operands have the same measure (e.g., two `Length` values both in metres), a `ReferenceEquals` check could skip directly to a self-multiply cache:

```csharp
public static Quantity operator *(Quantity left, Quantity right)
{
    Result product = left.measure * right.measure; // could fast-path if same ref
    return new(product * left.value * right.value, product);
}
```

This is essentially Experiment 1 at the `Quantity` level. The challenge: even for same-measure, the *result* measure differs from the input measure (e.g., `m × m → m²`), so you still need the `Result`. But Experiment 1's per-instance cache would handle this naturally — the dictionary on the metre `Measure` would cache the entry for `metre → Result(m²)`.

## What Should NOT Change

- **`Create<Area>`**: Already a static interface call, fully inlineable by JIT — zero overhead.
- **`Multiplication<,>.Result` static cache**: Already excellent. The initialization cost is amortised to zero.
- **`Result` being a class**: It's a singleton stored in a static field. The indirection is a single pointer dereference — negligible.

## Recommendation

Start with **Experiment 1** (per-instance result cache on `Measure`). It directly targets the dominant cost (~4.8 ns generic virtual dispatch) and is a localised change within `Measure.cs`. The existing `MeasureBenchmark` can validate it immediately — `ProjectOntoSame` should drop from ~4.9 ns to ~1-2 ns.
