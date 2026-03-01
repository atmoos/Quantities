## Plan: Improve Coverage for Next Top 5 Opportunities

**TL;DR** — After the last coverage pass, the highest-impact remaining targets are `ScalarBuilder`, `Serializer<TUnit>`, `Frequency`, `QuantityFactory<TQuantity>`, and `Area`. This plan prioritizes branch-heavy construction and serialization code first, then closes quantity wrapper gaps.

---

### Scope

Target types (priority order):

1. [ScalarBuilder](../../source/Atmoos.Quantities/Core/Construction/ScalarBuilder.cs)
2. [Serializer<TUnit>](../../source/Atmoos.Quantities/Measures/Serializer.cs)
3. [Frequency](../../source/Atmoos.Quantities/Quantities/Frequency.cs)
4. [QuantityFactory<TQuantity>](../../source/Atmoos.Quantities/Core/Construction/QuantityFactory.cs)
5. [Area](../../source/Atmoos.Quantities/Quantities/Area.cs)

Primary objective: maximize reduction of combined `uncovered-lines + 2 * uncovered-branches` for these five targets.

---

### Baseline (current)

From `Atmoos.Quantities.Test` coverage report:

- `ScalarBuilder`: uncovered lines `10`, uncovered branches `20`, score `50`
- `Serializer<TUnit>`: uncovered lines `11`, uncovered branches `6`, score `23`
- `Frequency`: uncovered lines `11`, uncovered branches `6`, score `23`
- `QuantityFactory<TQuantity>`: uncovered lines `6`, uncovered branches `8`, score `22`
- `Area`: uncovered lines `8`, uncovered branches `6`, score `20`

Combined baseline score for these 5 targets: `138`

---

### Phase 1: Branch-First Infrastructure

#### 1) ScalarBuilder

Focus test additions on:

- Remaining system + prefix + alias/invertible branch combinations not yet exercised
- Reflection/generic-method fallback failure paths where stable via public entry points
- Invalid model combinations that force guard/fallback branches

Test design:

- Drive through `QuantityFactory` and parser entry points (avoid brittle reflection assertions)
- Use `[Theory]` datasets for system × prefix × exponent matrices

#### 2) Serializer<TUnit>

Focus test additions on:

- `exponent == 1` (omit exponent field) vs `exponent != 1` branch
- prefixed vs non-prefixed serialization path
- system casing normalization (`ToLowerInvariant`) behavior

Test design:

- Use writer spies/fakes to assert emitted keys and call order (`Start/Write/End`)
- Keep assertions on stable contract fields only

---

### Phase 2: Construction Flow Coverage

#### 3) QuantityFactory<TQuantity>

Focus test additions on:

- model-count switch branches: `[]`, `[single]`, `[left,right]`, and `> 2`
- unsupported exponent branch and unsupported model-count branch
- build mismatch branch (`quantity.IsOf<TQuantity>() == false`)
- complex builder product branch behavior (left/right exponent combinations)

Test design:

- Theory-driven model lists for cardinality and exponent variants
- Assert exception types/messages only where stable

---

### Phase 3: Quantity Wrapper Density

#### 4) Frequency

Focus test additions on:

- both creation/conversion paths (`Of<TUnit>`, `To<TUnit>`)
- equality and hash-code consistency branches
- `ToString()` and `ToString(format, provider)` forwarding behavior
- object-equality branch (`Equals(Object?)`) with non-matching type

Test design:

- deterministic conversion assertions with SI/metric units
- add explicit negative equality/object cases

#### 5) Area

Focus test additions on:

- both overload pairs (`Of`/`To` for `Power<TLength, Two>` and alias-based scalar path)
- alias conversion branch paths (e.g., litre-like area aliases where available)
- equality/hash-code and object-equality branches
- formatting and string-forwarding branches

Test design:

- keep conversion cases minimal but branch-complete
- separate conversion correctness from representation checks

---

### Execution Steps

1. Add/extend tests for `ScalarBuilder` paths.
2. Run targeted tests + coverage and record deltas.
3. Add/extend tests for `Serializer<TUnit>`.
4. Run targeted tests + coverage and record deltas.
5. Add/extend tests for `QuantityFactory<TQuantity>`.
6. Run targeted tests + coverage and record deltas.
7. Add/extend tests for `Frequency`.
8. Run targeted tests + coverage and record deltas.
9. Add/extend tests for `Area`.
10. Run full solution tests + coverage and record final deltas.

---

### Verification

Required checks:

- `dotnet build ../../source/Atmoos.Quantities.sln` succeeds
- `dotnet test ../../source/Atmoos.Quantities.sln` passes
- coverage report shows reduced uncovered lines and/or branches for each target

Recommended success criteria:

- each target improves in either line or branch coverage
- at least 3/5 targets improve in both line and branch coverage
- combined target score decreases materially from `138`

---

### Notes

- Prioritize branch-complete behavior tests over assertion volume.
- Prefer theory datasets for matrix branches (`system × prefix × exponent`).
- Re-prioritize within the five targets after each phase based on observed ROI.
