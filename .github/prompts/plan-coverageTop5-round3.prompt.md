## Plan: Improve Coverage for Next Top 5 Classes (Round 3)

**TL;DR** — From the latest `round3e` coverage snapshot, the highest-impact concrete class targets are `ProductInjector`, `Introspection`, `Deserializer` (Newtonsoft), `Deserializer` (Text.Json), and `Dimensions.Product`. This plan prioritizes branch-heavy construction + deserialization paths first, then closes algebraic-dimension gaps.

---

### Scope

Target classes (priority order):

1. [ProductInjector](../../source/Atmoos.Quantities/Core/Construction/Injectors.cs)
2. [Introspection](../../source/Atmoos.Quantities/Common/Introspection.cs)
3. [Deserializer (Newtonsoft)](../../source/Atmoos.Quantities.Serialization/Newtonsoft/Deserializer.cs)
4. [Deserializer (Text.Json)](../../source/Atmoos.Quantities.Serialization/Text.Json/Deserializer.cs)
5. [Dimensions.Product](../../source/Atmoos.Quantities/Dimensions/Dimension.cs)

Primary objective: maximize reduction of combined `uncovered-lines + 2 * uncovered-branches` across these five classes.

---

### Baseline (current)

From `source/coveragereport/round3e/Cobertura.xml`:

- `ProductInjector`: uncovered lines `4`, uncovered branches `8`, score `20`
- `Introspection`: uncovered lines `8`, uncovered branches `4`, score `16`
- `Deserializer (Newtonsoft)`: uncovered lines `3`, uncovered branches `6`, score `15`
- `Deserializer (Text.Json)`: uncovered lines `3`, uncovered branches `5`, score `13`
- `Dimensions.Product`: uncovered lines `6`, uncovered branches `3`, score `12`

Combined baseline score for these 5 targets: `76`

---

### Phase 1: Branch-First Construction + Reflection

#### 1) ProductInjector

Focus test additions on:

- all `(leftExp, rightExp)` sign combinations: `(+, +)`, `(+, -)`, `(-, +)`, `(-, -)`
- unsupported/guard branch (`0` exponent involvement) that throws `NotSupportedException`
- two-step inject flow (`Inject<TLeft>()` then `Inject<TRight>()`) and produced builder type behavior
- `Build(in Double)` path for left-term single-measure construction

Test design:

- use `[Theory]` + `TheoryData<Int32, Int32, Type>` for matrix coverage
- assert branch outcome by behavior/resulting quantity, not brittle implementation details

#### 2) Introspection

Focus test additions on:

- `MostDerivedOf` tie-breaking and multi-interface inheritance depth ordering
- `InnerType` success (`exactly one`) and failure (`0` / `>1`) branches
- `InnerTypes` for interface vs class input, generic match vs non-match
- `Implements` vs `ImplementsGeneric` positive/negative combinations

Test design:

- small local test interfaces/types to isolate inheritance shapes
- explicit negative tests for `InvalidOperationException` branch in `InnerType`

---

### Phase 2: Deserialization Branch Density

#### 3) Deserializer (Newtonsoft)

Focus test additions on:

- string token vs structured-object token paths
- missing/extra/invalid fields and failure branches
- scalar vs compound measure model payloads
- malformed JSON and null-token handling paths

Test design:

- split success and failure datasets
- assert stable contract outcomes (parsed quantity / exception type)

#### 4) Deserializer (Text.Json)

Focus test additions on:

- string vs object payload branches
- missing-property and unsupported-shape branches
- invalid unit/measure symbol and parser failure propagation
- invalid numeric payload branches

Test design:

- mirror Newtonsoft datasets where contracts are intentionally equivalent
- keep serializer-specific parser differences explicit in test names

---

### Phase 3: Dimension Algebra Completion

#### 5) Dimensions.Product

Focus test additions on:

- `Pow`/`Root` switch branches (`0`, `1`, other; divide-by-zero root)
- `Multiply` simplification branches (`Unit`, matching scalar, partial-overlap product, fallback)
- `CommonRoot` set-comparison behavior with exponent normalization
- `SimplifyExponents` gcd/sign branches and resulting reduced structure

Test design:

- table-driven dimension compositions using `Scalar.Of<TDim>()`
- separate algebraic equivalence tests from string-format tests

---

### Execution Steps

1. Add/extend tests for `ProductInjector`.
2. Run targeted tests + coverage and record deltas.
3. Add/extend tests for `Introspection`.
4. Run targeted tests + coverage and record deltas.
5. Add/extend tests for `Deserializer (Newtonsoft)`.
6. Run targeted tests + coverage and record deltas.
7. Add/extend tests for `Deserializer (Text.Json)`.
8. Run targeted tests + coverage and record deltas.
9. Add/extend tests for `Dimensions.Product`.
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
- combined target score decreases materially from `76`

---

### Notes

- prioritize branch-complete tests over assertion volume
- for deserializers, prefer compact malformed-payload matrices over many near-duplicate tests
- re-prioritize within the five targets after each phase based on observed ROI

#### Outcome (verified)

Verification run completed on 2026-03-04 against fresh snapshot `source/coveragereport/round3f/Cobertura.xml`, compared to baseline `source/coveragereport/round3e/Cobertura.xml`.

Required checks:

- `dotnet build ../../source/Atmoos.Quantities.sln`: passed
- `dotnet test ../../source/Atmoos.Quantities.sln --collect:"XPlat Code Coverage"`: passed (`1447` total, `0` failed, `1` skipped)

Target deltas (`uncovered-lines`, `uncovered-branches`, `score = lines + 2 * branches`):

- `ProductInjector`: `4/8/20` to `0/0/0`
- `Introspection`: `8/4/16` to `1/2/5`
- `Deserializer (Newtonsoft)`: `3/6/15` to `0/2/4`
- `Deserializer (Text.Json)`: `3/5/13` to `0/2/4`
- `Dimensions.Product`: `6/3/12` to `1/1/3`

Combined score for the 5 targets: `76` to `16` (`-60`).

Success criteria result:

- each target improved in either line or branch coverage: met (5/5)
- at least `3/5` targets improved in both line and branch coverage: met (5/5)
- combined target score decreased materially from `76`: met
