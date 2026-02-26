```prompt
## Plan: Improve Coverage for Top 5 Opportunities

**TL;DR** — Coverage analysis identified 5 high-impact targets: `ScalarBuilder`, `Product`, `Quantity`, `ModelParser`, and `BinaryPrefix`. This plan prioritizes branch-heavy paths first, adds focused xUnit theories for combinatorial behavior, and validates impact after each phase using line + branch deltas.

---

### Scope

Target types (in priority order):

1. [ScalarBuilder](../../source/Atmoos.Quantities/Core/Construction/ScalarBuilder.cs)
2. [Product](../../source/Atmoos.Quantities/Dimensions/Product.cs)
3. [Quantity](../../source/Atmoos.Quantities/Core/Quantity.cs)
4. [ModelParser](../../source/Atmoos.Quantities/Parsing/ModelParser.cs)
5. [BinaryPrefix](../../source/Atmoos.Quantities/Prefixes/BinaryPrefix.cs)

Primary objective: maximize combined uncovered line + branch reduction for these five types.

---

### Baseline (before changes)

1. Run full test suite with coverage collection:
   - `dotnet test --collect:"XPlat Code Coverage"`
2. Export per-class baseline for the five target types (uncovered lines, uncovered branches, weighted score).
3. Keep baseline table in PR/notes for before/after comparison.

---

### Phase 1: Branch-First Targets

#### 1) ScalarBuilder (highest branch potential)

Focus test additions on:

- Builder registration lifecycle (first registration, duplicate registration, overwrite/reject paths)
- Unknown symbol/unit/prefix lookup behavior
- Empty/null/whitespace inputs where accepted by API surface
- Generic overload paths and fallback/default branch behavior
- Exceptional paths (invalid construction combinations)

Test design:

- Prefer `[Theory]` + `TheoryData<...>` for matrix coverage
- Add explicit negative tests for each expected guard/exception branch

#### 2) Product

Focus test additions on:

- Multiplication/division composition branch paths
- Reduction/simplification/cancellation logic
- Identity/neutral cases and operand-order variants
- Equivalent dimension expressions built through different operation orders

Test design:

- Table-driven tests for algebraic permutations
- Separate semantic-equivalence checks from representation/format checks

---

### Phase 2: Core Quantity Behavior

#### 3) Quantity

Focus test additions on:

- Arithmetic operators and comparison branches
- Equality and hash-code consistency for equivalent values
- Formatting and string conversion branches (`ToString` overloads / format-provider paths)
- Numeric edge values (0, sign changes, very large/small, NaN/Infinity where supported)

Test design:

- Keep operator tests deterministic and unit-invariant where possible
- Include round-trip assertions for formatting/parsing if supported by public APIs

---

### Phase 3: Parser + Prefix Density

#### 4) ModelParser

Focus test additions on:

- Successful parses across valid grammar variants
- Invalid token/order/separator scenarios
- Whitespace and culture-sensitive number handling
- Ambiguous/partial inputs and expected failure modes

Test design:

- Positive and negative cases split into separate theory datasets
- Assert both result and failure semantics (type/message only where stable)

#### 5) BinaryPrefix

Focus test additions on:

- All defined prefix symbols/names and expected factors
- Case-sensitivity and alias handling (if applicable)
- Unknown prefix behavior and failure branches
- Formatting/serialization branch paths involving binary prefixes

Test design:

- Exhaustive map-based theory over known prefixes
- Single-source expected factors to avoid test drift

---

### Execution Steps

1. Add/extend tests for `ScalarBuilder`.
2. Run targeted tests + coverage and record deltas.
3. Add/extend tests for `Product`.
4. Run targeted tests + coverage and record deltas.
5. Add/extend tests for `Quantity`.
6. Run targeted tests + coverage and record deltas.
7. Add/extend tests for `ModelParser`.
8. Run targeted tests + coverage and record deltas.
9. Add/extend tests for `BinaryPrefix`.
10. Run full test suite + coverage and record final deltas.

---

### Verification

Required checks:

- `dotnet build ../../source/Atmoos.Quantities.sln` succeeds
- `dotnet test ../../source/Atmoos.Quantities.sln` passes
- Coverage report shows reduced uncovered lines and branches in all 5 target types

Recommended success criteria:

- Each target type improves in either line or branch coverage
- At least 3 of 5 targets improve in both line and branch coverage
- Combined weighted score (`uncovered-lines + 2 * uncovered-branches`) decreases materially from baseline

---

### Notes

- Favor branch-complete tests over snapshot-style assertion volume.
- Keep tests minimal and behavior-focused; avoid asserting unstable formatting text unless part of contract.
- Re-prioritize within the five targets after each phase based on observed coverage ROI.

```
