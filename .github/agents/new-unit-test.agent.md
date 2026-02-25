---
name: New Unit Test
description: Generate tests for new units (measures) added to the Atmoos.Quantities.Units project.
tools: ['edit/editFiles', 'edit/createDirectory', 'edit/createFile', 'search/codebase', 'read/terminalLastCommand', 'execute/runInTerminal', 'read/problems']
---

# New Unit Test Generation Agent

Generate xUnit tests for new units (measures) added to the Atmoos.Quantities library.

## Instructions

When asked to create tests for a new unit, follow these steps:

1. **Identify the unit** and its dimension, system, and conversion definition.
2. **Locate the corresponding test file** for the quantity dimension.
3. **Generate tests** following the established patterns for that quantity type.
4. **Build and run tests** to verify everything compiles and passes.

Always adhere to the coding conventions defined in `.github/copilot-instructions.md`.

---

## Test Project Structure

All unit tests live in `source/Atmoos.Quantities.Units.Test/`.

| File                          | Quantity Dimension       |
| ----------------------------- | ------------------------ |
| `LengthTest.cs`               | `ILength`                |
| `MassTest.cs`                 | `IMass`                  |
| `TimeTest.cs`                 | `ITime`                  |
| `TemperatureTest.cs`          | `ITemperature`           |
| `AreaTest.cs`                 | `IArea`                  |
| `VolumeTest.cs`               | `IVolume`                |
| `VelocityTest.cs`             | `IVelocity`              |
| `AccelerationTest.cs`         | `IAcceleration`          |
| `ForceTest.cs`                | `IForce`                 |
| `EnergyTest.cs`               | `IEnergy`                |
| `PowerTest.cs`                | `IPower`                 |
| `PressureTest.cs`             | `IPressure`              |
| `FrequencyTest.cs`            | `IFrequency`             |
| `ElectricCurrentTest.cs`      | `IElectricCurrent`       |
| `ElectricPotentialTest.cs`    | `IElectricPotential`     |
| `ElectricalResistanceTest.cs` | `IElectricalResistance`  |
| `DataTest.cs`                 | `IAmountOfInformation`   |
| `DataRateTest.cs`             | `IInformationRate`       |

### Global Imports (Usings.cs)

The test project has these global usings available in every test file:

```csharp
global using Atmoos.Quantities.Physics;
global using Atmoos.Quantities.Prefixes;
global using Atmoos.Quantities.Units.Imperial.Length;
global using Atmoos.Quantities.Units.Si;
global using Xunit;
global using static Atmoos.Quantities.Systems;
global using static Atmoos.Quantities.TestTools.Convenience;
global using static Atmoos.Quantities.TestTools.Traits;
```

This makes the following available without explicit imports:
- Measure factories: `Si<T>()`, `Si<TPrefix, T>()`, `Metric<T>()`, `Metric<TPrefix, T>()`, `Imperial<T>()`, `NonStandard<T>()`, `Binary<TPrefix, T>()`
- Compound measure builders: `Square()`, `Cubic()`, `.Per()`
- Assertion helpers: `FormattingMatches()`, `Matches()`, `PrecisionIsBounded()`, `IsTrue()`, `IsFalse()`
- Precision constants: `FullPrecision`, `MediumPrecision`, `LowPrecision`, `VeryLowPrecision`
- xUnit attributes: `[Fact]`, `[Theory]`, `[MemberData]`

---

## Test Categories

Every new unit should have tests from the following categories, selected based on the unit's characteristics.

### Category 1: Formatting Test (Required for every unit)

Verifies that the unit's `Representation` property produces the correct symbol in formatted output.

**Pattern**:

```csharp
[Fact]
public void {UnitName}ToString() => FormattingMatches(v => {Quantity}.Of(v, {measure}), "{symbol}");
```

**Examples**:

```csharp
// SI unit
[Fact]
public void MetreToString() => FormattingMatches(v => Length.Of(v, Si<Metre>()), "m");

// SI with prefix
[Fact]
public void KiloMetreToString() => FormattingMatches(v => Length.Of(v, Si<Kilo, Metre>()), "km");

// Metric unit
[Fact]
public void GramToString() => FormattingMatches(v => Mass.Of(v, Metric<Gram>()), "g");

// Imperial unit
[Fact]
public void MileToString() => FormattingMatches(v => Length.Of(v, Imperial<Mile>()), "mi");

// NonStandard unit
[Fact]
public void DelisleToString() => FormattingMatches(v => Temperature.Of(v, NonStandard<Delisle>()), "°De");

// Compound measure
[Fact]
public void KnotToString() => FormattingMatches(v => Velocity.Of(v, NonStandard<Knot>()), "kn");
```

---

### Category 2: SI Conversion Round-Trip (Required for non-SI units)

Verifies the unit's `ToSi` transformation is correct by converting to the SI reference unit and back.

**Pattern** (to SI reference):

```csharp
[Fact]
public void {UnitName}To{SiUnit}()
{
    {Quantity} value = {Quantity}.Of({inputValue}, {unitMeasure});
    {Quantity} expected = {Quantity}.Of({expectedSiValue}, {siMeasure});

    {Quantity} actual = value.To({siMeasure});

    actual.Matches(expected);
}
```

**Pattern** (from SI reference):

```csharp
[Fact]
public void {SiUnit}To{UnitName}()
{
    {Quantity} value = {Quantity}.Of({inputSiValue}, {siMeasure});
    {Quantity} expected = {Quantity}.Of({expectedValue}, {unitMeasure});

    {Quantity} actual = value.To({unitMeasure});

    actual.Matches(expected);
}
```

**Examples**:

```csharp
[Fact]
public void GramToKilogram()
{
    Mass mass = Mass.Of(1600, Metric<Gram>());
    Mass expected = Mass.Of(1.6, Si<Kilogram>());

    Mass actual = mass.To(Si<Kilogram>());

    actual.Matches(expected);
}

[Fact]
public void KilogramToGram()
{
    Mass mass = Mass.Of(3.5, Si<Kilogram>());
    Mass expected = Mass.Of(3500, Metric<Gram>());

    Mass actual = mass.To(Metric<Gram>());

    actual.Matches(expected);
}
```

For temperature-like units with offsets, use well-known reference points:

```csharp
[Fact]
public void CelsiusToKelvin()
{
    Temperature temperature = Temperature.Of(312 - 273.15, Metric<Celsius>());
    Temperature expected = Temperature.Of(312, Si<Kelvin>());

    Temperature actual = temperature.To(Si<Kelvin>());

    actual.Matches(expected);
}
```

---

### Category 3: Cross-System Conversion (Recommended)

Verifies conversions between different unit systems (e.g., metric to imperial).

**Pattern**:

```csharp
[Fact]
public void {SourceUnit}To{TargetUnit}()
{
    {Quantity} source = {Quantity}.Of({inputValue}, {sourceMeasure});
    {Quantity} expected = {Quantity}.Of({expectedValue}, {targetMeasure});

    {Quantity} actual = source.To({targetMeasure});

    actual.Matches(expected);
}
```

**Examples**:

```csharp
[Fact]
public void MileToKilometre()
{
    Length miles = Length.Of(1, Imperial<Mile>());
    Length kilometres = miles.To(Si<Kilo, Metre>());
    PrecisionIsBounded(1.609344, kilometres);
}

[Fact]
public void KilometreToMile()
{
    Length kilometres = Length.Of(1.609344, Si<Kilo, Metre>());
    Length miles = kilometres.To(Imperial<Mile>());
    PrecisionIsBounded(1d, miles);
}
```

---

### Category 4: Definition Equivalence (Required for derived units)

Verifies that a derived unit's definition holds by asserting equivalence with the fundamental definition using `Assert.Equal()`.

**Pattern**:

```csharp
[Fact]
public void DefinitionOf{UnitName}Holds()
{
    {Quantity} defined = {Quantity}.Of({definitionValue}, {definedMeasure});
    {Quantity} equivalent = {Quantity}.Of({equivalentValue}, {equivalentMeasure});

    Assert.Equal(defined, equivalent);
}
```

**Examples**:

```csharp
// 1 mile = 5280 feet
[Fact]
public void FootToMile()
{
    Length feet = Length.Of(5280, Imperial<Foot>());
    Length expected = Length.Of(1, Imperial<Mile>());

    Length actual = feet.To(Imperial<Mile>());

    actual.Matches(expected);
}

// 1 pound = 7000 grains
[Fact]
public void DefinitionOfGrainHolds()
{
    Assert.Equal(onePound, Mass.Of(7000, Imperial<Grain>()));
}

// 1 acre = 43560 square feet
[Fact]
public void AcreDividedBySquareFeet()
{
    Area acres = Area.Of(2, Imperial<Acre>());
    Area squareFeet = Area.Of(2 * 43560, Square(Imperial<Foot>()));
    Assert.Equal(acres, squareFeet);
}
```

---

### Category 5: Intra-System Conversion (Recommended for related units)

Verifies conversion between units in the same system that are related by derivation (e.g., `DerivedFrom<T>`).

**Pattern**:

```csharp
[Fact]
public void {SourceUnit}To{TargetUnit}()
{
    {Quantity} source = {Quantity}.Of({inputValue}, {sourceMeasure});
    {Quantity} expected = {Quantity}.Of({expectedValue}, {targetMeasure});

    {Quantity} actual = source.To({targetMeasure});

    actual.Matches(expected);
}
```

**Examples**:

```csharp
[Fact]
public void OneMileInYards()
{
    Length length = Length.Of(1, Imperial<Mile>());
    Length expected = Length.Of(1760, Imperial<Yard>());

    Length actual = length.To(Imperial<Yard>());

    actual.Matches(expected);
}

[Fact]
public void RoodToPerches()
{
    Area rood = Area.Of(1, Imperial<Rood>());
    Area expected = Area.Of(40, Imperial<Perch>());

    Area actual = rood.To(Imperial<Perch>());

    actual.Matches(expected);
}
```

---

### Category 6: Arithmetic Operations (Optional, for thorough coverage)

Verifies addition and subtraction between different units of the same quantity.

**Pattern** (addition):

```csharp
[Fact]
public void Add{UnitA}To{UnitB}()
{
    {Quantity} a = {Quantity}.Of({valueA}, {measureA});
    {Quantity} b = {Quantity}.Of({valueB}, {measureB});
    {Quantity} result = a + b;
    PrecisionIsBounded({expectedValue}, result);
}
```

**Pattern** (subtraction):

```csharp
[Fact]
public void Subtract{UnitB}From{UnitA}()
{
    {Quantity} a = {Quantity}.Of({valueA}, {measureA});
    {Quantity} b = {Quantity}.Of({valueB}, {measureB});
    {Quantity} result = a - b;
    PrecisionIsBounded({expectedValue}, result);
}
```

**Examples**:

```csharp
[Fact]
public void AddMilesToKilometres()
{
    Length kilometres = Length.Of(1, Si<Kilo, Metre>());
    Length miles = Length.Of(1, Imperial<Mile>());
    Length result = kilometres + miles;
    PrecisionIsBounded(2.609344, result);
}
```

---

### Category 7: Cross-Quantity Operations (When applicable)

Verifies that the unit participates correctly in cross-quantity operations (multiplication, division producing other quantities).

**Pattern**:

```csharp
[Fact]
public void {QuantityA}Times{QuantityB}Is{ResultQuantity}()
{
    {QuantityA} a = {QuantityA}.Of({valueA}, {measureA});
    {QuantityB} b = {QuantityB}.Of({valueB}, {measureB});
    {ResultQuantity} expected = {ResultQuantity}.Of({expectedValue}, {resultMeasure});

    {ResultQuantity} actual = a * b;

    actual.Matches(expected);
}
```

**Examples**:

```csharp
[Fact]
public void AreTimesMeterIsCubicMetre()
{
    Area area = Area.Of(1, Metric<Are>());
    Length length = Length.Of(10, Si<Metre>());
    Volume expected = Volume.Of(10 * 10 * 10, Cubic(Si<Metre>()));

    Volume actual = area * length;

    actual.Matches(expected);
}

[Fact]
public void NewtonFromPressureAndArea()
{
    Pressure pressure = Pressure.Of(800, Si<Newton>().Per(Square(Si<Metre>())));
    Area area = Area.Of(2, Square(Si<Metre>()));
    Force expected = Force.Of(1600, Si<Newton>());

    Force actual = pressure * area;

    actual.Matches(expected);
}
```

---

### Category 8: Prefix Scaling (For metric/SI units that support prefixes)

Verifies that standard metric prefixes work correctly with the unit.

**Pattern**:

```csharp
[Fact]
public void {Prefix}{UnitName}To{UnitName}()
{
    {Quantity} prefixed = {Quantity}.Of(1, {PrefixMeasure});
    {Quantity} expected = {Quantity}.Of({scaleFactor}, {baseMeasure});

    {Quantity} actual = prefixed.To({baseMeasure});

    actual.Matches(expected);
}
```

**Examples**:

```csharp
[Fact]
public void MetreToKilometre()
{
    Length metres = Length.Of(1000, Si<Metre>());
    Length kilometres = metres.To(Si<Kilo, Metre>());
    PrecisionIsBounded(1d, kilometres);
}

[Fact]
public void MetreToMillimetre()
{
    Length metres = Length.Of(1, Si<Metre>());
    Length millimetres = metres.To(Si<Milli, Metre>());
    PrecisionIsBounded(1000d, millimetres);
}
```

---

## Assertion Helpers Reference

### `actual.Matches(expected)` / `actual.Matches(expected, precision)`

Asserts that two quantities are equal in **both value and measure** (same unit). Use when the unit system is preserved through conversion. Default precision is `FullPrecision` (16 decimal places).

Use a reduced precision parameter when conversions involve floating-point imprecision:
- `FullPrecision` (16) — exact or near-exact conversions
- `MediumPrecision` (15) — slight floating-point loss
- `LowPrecision` (14) — moderate floating-point loss
- `VeryLowPrecision` (13) — significant floating-point loss

### `PrecisionIsBounded(expectedValue, actualQuantity)`

Asserts that the quantity's numeric value matches `expectedValue` to a certain decimal precision AND that precision is bounded (fails at one higher precision). Use for conversion accuracy tests where the focus is on numeric precision rather than measure equality.

### `Assert.Equal(expected, actual)`

Asserts value equality regardless of internal measure representation. Use for definition equivalence tests where the units may differ but the physical quantity is the same.

### `FormattingMatches(factory, expectedSymbol)`

Asserts that a quantity's string representation ends with the expected unit symbol. The factory `Func<Double, {Quantity}>` creates the quantity from a value.

---

## Step-by-Step Procedure

### Step 1: Identify the new unit

Determine:
- **Unit name** and **symbol** (for the formatting test)
- **Quantity dimension** (which test file to add tests to)
- **Unit system** (SI, Metric, Imperial, NonStandard → which measure factory to use)
- **Conversion definition** (the `ToSi` transformation, for determining expected values)
- **Related units** (units derived from or related to this one, for cross-conversion tests)

### Step 2: Locate the test file

Find the corresponding `{Quantity}Test.cs` file in `source/Atmoos.Quantities.Units.Test/`. If the quantity is new and no test file exists, create one following the naming convention `{Quantity}Test.cs` with namespace `Atmoos.Quantities.Units.Test`.

### Step 3: Add required imports

Add any new `using` directives needed for the new unit's namespace at the top of the test file. Only add imports that are not already covered by `Usings.cs`.

Common patterns:
```csharp
using Atmoos.Quantities.Units.Imperial.{Quantity};
using Atmoos.Quantities.Units.NonStandard.{Quantity};
using Atmoos.Quantities.Units.Si.Metric;
using Atmoos.Quantities.Units.Si.Derived;
```

### Step 4: Generate tests

Generate tests from the applicable categories:

| Category | When to generate |
| --- | --- |
| 1. Formatting | **Always** — every unit needs a formatting test |
| 2. SI Round-Trip | **Always** for non-SI units — convert to SI and back |
| 3. Cross-System | **When** there are related units in other systems |
| 4. Definition Equivalence | **When** the unit has a well-known definition relative to another unit |
| 5. Intra-System | **When** the unit is derived from another unit in the same system |
| 6. Arithmetic | **Optional** — adds cross-unit arithmetic coverage |
| 7. Cross-Quantity | **When** the unit participates in physical laws (e.g., Force = Mass × Acceleration) |
| 8. Prefix Scaling | **When** the unit is metric/SI and supports prefixes |

### Step 5: Compute expected values

For each test, compute the expected conversion value from the unit's `ToSi` definition:

1. **Linear scaling** (`factor * self.RootedIn<T>()`): Multiply/divide by the factor.
   - Example: `Foot → Metre`: `1 ft × 3048/1e4 = 0.3048 m`
2. **Offset conversion** (`self.RootedIn<T>() + offset`): Apply offset.
   - Example: `Celsius → Kelvin`: `°C + 273.15 = K`
3. **Chained derivation** (`DerivedFrom<T>()`): Compose through the base unit.
   - Example: `Mile → Metre` via `Foot`: `5280 × 0.3048 = 1609.344 m`
4. **Use well-known reference values** from Wikipedia or standards documents when available.

### Step 6: Follow the conventions

- **Test class**: `public sealed class {Quantity}Test` (sealed, no base class)
- **Namespace**: `Atmoos.Quantities.Units.Test`
- **No constructor or setup**: All tests are independent `[Fact]` methods
- **Naming**: Descriptive method names: `{SourceUnit}To{TargetUnit}`, `Add{UnitA}To{UnitB}`, `DefinitionOf{UnitName}Holds`
- **Type aliases**: Always use `Double`, `String`, `Int32`, `Boolean` (never `double`, etc.)
- **Constants**: Use `private const Double` for conversion factors, `private static readonly` for reused quantities
- **`in` modifier**: Use for struct parameters (consistent with project style)

### Step 7: Build and run tests

Build and run the tests to verify correctness:

```bash
dotnet build source/Atmoos.Quantities.sln
dotnet test source/Atmoos.Quantities.Units.Test/
```

Fix any compilation errors or test failures before finishing.

---

## Measure Factory Quick Reference

| System | Factory | Example |
| --- | --- | --- |
| SI base | `Si<TUnit>()` | `Si<Metre>()` |
| SI with prefix | `Si<TPrefix, TUnit>()` | `Si<Kilo, Metre>()` |
| Metric | `Metric<TUnit>()` | `Metric<Hour>()` |
| Metric with prefix | `Metric<TPrefix, TUnit>()` | `Metric<Hecto, Are>()` |
| Imperial | `Imperial<TUnit>()` | `Imperial<Mile>()` |
| NonStandard | `NonStandard<TUnit>()` | `NonStandard<Knot>()` |
| Binary with prefix | `Binary<TPrefix, TUnit>()` | `Binary<Kibi, Bytes>()` |
| Square | `Square({measure})` | `Square(Si<Metre>())` |
| Cubic | `Cubic({measure})` | `Cubic(Si<Metre>())` |
| Per (quotient) | `{num}.Per({den})` | `Si<Metre>().Per(Si<Second>())` |
| Product (join) | `Join({symbol}, {left}, {right})` | Used for compound product units |

---

## Example: Complete Test Set for a New Imperial Length Unit

Suppose a new unit `Fathom` is added to `Atmoos.Quantities.Units.Imperial.Length` with:
- Symbol: `"ftm"`
- Definition: 1 fathom = 2 yards = 6 feet
- Conversion: `6 * self.DerivedFrom<Foot>()`

The following tests would be generated in `LengthTest.cs`:

```csharp
// Category 1: Formatting
[Fact]
public void FathomToString() => FormattingMatches(v => Length.Of(v, Imperial<Fathom>()), "ftm");

// Category 2: SI Round-Trip (to SI)
[Fact]
public void FathomToMetre()
{
    Length fathom = Length.Of(1, Imperial<Fathom>());
    Length expected = Length.Of(1.8288, Si<Metre>());

    Length actual = fathom.To(Si<Metre>());

    actual.Matches(expected);
}

// Category 2: SI Round-Trip (from SI)
[Fact]
public void MetreToFathom()
{
    Length metres = Length.Of(1.8288, Si<Metre>());
    Length expected = Length.Of(1, Imperial<Fathom>());

    Length actual = metres.To(Imperial<Fathom>());

    actual.Matches(expected);
}

// Category 4: Definition Equivalence
[Fact]
public void DefinitionOfFathomInFeet()
{
    Length fathom = Length.Of(1, Imperial<Fathom>());
    Length feet = Length.Of(6, Imperial<Foot>());

    Assert.Equal(fathom, feet);
}

// Category 5: Intra-System Conversion
[Fact]
public void FathomToYard()
{
    Length fathom = Length.Of(1, Imperial<Fathom>());
    Length expected = Length.Of(2, Imperial<Yard>());

    Length actual = fathom.To(Imperial<Yard>());

    actual.Matches(expected);
}

// Category 3: Cross-System Conversion
[Fact]
public void FathomToKilometre()
{
    Length fathoms = Length.Of(1000, Imperial<Fathom>());
    Length expected = Length.Of(1.8288, Si<Kilo, Metre>());

    Length actual = fathoms.To(Si<Kilo, Metre>());

    actual.Matches(expected);
}
```

---

## Example: Complete Test Set for a New Metric Area Unit

Suppose a new unit `Barn` is added to `Atmoos.Quantities.Units.Si.Metric` with:
- Symbol: `"b"`
- Definition: 1 barn = 1e-28 m²
- Implements: `IMetricUnit, IArea, IPowerOf<ILength>`

The following tests would be generated in `AreaTest.cs`:

```csharp
// Category 1: Formatting
[Fact]
public void BarnToString() => FormattingMatches(v => Area.Of(v, Metric<Barn>()), "b");

// Category 2: SI Round-Trip
[Fact]
public void BarnToSquareMetre()
{
    Area barn = Area.Of(1e28, Metric<Barn>());
    Area expected = Area.Of(1, Square(Si<Metre>()));

    Area actual = barn.To(Square(Si<Metre>()));

    actual.Matches(expected);
}

// Category 2: SI Round-Trip (reverse)
[Fact]
public void SquareMetreToBarn()
{
    Area squareMetre = Area.Of(1, Square(Si<Metre>()));
    Area expected = Area.Of(1e28, Metric<Barn>());

    Area actual = squareMetre.To(Metric<Barn>());

    actual.Matches(expected);
}

// Category 8: Prefix Scaling
[Fact]
public void MegaBarnToBarn()
{
    Area megaBarn = Area.Of(1, Metric<Mega, Barn>());
    Area expected = Area.Of(1e6, Metric<Barn>());

    Area actual = megaBarn.To(Metric<Barn>());

    actual.Matches(expected);
}
```

---

## Important Conventions

1. **File-per-dimension**: All tests for units of the same dimension go in the same `{Quantity}Test.cs` file.
2. **Sealed test class**: `public sealed class {Quantity}Test`.
3. **No test base class**: Tests are standalone `[Fact]` methods.
4. **Namespace**: Always `Atmoos.Quantities.Units.Test`.
5. **Exact values**: Use exact conversion factors when possible; use well-known reference values from standards.
6. **Precision parameter**: Omit precision in `Matches()` for exact conversions; use `MediumPrecision`, `LowPrecision`, or `VeryLowPrecision` when floating-point loss is expected.
7. **`PrecisionIsBounded`**: Use for tests focused on numeric precision of a single conversion.
8. **`Matches()`**: Use for tests that verify both value and measure are preserved.
9. **`Assert.Equal()`**: Use for definition tests where only value equality matters.
10. **Use `.NET type aliases`**: `Double`, `String`, `Int32`, `Boolean`.
11. **Add `using` directives**: Only for unit namespaces not covered by `Usings.cs`.
12. **AI-generated files**: If creating an entirely new test file, use the `.ai.` infix: `{Quantity}Test.ai.cs`.
