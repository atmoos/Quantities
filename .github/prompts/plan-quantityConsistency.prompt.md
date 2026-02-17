## Plan: Fix Quantity Struct Inconsistencies

**TL;DR** — 19 quantity structs were checked against the templates in [new-quantity.md](../agents/new-quantity.md). 7 are fully consistent (Length, Velocity, Acceleration, Energy, Pressure, Data, Frequency). The remaining 12 have inconsistencies across 6 categories. The canonical reference is [Length.cs](../../source/Atmoos.Quantities/Quantities/Length.cs) for scalar quantities.

---

### Issue 1: Missing `using Atmoos.Quantities.Creation;` + qualified `Creation.Scalar<TUnit>` (8 files)

These scalar quantities omit the `using` directive and qualify `Creation.Scalar<TUnit>` instead of using unqualified `Scalar<TUnit>`:

- [Time.cs](../../source/Atmoos.Quantities/Quantities/Time.cs)
- [Mass.cs](../../source/Atmoos.Quantities/Quantities/Mass.cs)
- [Temperature.cs](../../source/Atmoos.Quantities/Quantities/Temperature.cs)
- [ElectricCurrent.cs](../../source/Atmoos.Quantities/Quantities/ElectricCurrent.cs)
- [LuminousIntensity.ai.cs](../../source/Atmoos.Quantities/Quantities/LuminousIntensity.ai.cs)
- [Force.cs](../../source/Atmoos.Quantities/Quantities/Force.cs)
- [Power.cs](../../source/Atmoos.Quantities/Quantities/Power.cs)
- [ElectricalResistance.cs](../../source/Atmoos.Quantities/Quantities/ElectricalResistance.cs)

The template and canonical example (Length) use `using Atmoos.Quantities.Creation;` with unqualified `Scalar<TUnit>`. Note: the invertible template (Frequency) intentionally uses qualified `Creation.Scalar<TUnit>`, so Frequency is correct as-is.

### Issue 2: Field naming not camelCase of type name (3 files)

| File | Actual field | Expected (camelCase of type name) |
|------|-------------|-----------------------------------|
| [ElectricCurrent.cs](../../source/Atmoos.Quantities/Quantities/ElectricCurrent.cs#L8) | `current` | `electricCurrent` |
| [ElectricPotential.cs](../../source/Atmoos.Quantities/Quantities/ElectricPotential.cs#L9) | `potential` | `electricPotential` |
| [ElectricalResistance.cs](../../source/Atmoos.Quantities/Quantities/ElectricalResistance.cs#L8) | `resistance` | `electricalResistance` |

Every other multi-word quantity (e.g., `DataRate` → `dataRate`, `LuminousIntensity` → `luminousIntensity`, `ElectricCurrent` → should be `electricCurrent`) follows the full camelCase convention. These three use shortened names.

### Issue 3: Missing `internal Quantity Value` property (1 file)

[Mass.cs](../../source/Atmoos.Quantities/Quantities/Mass.cs#L9) only has the explicit interface implementation `Quantity IQuantity<Mass>.Value => this.mass;` but is missing the corresponding `internal Quantity Value => this.mass;` property that all other 18 quantities have. Currently nothing accesses `Mass.Value` internally, but this breaks the pattern and would block any future cross-quantity operators involving Mass.

### Issue 4: PascalCase / wrong variable name in `Equals(Object?)` (3 files)

| File | Actual | Expected |
|------|--------|----------|
| [Area.cs](../../source/Atmoos.Quantities/Quantities/Area.cs#L33) | `obj is Area Area` | `obj is Area area` |
| [Volume.cs](../../source/Atmoos.Quantities/Quantities/Volume.cs#L37) | `obj is Volume Volume` | `obj is Volume volume` |
| [DataRate.cs](../../source/Atmoos.Quantities/Quantities/DataRate.cs#L32) | `obj is DataRate rate` | `obj is DataRate dataRate` |

Area and Volume use PascalCase (shadowing the type name). DataRate uses a shortened name `rate` instead of the field name `dataRate`.

### Issue 5: Method ordering differs from template (2 files)

Both [ElectricCurrent.cs](../../source/Atmoos.Quantities/Quantities/ElectricCurrent.cs#L25) and [ElectricPotential.cs](../../source/Atmoos.Quantities/Quantities/ElectricPotential.cs#L25) place `ToString(String?, IFormatProvider?)` between `Equals(T)` and `Equals(Object?)`.

**Template order**: `Equals(T)` → `Equals(Object?)` → `GetHashCode()` → `ToString()` → `ToString(String?, IFormatProvider?)`

### Issue 6: Wrong constraint order in `Of` method (1 file)

[DataRate.cs](../../source/Atmoos.Quantities/Quantities/DataRate.cs#L26-L27): The quotient `Of` method uses `IDimension, IUnit` constraint order instead of `IUnit, IDimension`.

**Actual**: `TNominator : IAmountOfInformation, IUnit` / `TDenominator : ITime, IUnit`
**Expected**: `TNominator : IUnit, IAmountOfInformation` / `TDenominator : IUnit, ITime`

All other quotient types (Velocity, Acceleration, Pressure) correctly use `IUnit, IDimension` order in `Of` while using `IDimension, IUnit` in `To`.

### ~~Issue 7: Missing `readonly` on alias `To` method (1 file)~~ FALSE POSITIVE

[Volume.cs](../../source/Atmoos.Quantities/Quantities/Volume.cs#L20): The alias `To<TVolume>` method is `public Volume To<TVolume>(...)` but [Area.cs](../../source/Atmoos.Quantities/Quantities/Area.cs#L20) (same category) and the template both use `public readonly Area To<TArea>(...)`.

---

### Steps

1. **Fix Issue 1** — In each of the 8 scalar files: add `using Atmoos.Quantities.Creation;` and replace `Creation.Scalar<TUnit>` with `Scalar<TUnit>` in `To` and `Of` methods.

2. **Fix Issue 2** — In [ElectricCurrent.cs](../../source/Atmoos.Quantities/Quantities/ElectricCurrent.cs), [ElectricPotential.cs](../../source/Atmoos.Quantities/Quantities/ElectricPotential.cs), and [ElectricalResistance.cs](../../source/Atmoos.Quantities/Quantities/ElectricalResistance.cs): rename the private field and update all references (field, constructor, `Value` properties, `To`, `Of`, `Equals`, `GetHashCode`, `ToString`).

3. **Fix Issue 3** — In [Mass.cs](../../source/Atmoos.Quantities/Quantities/Mass.cs): add `internal Quantity Value => this.mass;` before the explicit interface line.

4. **Fix Issue 4** — In [Area.cs](../../source/Atmoos.Quantities/Quantities/Area.cs), [Volume.cs](../../source/Atmoos.Quantities/Quantities/Volume.cs), and [DataRate.cs](../../source/Atmoos.Quantities/Quantities/DataRate.cs): fix the variable name in `Equals(Object?)` to use the (corrected) field name in camelCase.

5. **Fix Issue 5** — In [ElectricCurrent.cs](../../source/Atmoos.Quantities/Quantities/ElectricCurrent.cs) and [ElectricPotential.cs](../../source/Atmoos.Quantities/Quantities/ElectricPotential.cs): reorder methods to match template order.

6. **Fix Issue 6** — In [DataRate.cs](../../source/Atmoos.Quantities/Quantities/DataRate.cs): swap constraint order in the quotient `Of` method to `IUnit, IDimension`.

7. ~~**Fix Issue 7**~~ — In [Volume.cs](../../source/Atmoos.Quantities/Quantities/Volume.cs): add `readonly` modifier to the alias `To<TVolume>` method.

8. **Build** — Run `dotnet build ../../source/Atmoos.Quantities.sln` to verify all changes compile.

9. **Run tests** — Run `dotnet test ../../source/Atmoos.Quantities.sln` to verify no regressions.

### Verification

- `dotnet build ../../source/Atmoos.Quantities.sln` must succeed
- `dotnet test ../../source/Atmoos.Quantities.sln` must pass all existing tests
- Visual inspection: all 19 quantity files should follow their respective category template

### Decisions

- Issue 1 (using/qualified): Align to the template convention (unqualified + import), matching the canonical Length example, rather than the majority pattern (qualified without import)
- Issue 2 (field naming): Use strict camelCase of the full type name per the template convention, even though shortened names are arguably more readable for long type names
