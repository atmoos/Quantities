---
name: New Unit
description: Generate a new unit of measurement for the Atmoos.Quantities library.
tools: ['edit/editFiles', 'edit/createDirectory', 'edit/createFile', 'search/codebase', 'read/terminalLastCommand', 'web/fetch', 'execute/runInTerminal', 'microsoftdocs/mcp/*','read/problems']
---

# New Unit Generation Agent

Generate a new unit of measurement for the Atmoos.Quantities library.

## Instructions

When asked to create a new unit, follow these steps:

1. **Identify the unit system** (SI base, SI derived, Metric, Imperial, or NonStandard).
2. **Identify the quantity dimension** the unit measures (e.g., `ILength`, `IMass`, `ITime`, `IPower`).
3. **Determine the conversion** to the SI base unit for that dimension.
4. **Create the unit struct** in the correct project and namespace.
5. **Build and verify** the solution compiles.

Always adhere to the coding conventions defined in `.github/copilot-instructions.md`.

---

## Unit Systems

Every unit belongs to one of five systems, determined by its marker interface:

### 1. SI Base Unit (`ISiUnit`)

**When**: The unit is one of the seven SI base units (Metre, Second, Kilogram, Kelvin, Ampere, Mole, Candela) or an SI derived unit that serves as the canonical reference for its dimension (Newton, Watt, Joule, Pascal, Volt, Ohm).

**Key property**: SI base/derived units do **not** implement `ITransform` — they define the reference point. Their `ToSi` transformation is implicitly the identity.

**Project**: `Atmoos.Quantities` (core) for the seven base units. `Atmoos.Quantities.Units` for derived units (Newton, Watt, etc.).

**Namespace**: `Atmoos.Quantities.Units.Si` (base) or `Atmoos.Quantities.Units.Si.Derived` (derived).

**Template**:

```csharp
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si; // or .Derived for derived units

// See: https://en.wikipedia.org/wiki/{UnitName}
public readonly struct {Name} : ISiUnit, I{Dimension}
{
    public static String Representation => "{symbol}";
}
```

**Examples**: `Metre`, `Second`, `Kilogram`, `Newton`, `Watt`, `Joule`, `Pascal`, `Volt`, `Ohm`

---

### 2. Metric Unit (`IMetricUnit`)

**When**: The unit is accepted by the SI system but is not itself an SI unit. Metric units support SI metric prefixes (`Kilo`, `Mega`, `Milli`, etc.).

**Key property**: Must implement `ITransform` via `ToSi(Transformation)`. Support metric prefixes by default.

**Project**: `Atmoos.Quantities` (core) for fundamental metric units (Hour, Minute, Litre, Celsius). `Atmoos.Quantities.Units` for additional metric units.

**Namespace**: `Atmoos.Quantities.Units.Si.Metric` (optionally with a sub-namespace for grouping, e.g., `UnitsOfInformation`).

**Template** (simple linear conversion):

```csharp
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Metric;

// See: https://en.wikipedia.org/wiki/{UnitName}
public readonly struct {Name} : IMetricUnit, I{Dimension}
{
    public static Transformation ToSi(Transformation self) => {factor} * self.RootedIn<{SiBaseUnit}>();

    public static String Representation => "{symbol}";
}
```

**Examples**: `Hour`, `Minute`, `Gram`, `Tonne`, `Day`, `Week`, `Are`, `Bar`, `AstronomicalUnit`, `Ångström`, `HorsePower`, `Bit`, `Byte`

---

### 3. Imperial Unit (`IImperialUnit`)

**When**: The unit belongs to the British imperial measurement system.

**Key property**: Must implement `ITransform` via `ToSi(Transformation)`.

**Project**: `Atmoos.Quantities` (core) for foundational imperial units (Foot, Inch, Mile, Pound, Fahrenheit, Pint). `Atmoos.Quantities.Units` for additional imperial units.

**Namespace**: `Atmoos.Quantities.Units.Imperial.{Quantity}` (grouped by quantity, e.g., `Imperial.Length`, `Imperial.Mass`).

**Template**:

```csharp
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Imperial.{Quantity};

// See: https://en.wikipedia.org/wiki/{UnitName}
public readonly struct {Name} : IImperialUnit, I{Dimension}
{
    public static Transformation ToSi(Transformation self) => {conversion expression};

    public static String Representation => "{symbol}";
}
```

**Examples**: `Foot`, `Inch`, `Mile`, `Yard`, `Pound`, `Ounce`, `Fahrenheit`, `Acre`, `Gallon`, `PoundForce`

---

### 4. NonStandard Unit (`INonStandardUnit`)

**When**: The unit does not belong to any recognised system (not SI, not metric, not imperial).

**Key property**: Must implement `ITransform` via `ToSi(Transformation)`.

**Project**: `Atmoos.Quantities.Units`.

**Namespace**: `Atmoos.Quantities.Units.NonStandard.{Quantity}` (grouped by quantity).

**Template**:

```csharp
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.NonStandard.{Quantity};

// See: https://en.wikipedia.org/wiki/{UnitName}
public readonly struct {Name} : INonStandardUnit, I{Dimension}
{
    public static Transformation ToSi(Transformation self) => {conversion expression};

    public static String Representation => "{symbol}";
}
```

**Examples**: `NauticalMile`, `LightYear`, `Knot`, `StandardAtmosphere`, `Torr`, `Delisle`, `Morgen`, `Pfund`

---

## Special Unit Patterns

### Alias Units for Power-of Dimensions (Area, Volume)

**When**: The unit measures a power-of dimension (e.g., area = length², volume = length³) but has its own name rather than being expressed as a power of a length unit (e.g., Acre, Litre, Are).

**Additional interface**: Must also implement `IPowerOf<I{Linear}>` and explicitly implement `ISystemInject<I{Linear}>.Inject<T>()`.

**Template**:

```csharp
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.{SystemPath}; // for the length unit used in Inject

namespace Atmoos.Quantities.Units.{System}.{Quantity};

// See: https://en.wikipedia.org/wiki/{UnitName}
public readonly struct {Name} : I{SystemMarker}, I{Dimension}, IPowerOf<I{Linear}>
{
    public static Transformation ToSi(Transformation self) => {conversion expression};

    static T ISystemInject<I{Linear}>.Inject<T>(ISystems<I{Linear}, T> basis) => basis.{System}<{LengthUnit}>();

    public static String Representation => "{symbol}";
}
```

The `Inject` method specifies which length unit to use for display purposes when deconstructing. Examples:
- `basis.Si<Metre>()` for metric/SI area/volume units
- `basis.Imperial<Foot>()` or `basis.Imperial<Inch>()` for imperial volume units
- `basis.Imperial<Yard>()` for imperial area units like Acre

**Examples**: `Litre`, `Lambda`, `Stere`, `Are`, `Acre`, `Perch`, `Rood`, `Morgen`, `Pint`, `Gallon`, `FluidOunce`

---

### Invertible Units

**When**: The unit measures a quantity that is the inverse of a base dimension (e.g., Hertz = 1/Time).

**Additional interface**: Must implement `IInvertible<I{Inverse}>` and explicitly implement `ISystemInject<I{Inverse}>.Inject<T>()`.

**Template**:

```csharp
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived;

public readonly struct {Name} : ISiUnit, I{Dimension}, IInvertible<I{Inverse}>
{
    public static Transformation ToSi(Transformation self) => self;

    static T ISystemInject<I{Inverse}>.Inject<T>(ISystems<I{Inverse}, T> basis) => basis.Si<{InverseUnit}>();

    public static String Representation => "{symbol}";
}
```

**Examples**: `Hertz` (frequency, inverse of `ITime`, injects `Second`)

---

### Compound Quantity Units (Velocity, DataRate, etc.)

**When**: The unit measures a compound quantity (quotient, product) using a single named unit rather than a compound expression.

The unit simply implements the compound dimension interface and provides its `ToSi` conversion. No special additional interfaces are needed beyond the standard pattern.

**Template**:

```csharp
using Atmoos.Quantities.Dimensions;
using static Atmoos.Quantities.Extensions;

namespace Atmoos.Quantities.Units.{System}.{Quantity};

// See: https://en.wikipedia.org/wiki/{UnitName}
public readonly struct {Name} : I{SystemMarker}, I{Dimension}
{
    public static Transformation ToSi(Transformation self) => {conversion expression};

    public static String Representation => "{symbol}";
}
```

For compound units, the conversion often involves `ValueOf<T>()` to incorporate the conversion factor of a related unit.

**Examples**: `Knot` (velocity: `self.DerivedFrom<NauticalMile>() / ValueOf<Hour>()`)

---

## Conversion Expressions

The `ToSi` method defines how to convert from the new unit to the SI reference. Three key helpers are available:

### `RootedIn<TSi>()`

Use when defining conversion **directly relative to the SI base/derived unit** for the dimension. The constraint is `TSi : ISiUnit`. This is a no-op that documents the SI anchor.

```csharp
// Foot → Metre (SI base for length)
public static Transformation ToSi(Transformation self) => 3048 * self.RootedIn<Metre>() / 1e4;

// Celsius → Kelvin (SI base for temperature), with offset
public static Transformation ToSi(Transformation self) => self.RootedIn<Kelvin>() + 273.15d;

// Gram → Kilogram (SI base for mass)
public static Transformation ToSi(Transformation self) => self.RootedIn<Kilogram>() / 1000;
```

### `DerivedFrom<TBasis>()`

Use when defining conversion **relative to another non-SI unit**. This chains through `TBasis.ToSi()`, composing transformations. The constraint is `TBasis : IUnit, ITransform`.

```csharp
// Mile derived from Foot: 1 mi = 5280 ft
public static Transformation ToSi(Transformation self) => 5280 * self.DerivedFrom<Foot>();

// Inch derived from Foot: 1 ft = 12 in
public static Transformation ToSi(Transformation self) => self.DerivedFrom<Foot>() / 12;

// Day derived from Hour: 1 d = 24 h
public static Transformation ToSi(Transformation self) => 24 * self.DerivedFrom<Hour>();

// Week derived from Day: 1 w = 7 d
public static Transformation ToSi(Transformation self) => 7 * self.DerivedFrom<Day>();

// Torr derived from StandardAtmosphere: 1 atm = 760 Torr
public static Transformation ToSi(Transformation self) => self.DerivedFrom<StandardAtmosphere>() / 760;
```

### `ValueOf<T>()`

Use when a compound quantity unit needs the numeric conversion factor of another unit. Returns the evaluated polynomial at value 1.0, optionally raised to an exponent.

```csharp
// Knot = NauticalMile per Hour
public static Transformation ToSi(Transformation self) => self.DerivedFrom<NauticalMile>() / ValueOf<Hour>();
```

### `FusedMultiplyAdd(multiplicand, addend)`

Use for complex affine transformations that combine multiplication and addition efficiently:

```csharp
// GasMark: [K] = [GM] × 125/9 + (5 × 218 + 9 × 273.15) / 9
public static Transformation ToSi(Transformation self) => self.FusedMultiplyAdd(125, 5d * 218d + 9 * 273.15d) / 9;
```

### Conversion Guidelines

1. **Pure scaling** (no offset): Use multiplication and division operators.
   - `factor * self.RootedIn<T>()` for scaling up from the new unit
   - `self.RootedIn<T>() / factor` for scaling down
2. **Offset conversions** (e.g., temperature): Use `+` and `-` operators after scaling.
   - `self.RootedIn<Kelvin>() + 273.15d` for Celsius
3. **Chained conversions**: Use `DerivedFrom<T>()` to build on existing unit conversions.
4. **Keep conversions exact**: Use integer arithmetic or exact decimal fractions where possible.
5. **Internal constants**: Use `internal const` fields for conversion factors that may be reused.
6. **Preserve numerator/denominator separation**: The `Polynomial` type stores nominator and denominator separately. Structure conversions so that irrational or large factors remain in the numerator and integer divisors remain in the denominator, rather than pre-computing their quotient as a single `Double`. This yields higher precision in round-trip conversions.
   - ✅ `Math.PI * self.RootedIn<Radian>() / 180` — stores π as nominator, 180 as denominator.
   - ❌ `Math.PI / 180 * self.RootedIn<Radian>()` — pre-computes π÷180 into a single `Double`, losing precision.
   - Note: cross-unit conversions that chain two irrational factors (e.g. Turn → Gradian, both via π) may still exhibit IEEE 754 limits and require `MediumPrecision` in test assertions.

---

## Step-by-Step Procedure

### Step 1: Determine the unit system and dimension

Identify:
- **System**: Is it SI, Metric, Imperial, or NonStandard?
- **Dimension**: What physical quantity does it measure? Map to the existing dimension interface (e.g., `ILength`, `IMass`, `ITime`, `IPower`, `IArea`, `IVelocity`).
- **Special traits**: Is it an alias for a power-of dimension (`IPowerOf`)? Is it invertible (`IInvertible`)? Is it a compound quantity unit?

### Step 2: Determine the conversion to SI

Research the exact conversion factor or formula:
- Find the relationship to the SI reference unit for the dimension.
- Decide whether to use `RootedIn<T>()` (direct SI reference) or `DerivedFrom<T>()` (chained through another unit).
- For temperature-like units with offsets, include the offset in the transformation.

### Step 3: Choose the correct project

- **`Atmoos.Quantities` (core project)**: Only for the foundational units that the core library depends on. These are the SI base units (Metre, Second, Kilogram, Kelvin, Ampere, Candela, Mole), plus a small set of essential metric units (Hour, Minute, Litre, Celsius) and foundational imperial units (Foot, Inch, Mile, Pound, Fahrenheit, Pint).
- **`Atmoos.Quantities.Units`**: For all other units. This is where new units should almost always go.

### Step 4: Create the unit struct

Create the file in the correct location:

| System      | Path Pattern                                                                      |
| ----------- | --------------------------------------------------------------------------------- |
| SI base     | `source/Atmoos.Quantities/Units/Si/{Name}.cs`                                    |
| SI derived  | `source/Atmoos.Quantities.Units/Si/Derived/{Name}.cs`                            |
| Metric      | `source/Atmoos.Quantities.Units/Si/Metric/{Name}.cs`                             |
| Imperial    | `source/Atmoos.Quantities.Units/Imperial/{Quantity}/{Name}.cs`                   |
| NonStandard | `source/Atmoos.Quantities.Units/NonStandard/{Quantity}/{Name}.cs`                |

Use the appropriate template from the unit system categories above. Ensure:
- The struct is `public readonly struct`.
- It implements the correct system marker interface (`ISiUnit`, `IMetricUnit`, `IImperialUnit`, or `INonStandardUnit`).
- It implements the dimension interface (e.g., `ILength`, `IMass`).
- It provides a `static String Representation` property with the standard symbol.
- For non-SI units, it implements `ToSi(Transformation)` with the correct conversion.
- Add a Wikipedia link comment for reference.

### Step 5: Verify

Build the solution to ensure everything compiles:

```
dotnet build source/Atmoos.Quantities.sln
```

---

## Quick Reference: Existing Dimension-Unit Mappings

| Dimension              | SI Reference Unit     | Dimension Interface    |
| ---------------------- | --------------------- | ---------------------- |
| Length                  | Metre                 | `ILength`              |
| Time                   | Second                | `ITime`                |
| Mass                   | Kilogram              | `IMass`                |
| Electric Current       | Ampere                | `IElectricCurrent`     |
| Temperature            | Kelvin                | `ITemperature`         |
| Amount of Substance    | Mole                  | `IAmountOfSubstance`   |
| Luminous Intensity     | Candela               | `ILuminousIntensity`   |
| Area                   | Metre² (Square)       | `IArea`                |
| Volume                 | Metre³ (Cubic)        | `IVolume`              |
| Velocity               | m/s                   | `IVelocity`            |
| Acceleration           | m/s²                  | `IAcceleration`        |
| Force                  | Newton                | `IForce`               |
| Power                  | Watt                  | `IPower`               |
| Energy                 | Joule                 | `IEnergy`              |
| Frequency              | Hertz                 | `IFrequency`           |
| Pressure               | Pascal                | `IPressure`            |
| Electric Potential     | Volt                  | `IElectricPotential`   |
| Electrical Resistance  | Ohm                   | `IElectricalResistance`|
| Amount of Information  | Bit (metric)          | `IAmountOfInformation` |
| Information Rate       | bit/s                 | `IInformationRate`     |
| Mass Flow              | kg/s                  | `IMassFlow`            |

---

## Important Conventions

1. **`readonly struct`**: All units are `public readonly struct`.
2. **Single file per unit**: Each unit gets its own `.cs` file.
3. **Namespace matches directory**: The namespace must match the file's directory path.
4. **.NET type aliases**: Always use `Double`, `String`, `Int32`, etc. — never `double`, `string`, `int`.
5. **`Representation`**: Return the standard symbol string (e.g., `"m"`, `"kg"`, `"°C"`, `"ft"`).
6. **No constructor needed**: Units are marker structs — they carry no instance state.
7. **Wikipedia references**: Add `// See: https://en.wikipedia.org/wiki/{UnitArticle}` comments.
8. **Conversion accuracy**: Prefer exact integer/rational arithmetic in `ToSi` over floating-point approximations.
9. **`using` directives**: Always include `using Atmoos.Quantities.Dimensions;`. Add other `using` directives only as needed (e.g., `using static Atmoos.Quantities.Extensions;` when using `ValueOf<T>()`).
