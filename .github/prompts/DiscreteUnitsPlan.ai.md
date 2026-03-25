# Discrete Units Implementation Plan

Implementation plan for adding discrete (named) units to the ten quantities introduced in the [quantity implementation prompt](../.github/prompts/next-10-quantities-implementation.prompt.md).

Only **discrete units** — standalone unit structs with their own `Representation` — are in scope.
Compound units (e.g. A·h for electric charge, kg/m³ for density) are already supported as aggregates by the quantity types themselves and are therefore explicitly excluded.

---

## 1  Scope and Assumptions

| Assumption | Detail |
|---|---|
| **Discrete-only** | We add only named unit structs. Compound measures (`Product<>`, `Quotient<>`, `Power<>`) are already usable through the quantities' generic `Of` / `To` overloads. |
| **Existing base units suffice** | `Mole` and `Candela` already exist and cover AmountOfSubstance and LuminousIntensity. No new unit files required for those two quantities. |
| **No named unit exists** | For Density, VolumetricFlowRate, MassFlowRate, Momentum, Impulse, and SpecificEnergy there are no widely-adopted named SI or metric discrete units. These quantities are fully served by their compound measure APIs. |
| **Two quantities gain units** | Only **ElectricCharge** and **Angle** require new discrete unit structs. |
| **AI transparency** | All new files are fully AI-generated and use the `.ai.` filename infix. All new types carry `[Ai]`. |
| **en-GB spelling** | All generated content follows en-GB conventions. |

---

## 2  Dependency Order and Rationale

```text
Wave 1 — Coulomb           (depends on: IElectricCharge dimension, already present)
Wave 2 — Radian            (depends on: IAngle dimension, already present)
Wave 3 — Degree, Gradian   (depend on: Radian as SI anchor)
Wave 4 — Turn              (depends on: Radian as SI anchor)
```

Coulomb and Radian are SI derived units with no transformation (identity to SI).
Non-standard angle units each define a `ToSi` conversion rooted in Radian.
The waves can realistically be collapsed into a single PR, but the ordering clarifies review priority.

---

## 3  Inventory

### 3.1  Quantities That Already Have Their Discrete Unit

| Quantity | Existing Unit | File | Action |
|---|---|---|---|
| AmountOfSubstance | `Mole` | `Units/Si/Mole.cs` | **None** — unit already exists. Add unit tests if missing. |
| LuminousIntensity | `Candela` | `Units/Si/Candela.cs` | **None** — unit already exists. Add unit tests if missing. |

### 3.2  Quantities Receiving New Discrete Units

| Quantity | Unit | System | Symbol | Conversion | File to Create |
|---|---|---|---|---|---|
| ElectricCharge | **Coulomb** | SI Derived | `C` | Identity (1 C = 1 A·s) | `Units/Si/Derived/Coulomb.ai.cs` |
| Angle | **Radian** | SI Derived | `rad` | Identity | `Units/Si/Derived/Radian.ai.cs` |
| Angle | **Degree** | NonStandard | `°` | $\pi / 180$ | `Units/NonStandard/Angle/Degree.ai.cs` |
| Angle | **Gradian** | NonStandard | `gon` | $\pi / 200$ | `Units/NonStandard/Angle/Gradian.ai.cs` |
| Angle | **Turn** | NonStandard | `rev` | $2\pi$ | `Units/NonStandard/Angle/Turn.ai.cs` |

### 3.3  Quantities With No Discrete Unit to Add

| Quantity | Reason |
|---|---|
| Density | No named SI unit; users express as `kg/m³` via `Quotient<Kilogram, Power<Metre, Three>>`. |
| VolumetricFlowRate | No named SI unit; users express as `m³/s` via `Quotient<…>`. |
| MassFlowRate | No named SI unit; users express as `kg/s` via `Quotient<…>`. |
| Momentum | No named SI unit; users express as `kg·m/s` via `Product<…>`. |
| Impulse | No named SI unit; users express as `N·s` via `Product<Newton, Second>`. |
| SpecificEnergy | No named SI unit; users express as `J/kg` via `Quotient<Joule, Kilogram>`. |

---

## 4  Per-Unit Implementation Checklist

### 4.1  Coulomb (ElectricCharge)

- **Category**: SI Derived, identity transformation
- **Interfaces**: `ISiUnit`, `IElectricCharge`
- **Reference**: <https://en.wikipedia.org/wiki/Coulomb>

| Artefact | Path (relative to `source/`) | Notes |
|---|---|---|
| Unit struct | `Atmoos.Quantities.Units/Si/Derived/Coulomb.ai.cs` | `ISiUnit, IElectricCharge`, Representation `"C"` |
| Unit tests | `Atmoos.Quantities.Units.Test/ElectricChargeTest.ai.cs` | Formatting, prefix scaling, round-trip via `A·s` compound |
| **Existing empty dir** | `Atmoos.Quantities.Units/NonStandard/ElectricCharge/` | No non-standard units planned; directory may remain empty or be removed. |

**Minimum tests**:
1. `CoulombToString` — `Si<Coulomb>()` formats as `"C"`.
2. `KiloCoulombToString` — `Si<Kilo, Coulomb>()` formats as `"kC"`.
3. `MicroCoulombToString` — `Si<Micro, Coulomb>()` formats as `"μC"`.
4. `CoulombRoundTripViaAmpereSecond` — `ElectricCharge.Of(v, Si<Coulomb>())` equals `ElectricCharge.Of(v, Si<Ampere>().Times(Si<Second>()))`.
5. `KiloCoulombToMilliCoulomb` — prefix-scaling conversion accuracy.

**Complexity**: **S** — single file, no transformation.

---

### 4.2  Radian (Angle)

- **Category**: SI Derived, identity transformation
- **Interfaces**: `ISiUnit`, `IAngle`
- **Reference**: <https://en.wikipedia.org/wiki/Radian>

| Artefact | Path | Notes |
|---|---|---|
| Unit struct | `Atmoos.Quantities.Units/Si/Derived/Radian.ai.cs` | `ISiUnit, IAngle`, Representation `"rad"` |
| Unit tests | `Atmoos.Quantities.Units.Test/AngleTest.ai.cs` | Formatting, conversions to/from Degree, Gradian, Turn |

**Minimum tests** (in shared `AngleTest.ai.cs`):
1. `RadianToString` — formats as `"rad"`.
2. `MilliRadianToString` — formats as `"mrad"`.
3. `PiRadiansToHalfTurn` — verifies internal consistency.

**Complexity**: **S**

---

### 4.3  Degree (Angle)

- **Category**: NonStandard
- **Interfaces**: `INonStandardUnit`, `IAngle`
- **Conversion**: $1° = \pi / 180 \text{ rad}$
- **Reference**: <https://en.wikipedia.org/wiki/Degree_(angle)>

| Artefact | Path | Notes |
|---|---|---|
| Unit struct | `Atmoos.Quantities.Units/NonStandard/Angle/Degree.ai.cs` | `ToSi`: `Math.PI / 180 * self.RootedIn<Radian>()` |
| Unit tests | (in `AngleTest.ai.cs`) | |

**Minimum tests**:
1. `DegreeToString` — formats as `"°"`.
2. `180DegreesToPiRadians` — conversion accuracy.
3. `360DegreesToTwoPiRadians` — full circle.
4. `RadianToDegreeRoundTrip` — round-trip stability.

**Complexity**: **S**

---

### 4.4  Gradian (Angle)

- **Category**: NonStandard
- **Interfaces**: `INonStandardUnit`, `IAngle`
- **Conversion**: $1 \text{ gon} = \pi / 200 \text{ rad}$
- **Reference**: <https://en.wikipedia.org/wiki/Gradian>

| Artefact | Path | Notes |
|---|---|---|
| Unit struct | `Atmoos.Quantities.Units/NonStandard/Angle/Gradian.ai.cs` | `ToSi`: `Math.PI / 200 * self.RootedIn<Radian>()` |
| Unit tests | (in `AngleTest.ai.cs`) | |

**Minimum tests**:
1. `GradianToString` — formats as `"gon"`.
2. `200GradiansToPiRadians` — conversion accuracy.
3. `90DegreesTo100Gradians` — cross-unit conversion.

**Complexity**: **S**

---

### 4.5  Turn (Angle)

- **Category**: NonStandard
- **Interfaces**: `INonStandardUnit`, `IAngle`
- **Conversion**: $1 \text{ rev} = 2\pi \text{ rad}$
- **Reference**: <https://en.wikipedia.org/wiki/Turn_(angle)>

| Artefact | Path | Notes |
|---|---|---|
| Unit struct | `Atmoos.Quantities.Units/NonStandard/Angle/Turn.ai.cs` | `ToSi`: `2 * Math.PI * self.RootedIn<Radian>()` |
| Unit tests | (in `AngleTest.ai.cs`) | |

**Minimum tests**:
1. `TurnToString` — formats as `"rev"`.
2. `OneTurnTo360Degrees` — conversion accuracy.
3. `OneTurnToTwoPiRadians` — conversion accuracy.
4. `HalfTurnTo200Gradians` — cross-unit.

**Complexity**: **S**

---

### 4.6  AmountOfSubstance Unit Tests (Mole — existing unit)

No new unit struct required. Tests needed if absent.

| Artefact | Path | Notes |
|---|---|---|
| Unit tests | `Atmoos.Quantities.Units.Test/AmountOfSubstanceTest.ai.cs` | Formatting, prefix scaling |

**Minimum tests**:
1. `MoleToString` — formats as `"mol"`.
2. `KiloMoleToString` — `Si<Kilo, Mole>()` formats as `"kmol"`.
3. `MilliMoleToString` — `Si<Milli, Mole>()` formats as `"mmol"`.
4. `MicroMoleToKiloMole` — prefix-scaling accuracy.

**Complexity**: **S**

---

### 4.7  LuminousIntensity Unit Tests (Candela — existing unit)

No new unit struct required. Tests needed if absent.

| Artefact | Path | Notes |
|---|---|---|
| Unit tests | `Atmoos.Quantities.Units.Test/LuminousIntensityTest.ai.cs` | Formatting, prefix scaling |

**Minimum tests**:
1. `CandelaToString` — formats as `"cd"`.
2. `KiloCandelaToString` — `Si<Kilo, Candela>()` formats as `"kcd"`.
3. `MilliCandelaToString` — `Si<Milli, Candela>()` formats as `"mcd"`.
4. `MilliCandelaToKiloCandela` — prefix-scaling accuracy.

**Complexity**: **S**

---

## 5  Files Summary

### New Files to Create

| # | File | Type |
|---|---|---|
| 1 | `Atmoos.Quantities.Units/Si/Derived/Coulomb.ai.cs` | Unit struct |
| 2 | `Atmoos.Quantities.Units/Si/Derived/Radian.ai.cs` | Unit struct |
| 3 | `Atmoos.Quantities.Units/NonStandard/Angle/Degree.ai.cs` | Unit struct |
| 4 | `Atmoos.Quantities.Units/NonStandard/Angle/Gradian.ai.cs` | Unit struct |
| 5 | `Atmoos.Quantities.Units/NonStandard/Angle/Turn.ai.cs` | Unit struct |
| 6 | `Atmoos.Quantities.Units.Test/ElectricChargeTest.ai.cs` | Test class |
| 7 | `Atmoos.Quantities.Units.Test/AngleTest.ai.cs` | Test class |
| 8 | `Atmoos.Quantities.Units.Test/AmountOfSubstanceTest.ai.cs` | Test class |
| 9 | `Atmoos.Quantities.Units.Test/LuminousIntensityTest.ai.cs` | Test class |

### Files to Modify

None expected. The existing `Systems.cs` already supports all five system entry-points (`Si<>`, `Metric<>`, `NonStandard<>`, etc.) and needs no changes.

---

## 6  Test Plan

### 6.1  Unit Test Categories

| Category | Description | Example |
|---|---|---|
| **Formatting** | `ToString()` produces correct symbol | `CoulombToString`, `DegreeToString` |
| **Prefix scaling** | SI prefix multipliers compose correctly | `KiloCoulombToMilliCoulomb` |
| **Cross-unit conversion** | Values convert accurately between discrete units | `180DegreesToPiRadians` |
| **Compound round-trip** | Discrete unit ↔ compound measure equivalence | `CoulombRoundTripViaAmpereSecond` |
| **Identity checks** | Full-circle / definition-based assertions | `OneTurnTo360Degrees` |

### 6.2  Test Execution

```bash
cd source && dotnet test Atmoos.Quantities.sln --settings .runsettings
```

Or equivalently:

```bash
dotnet test source/Atmoos.Quantities.sln --settings source/.runsettings
```

### 6.3  Coverage (optional)

```bash
dotnet tool restore
dotnet tool run reportgenerator \
  "-reports:source/**/TestResults/**/coverage.cobertura.xml" \
  "-targetdir:source/coveragereport" \
  "-reporttypes:Html;HtmlSummary"
```

---

## 7  Delivery Plan (PR Slicing)

Given the small scope, a **single PR** is recommended:

| PR | Contents | Rationale |
|---|---|---|
| **PR 1** | All 5 unit structs + all 4 test files | Total ~9 new files, all `S`-complexity, no cross-dependencies beyond what is already merged. A single PR keeps review overhead low. |

If finer granularity is preferred:

| PR | Contents |
|---|---|
| 1a | Coulomb + ElectricChargeTest |
| 1b | Radian, Degree, Gradian, Turn + AngleTest |
| 1c | AmountOfSubstanceTest + LuminousIntensityTest |

---

## 8  Risks and Mitigations

| Risk | Impact | Mitigation |
|---|---|---|
| `IAngle` is dimensionless — prefix scaling might behave unexpectedly | Medium | Verify at test time that `Si<Milli, Radian>()` correctly produces `0.001 rad`. The dimension system already handles dimensionless quantities via `IDimensionless<IAngle>`. |
| Degree symbol `°` rendering varies by font/platform | Low | Use Unicode `\u00B0` in the `Representation` property; consistent with how `Celsius` uses `°C`. |
| NonStandard angle conversions use `Math.PI` (floating-point) | Low | Accept IEEE 754 precision; existing tests use `Matches` / `PrecisionIsBounded` helpers for bounded comparisons. |
| Empty `NonStandard/ElectricCharge/` directory | Trivial | Leave for now; can be cleaned up in a separate housekeeping PR. |

---

## 9  Definition of Done

- [ ] All 5 unit structs compile without warnings.
- [ ] All new types annotated with `[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]`.
- [ ] All new files use the `.ai.` filename infix.
- [ ] `dotnet build source/Atmoos.Quantities.sln` succeeds.
- [ ] `dotnet test source/Atmoos.Quantities.sln --settings source/.runsettings` passes, including all new tests.
- [ ] Each unit's `Representation` matches the standard symbol.
- [ ] Conversions between Radian ↔ Degree ↔ Gradian ↔ Turn are numerically accurate within IEEE 754 bounds.
- [ ] Coulomb ↔ Ampere·Second round-trip is exact.
- [ ] No existing tests regress.
