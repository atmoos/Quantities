## Plan: Improve Coverage for Next Top 5 Classes (Round 4)

**TL;DR** - From the latest `round3f` snapshot, the highest-impact remaining class targets are `Measures`, `Arithmetic<TSelf>`, `Dimension`, `Parser<T>`, and `QuantitySerialization` (Newtonsoft). This plan prioritizes branch-dense measure composition first, then parser and converter-cache behavior.

---

### Scope

Target classes (priority order):

1. [Measures](../../source/Atmoos.Quantities/Measures/Measures.cs)
2. [Arithmetic<TSelf>](../../source/Atmoos.Quantities/Measures/Arithmetic.cs)
3. [Dimension](../../source/Atmoos.Quantities/Dimensions/Dimension.cs)
4. [Parser<T>](../../source/Atmoos.Quantities/Parsing/Parser.cs)
5. [QuantitySerialization (Newtonsoft)](../../source/Atmoos.Quantities.Serialization/Newtonsoft/QuantitySerialization.cs)

Primary objective: maximize reduction of combined `uncovered-lines + 2 * uncovered-branches` across these five classes.

---

### Baseline (current)

From `source/coveragereport/round3f/Cobertura.xml`:

- `Measures`: uncovered lines `38`, uncovered branches `22`, score `82`
- `Arithmetic<TSelf>`: uncovered lines `10`, uncovered branches `10`, score `30`
- `Dimension`: uncovered lines `16`, uncovered branches `6`, score `28`
- `Parser<T>`: uncovered lines `8`, uncovered branches `6`, score `20`
- `QuantitySerialization (Newtonsoft)`: uncovered lines `4`, uncovered branches `8`, score `20`

Combined baseline score for these 5 targets: `180`

---

### Phase 1: Branch-First Measure Composition

#### 1) Measures

Focus test additions on:

- `Invertible<TSelf, TInverse>.Power` sign branches (`>= 0` and `< 0`)
- `Product<TLeft, TRight>.Rep` three representation branches (`left/denominator`, `right/denominator`, joiner path)
- `Alias<TAlias, TLinear>.Power` equality branch vs raised-linear fallback
- `Convenience.Raise<TMeasure>` exponent switch density (positive, zero, negative, and unsupported/default branch)
- `PowerInverse<TLinear, TResult>` branch for exponent `1` vs non-`1`

Test design:

- Use compact `[Theory]` matrices for exponent/sign combinations
- Prefer behavior assertions through stable public seams (`Representation`, parsing, quantity construction)

#### 2) Arithmetic<TSelf>

Focus test additions on:

- `Map<TRight>` `target` switch paths: `Unit`, matching linear target, fallback chain success, and null-result failure
- `Invert<TResult, TRight>` composition branch combinations via left/right exponent signs
- inner `Injector<TResult>.Left<TLeft>.Inject<TRight>()` switch cases: `(0,0)`, `(<0,>0)`, `(0,_)`, `(_,0)`, default product
- `Visitor.Fallback.Build` direct match vs product chain path vs `null`

Test design:

- Build dimensions deliberately to hit each branch, then assert result measure identity and polynomial behavior
- Keep fallback assertions semantic (success/failure and resulting dimension), not implementation-coupled

---

### Phase 2: Dimension Algebra Completion

#### 3) Dimension

Focus test additions on:

- `Dimension` equality operators for `(null,null)`, `(null,_)`, and non-null comparison branches
- `Scalar.Impl<T>.Multiply` switch paths (`Unit`, same scalar cancellation, scalar product, swapped fallback)
- `Scalar.Impl<T>.Pow`/`Root` branches including zeroth-root exception branch
- `Product.Multiply` branches: identity, scalar overlap simplify, dimension overlap simplify, fallback product
- `Product.SimplifyExponents` gcd/sign branches and reduced-shape outcomes

Test design:

- Table-driven algebraic permutations using `Scalar.Of<TDim>()`
- Separate algebraic equivalence checks from `ToString()` formatting checks

---

### Phase 3: Parser + Newtonsoft Converter Cache

#### 4) Parser<T>

Focus test additions on:

- `IParser<T>.TryParse(String, out T)` split/parse branches: bad token count, invalid number, valid handoff
- `Parser<T>.TryParse(Double, String, out T)` model-count branches: `[]`, `[single]`, `[left,right]`, and `> 2`
- exception handling branches in `Parser<T>.TryParse(Double, String, out T)` for `InvalidOperationException` and `NotSupportedException`
- `Parse(...)` methods throwing `FormatException` on failed parse paths

Test design:

- Positive and negative datasets separated to keep branch intent explicit
- Assert exception type and stable message fragments only

#### 5) QuantitySerialization (Newtonsoft)

Focus test additions on:

- `CanConvert` cache-hit fast path vs first-time creation path
- non-quantity/non-value-type branch returning `false`
- converter creation failure branch (`Activator.CreateInstance` not `JsonConverter`)
- `ReadJson` with cached converter vs uncached type (`null` path)
- `WriteJson` with cached type, uncached type, and `null` value branch

Test design:

- Use a controlled assembly set and multiple quantity types to assert cache behavior deterministically
- Keep JSON payload assertions minimal; prioritize converter dispatch paths

---

### Execution Steps

1. Add/extend tests for `Measures` branch matrices.
2. Run targeted tests + coverage and record deltas.
3. Add/extend tests for `Arithmetic<TSelf>`.
4. Run targeted tests + coverage and record deltas.
5. Add/extend tests for `Dimension`.
6. Run targeted tests + coverage and record deltas.
7. Add/extend tests for `Parser<T>`.
8. Run targeted tests + coverage and record deltas.
9. Add/extend tests for `QuantitySerialization` (Newtonsoft).
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
- combined target score decreases materially from `180`

---

### Notes

- Prioritize branch-complete tests over assertion volume.
- Prefer compact theory datasets that cover multiple switch arms per test group.
- Re-prioritize within the five targets after each phase based on observed ROI.
- Avoid creating new tests classes (files) unless absolutely necessary; prefer adding to existing relevant test classes for cohesion.
