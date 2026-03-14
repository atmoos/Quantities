## Plan: Improve Coverage for Next Top 5 Classes (Round 5, Fresh Baseline)

**TL;DR** - Revalidated against a fresh, full coverage run (`dotnet test source/Atmoos.Quantities.sln --collect:"XPlat Code Coverage"`) and merged across all four generated Cobertura reports. The highest-impact current targets are now `Extensions`, `Measures`, `Dimension`, `Operators`, and `Parser<T>`.

---

### Scope

Target classes (priority order):

1. [Extensions](../../source/Atmoos.Quantities/Extensions.cs)
2. [Measures](../../source/Atmoos.Quantities/Measures/Measures.cs)
3. [Dimension](../../source/Atmoos.Quantities/Dimensions/Dimension.cs)
4. [Operators](../../source/Atmoos.Quantities/Operators.cs)
5. [Parser<T>](../../source/Atmoos.Quantities/Parsing/Parser.cs)

Primary objective: maximise reduction of combined `uncovered-lines + 2 * uncovered-branches` across these five classes.

---

### Baseline (fresh)

From the fresh run (merged across:
`source/Atmoos.Quantities.Test/TestResults/9936ae7f-171f-4674-bb28-ac48b0520cce/coverage.cobertura.xml`,
`source/Atmoos.Quantities.Units.Test/TestResults/50358d22-797b-4e56-b995-8e444d7b29d4/coverage.cobertura.xml`,
`source/Atmoos.Quantities.Serialization/Newtonsoft.Test/TestResults/001ad030-0720-414c-897b-9e19e81291b3/coverage.cobertura.xml`,
`source/Atmoos.Quantities.Serialization/Text.Json.Test/TestResults/8dd319a4-485f-4a6f-a05d-2c6de4263512/coverage.cobertura.xml`):

- `Extensions`: uncovered lines `19`, uncovered branches `2`, score `23`
- `Measures`: uncovered lines `9`, uncovered branches `6`, score `21`
- `Dimension`: uncovered lines `7`, uncovered branches `2`, score `11`
- `Operators`: uncovered lines `9`, uncovered branches `0`, score `9`
- `Parser<T>`: uncovered lines `3`, uncovered branches `3`, score `9`

Combined baseline score for these 5 targets: `73`

---

### Phase 1: Branch-First Targets

#### 1) Measures

Focus test additions on:

- `Invertible<TSelf, TInverse>.Power` sign branches (`>= 0` and `< 0`)
- `Product<TLeft, TRight>.Rep` representation switch branches
- `Alias<TAlias, TLinear>.Power` equality branch vs raised-linear fallback
- `Convenience.Raise<TMeasure>` unsupported/default exponent branch
- `PowerInverse<TLinear, TResult>` exponent `1` vs non-`1`

Test design:

- compact `[Theory]` exponent/sign matrices
- behaviour assertions via stable seams (`Representation`, parsing, quantity creation)

#### 2) Parser<T>

Focus test additions on:

- `IParser<T>.TryParse(String, out T)` token-count and numeric-parse branches
- `Parser<T>.TryParse(Double, String, out T)` model-count switch: `[]`, `[single]`, `[left,right]`, and `> 2`
- exception handling branches for `InvalidOperationException` and `NotSupportedException`
- `Parse(...)` throwing `FormatException` on failed parse paths

Test design:

- split positive and negative datasets
- assert exception types and stable message fragments only

---

### Phase 2: Core Behaviour + Algebra

#### 3) Extensions

Focus test additions on:

- `ValueOf<T>(exponent)` with positive, zero, and negative exponent inputs
- `ToString(IFormattable, String)` invariant-culture path
- `NotImplemented(...)` overloads (`Object` and generic) and message composition
- generic type-name formatting in `ClassName` (`NameOf<T>()` via nested generic types)

Test design:

- keep tests deterministic and culture-explicit
- verify message/type-name contracts without over-specifying caller line numbers

#### 4) Dimension

Focus test additions on:

- `Dimension` equality operators for `(null,null)`, `(null,_)`, and non-null paths
- `Scalar.Impl<T>.Pow` and `Root` branches (including zeroth-root exception)
- `Product.Multiply` simplification branches
- `Product.SimplifyExponents` gcd/sign branch outcomes

Test design:

- algebraic permutation tables using `Scalar.Of<TDim>()`
- keep equivalence assertions separate from formatting assertions

---

### Phase 3: Operator Surface Completion

#### 5) Operators

Focus test additions on:

- comparison operators (`>`, `>=`, `<`, `<=`) for equal/greater/less paths
- arithmetic operators (`+`, `-`, `*`, `/`) with symmetric scalar multiplication branches
- quantity ratio operator (`left / right`) behaviour and unit-consistent values
- equality/inequality operator forwarding coverage

Test design:

- choose one representative quantity type and a small value matrix
- keep assertions value-based and avoid redundant operator permutations

---

### Execution Steps

1. Add/extend tests for `Measures`.
2. Run targeted tests + coverage and record deltas.
3. Add/extend tests for `Parser<T>`.
4. Run targeted tests + coverage and record deltas.
5. Add/extend tests for `Extensions`.
6. Run targeted tests + coverage and record deltas.
7. Add/extend tests for `Dimension`.
8. Run targeted tests + coverage and record deltas.
9. Add/extend tests for `Operators`.
10. Run full solution tests + coverage and record final deltas.

---

### Verification

Required checks:

- `dotnet build ../../source/Atmoos.Quantities.sln` succeeds
- `dotnet test ../../source/Atmoos.Quantities.sln` passes
- fresh merged coverage shows reduced uncovered lines and/or branches for each target

Recommended success criteria:

- each target improves in either line or branch coverage
- at least 3/5 targets improve in both line and branch coverage
- combined target score decreases materially from `73`

---

### Notes

- This plan supersedes the previous round5 target set that was based on stale snapshot assumptions.
- Prioritise branch-complete tests before broad assertion volume.
- Re-prioritise within the five targets after each phase based on observed ROI.
- Avoid creating new test classes unless necessary; prefer extending existing relevant test files for cohesion.
