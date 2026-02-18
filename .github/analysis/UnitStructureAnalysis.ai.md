# Unit Structure Analysis

**Date**: 18 February 2026

This document analyses the structural consistency of unit definitions across the Atmoos.Quantities project.
The internals of units (representation and transformation) are explicitly out of scope.

---

## Unit Systems

The project organises units into four systems, each with a corresponding marker interface:

| System | Namespace | Marker Interface |
|--------|-----------|-----------------|
| SI | `Units.Si` | `ISiUnit` |
| SI Metric | `Units.Si.Metric` | `IMetricUnit` |
| Imperial | `Units.Imperial` | `IImperialUnit` |
| Non-Standard | `Units.NonStandard` | `INonStandardUnit` |

---

## Structural Pattern

Every unit follows the same structural template:

```csharp
public readonly struct <UnitName> : <ISystemMarker>, <IDimension>
```

Key characteristics:

- **`readonly struct`** — all units are immutable value types.
- **Dual marker interfaces** — every unit implements exactly one system marker and one dimension marker.
- **File-scoped namespaces** — consistently used throughout.
- **One unit per file** — clean file organisation.
- **Namespace matches folder structure** — e.g., `Atmoos.Quantities.Units.Si` corresponds to `/Units/Si/`.

---

## Consistency Assessment

### ✅ Consistent Aspects

| Aspect | Status |
|--------|--------|
| All units are `readonly struct` | Consistent |
| Dual marker interfaces (system + dimension) | Consistent |
| One unit per file | Consistent |
| Namespace ↔ folder alignment | Consistent |
| File-scoped namespaces | Consistent |
| Prefix mechanism via `IMetricPrefix` (avoids type explosion) | Good design |

### ⚠️ Points of Attention

#### 1. `IMetricUnit` vs `ISiUnit` Boundary

The distinction between `ISiUnit` (base SI units like `Metre`, `Kelvin`, `Mole`) and `IMetricUnit` (metric-compatible units like `Hour`, `Minute`, `Litre`, `Tonne`) is **physically sound** but **implicitly defined**.

There is no documentation or enforced rule that makes the boundary between these two system markers explicit. A contributor could reasonably question whether a given unit belongs to `ISiUnit` or `IMetricUnit`.

**Recommendation**: Document the criteria for `ISiUnit` vs `IMetricUnit` classification explicitly.

#### 2. Baked-In Prefixed Units

SI prefixes (Kilo, Milli, Pico, etc.) are handled generically via `IMetricPrefix`, which is an excellent design choice that avoids combinatorial explosion. However, `Tonne` exists as a standalone unit in `Si.Metric` despite being essentially `Mega` + `Gram`.

This is a justified exception (the tonne has its own identity and symbol), but it breaks the prefix pattern. Any similar future exceptions should be treated with the same deliberation.

**Recommendation**: Consider documenting why `Tonne` is an explicit unit rather than a prefixed `Gram`.

#### 3. Derived Units

Units like `Newton` (if present) that represent derived dimensions (e.g., `IForce`) should follow the same dual-interface pattern as base units. Whether all derived units consistently adhere to this pattern warrants verification.

**Recommendation**: Verify that derived units follow the same `readonly struct` + dual marker interface pattern.

#### 4. Imperial Unit System Marker Consistency

All imperial units should consistently implement `IImperialUnit`. If any imperial unit accidentally implements `IMetricUnit` or `ISiUnit`, that would be a design error.

**Recommendation**: Ensure this invariant is enforced, ideally through a test.

#### 5. Non-Standard Units — Symmetry

Non-standard units implement `INonStandardUnit`. From a structural standpoint, they should receive the same level of design consistency (dual marker interfaces, `readonly struct`, etc.) as units in other systems.

**Recommendation**: Confirm that non-standard units are structurally symmetric with SI and Imperial units.

---

## Summary

The overall unit design is **clean, consistent, and well-structured**. The few inconsistencies identified are minor and represent deliberate design trade-offs rather than oversights. The main areas for improvement are:

1. **Documenting** the `ISiUnit` / `IMetricUnit` boundary criteria.
2. **Documenting** exceptions to the prefix pattern (e.g., `Tonne`).
3. **Verifying** derived unit consistency through tests.
4. **Enforcing** system marker correctness through tests.
