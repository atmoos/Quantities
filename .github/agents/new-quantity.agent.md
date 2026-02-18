---
name: New Quantity
description: Generate a new physical quantity type for the Atmoos.Quantities library.
tools: ['edit/editFiles', 'search/codebase', 'read/terminalLastCommand', 'web/fetch']
---

# New Quantity Generation Agent

Generate a new physical quantity type for the Atmoos.Quantities library.

## Instructions

When asked to create a new quantity, follow these steps:

1. **Identify the quantity category** (see below) based on its SI dimensional analysis.
2. **Create or verify the dimension interface** in the appropriate dimensions file.
3. **Create the quantity struct** in `source/Atmoos.Quantities/Quantities/`.
4. **Add cross-quantity operators** in the appropriate file under `source/Atmoos.Quantities/Physics/`.

Always adhere to the coding conventions defined in `.github/copilot-instructions.md`.

---

## Quantity Categories

Every quantity falls into one of these categories, determined by its SI dimensional formula:

### 1. Scalar (base quantity with a single dimension)

**When**: The quantity is an SI base quantity or is independently defined with its own linear dimension.

**Dimension**: `ILinear<TSelf>, IBaseQuantity` (for base) or `ILinear<TSelf>, IDerivedQuantity` (for derived).

**Struct implements**: `IQuantity<T>, IDimension, IScalar<T, IDimension>`

**Examples**: `Length` (ILength), `Time` (ITime), `Mass` (IMass), `ElectricCurrent`, `Power` (IPower), `ElectricPotential`, `ElectricalResistance`

**Template** (using `Length` as canonical example):

```csharp
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct {Name} : IQuantity<{Name}>, I{Name}, IScalar<{Name}, I{Name}>
{
    private readonly Quantity {fieldName};
    internal Quantity Value => this.{fieldName};
    Quantity IQuantity<{Name}>.Value => this.{fieldName};

    private {Name}(in Quantity value) => this.{fieldName} = value;

    public {Name} To<TUnit>(in Scalar<TUnit> other)
        where TUnit : I{Name}, IUnit => new(other.Transform(in this.{fieldName}));

    public static {Name} Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : I{Name}, IUnit => new(measure.Create(in value));

    static {Name} IFactory<{Name}>.Create(in Quantity value) => new(in value);

    public Boolean Equals({Name} other) => this.{fieldName}.Equals(other.{fieldName});

    public override Boolean Equals(Object? obj) => obj is {Name} {fieldName} && Equals({fieldName});

    public override Int32 GetHashCode() => this.{fieldName}.GetHashCode();

    public override String ToString() => this.{fieldName}.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.{fieldName}.ToString(format, provider);
}
```

**Notes**:
- The constructor is `private` for scalar quantities.
- There is no `using` for `Atmoos.Quantities.Core.Numerics` (not needed).
- `{fieldName}` is the camelCase version of `{Name}`.

---

### 2. PowerOf (quantity that is a power of a linear dimension)

**When**: The quantity's dimension is a single base dimension raised to a power > 1.

**Dimension**: `IDimension<TLinear, TExponent>, IDerivedQuantity` (e.g., `IDimension<ILength, Two>` for Area).

**Struct implements**: `IQuantity<T>, IDimension, IPowerOf<T, IDimension, ILinear, TExponent>`

**Examples**: `Area` (Length²), `Volume` (Length³)

**Template** (using `Area` as canonical example):

```csharp
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct {Name} : IQuantity<{Name}>, I{Name}, IPowerOf<{Name}, I{Name}, I{Linear}, {Exponent}>
{
    private readonly Quantity {fieldName};
    internal Quantity Value => this.{fieldName};
    Quantity IQuantity<{Name}>.Value => this.{fieldName};

    private {Name}(in Quantity value) => this.{fieldName} = value;

    public {Name} To<TUnit>(in Power<TUnit, {Exponent}> other)
        where TUnit : I{Linear}, IUnit => new(other.Transform(in this.{fieldName}));

    public {Name} To<TAlias>(in Scalar<TAlias> other)
        where TAlias : I{Name}, IPowerOf<I{Linear}>, IUnit => new(other.Transform(in this.{fieldName}, static f => ref f.AliasOf<TAlias, I{Linear}>()));

    public static {Name} Of<TUnit>(in Double value, in Power<TUnit, {Exponent}> measure)
        where TUnit : I{Linear}, IUnit => new(measure.Create(in value));

    public static {Name} Of<TAlias>(in Double value, in Scalar<TAlias> measure)
        where TAlias : I{Name}, IPowerOf<I{Linear}>, IUnit => new(measure.Create(in value, static f => ref f.AliasOf<TAlias, I{Linear}>()));

    static {Name} IFactory<{Name}>.Create(in Quantity value) => new(in value);

    public Boolean Equals({Name} other) => this.{fieldName}.Equals(other.{fieldName});

    public override Boolean Equals(Object? obj) => obj is {Name} {localVar} && Equals({localVar});

    public override Int32 GetHashCode() => this.{fieldName}.GetHashCode();

    public override String ToString() => this.{fieldName}.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.{fieldName}.ToString(format, provider);
}
```

**Notes**:
- Requires `using Atmoos.Quantities.Core.Numerics;` for the exponent type (`Two`, `Three`, etc.).
- Has two overloads each for `To` and `Of`: one for the power form (e.g., `m²`) and one for alias units (e.g., `Litre` for Volume).
- The constructor is `private`.

---

### 3. Quotient (quantity defined as a ratio of two dimensions)

**When**: The quantity is dimensionally a ratio of two base/derived dimensions: `Nominator / Denominator`.

**Dimension**: `IProduct<TNominator, IDimension<TDenominator, Negative<One>>>` (e.g., Velocity = Length / Time).

**Struct implements**: `IQuantity<T>, IDimension, IQuotient<T, IDimension, INomDimension, IDenomDimension>`

**Examples**: `Velocity` (Length/Time), `DataRate` (AmountOfInformation/Time)

**Template** (using `Velocity` as canonical example):

```csharp
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct {Name} : IQuantity<{Name}>, I{Name}, IQuotient<{Name}, I{Name}, I{Nominator}, I{Denominator}>
{
    private readonly Quantity {fieldName};
    internal Quantity Value => this.{fieldName};
    Quantity IQuantity<{Name}>.Value => this.{fieldName};

    internal {Name}(in Quantity value) => this.{fieldName} = value;

    public {Name} To<TUnit>(in Scalar<TUnit> other)
        where TUnit : I{Name}, IUnit => new(other.Transform(in this.{fieldName}));

    public {Name} To<TNominator, TDenominator>(in Quotient<TNominator, TDenominator> other)
        where TNominator : I{Nominator}, IUnit
        where TDenominator : I{Denominator}, IUnit => new(other.Transform(in this.{fieldName}));

    public static {Name} Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : I{Name}, IUnit => new(measure.Create(in value));

    public static {Name} Of<TNominator, TDenominator>(in Double value, in Quotient<TNominator, TDenominator> measure)
        where TNominator : IUnit, I{Nominator}
        where TDenominator : IUnit, I{Denominator} => new(measure.Create(in value));

    static {Name} IFactory<{Name}>.Create(in Quantity value) => new(in value);

    public Boolean Equals({Name} other) => this.{fieldName}.Equals(other.{fieldName});

    public override Boolean Equals(Object? obj) => obj is {Name} {fieldName} && Equals({fieldName});

    public override Int32 GetHashCode() => this.{fieldName}.GetHashCode();

    public override String ToString() => this.{fieldName}.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.{fieldName}.ToString(format, provider);
}
```

**Notes**:
- The constructor is `internal` (not `private`) for quotient quantities, since cross-quantity operators in the `Physics` namespace need to create instances.
- Has two overloads each for `To` and `Of`: one for a scalar alias unit and one for the quotient form.

---

### 4. Quotient with powered denominator

**When**: The quantity is a ratio where the denominator has an exponent > 1.

**Dimension**: `IProduct<TNominator, IDimension<TDenominator, Negative<TExponent>>>`.

**Struct implements**: `IQuantity<T>, IDimension, IQuotient<T, IDimension, INomDimension, IDenomDimension, TExponent>`

**Examples**: `Acceleration` (Length/Time²), `Pressure` (Force/Length²)

**Template** (using `Acceleration` as canonical example):

```csharp
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct {Name} : IQuantity<{Name}>, I{Name}, IQuotient<{Name}, I{Name}, I{Nominator}, I{Denominator}, {Exponent}>
{
    private readonly Quantity {fieldName};
    internal Quantity Value => this.{fieldName};
    Quantity IQuantity<{Name}>.Value => this.{fieldName};

    internal {Name}(in Quantity value) => this.{fieldName} = value;

    public {Name} To<TUnit>(in Scalar<TUnit> other)
        where TUnit : I{Name}, IUnit => new(other.Transform(in this.{fieldName}));

    public {Name} To<TNominator, TDenominator>(in Quotient<TNominator, Power<TDenominator, {Exponent}>> other)
        where TNominator : I{Nominator}, IUnit
        where TDenominator : I{Denominator}, IUnit => new(other.Transform(in this.{fieldName}));

    public static {Name} Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : I{Name}, IUnit => new(measure.Create(in value));

    public static {Name} Of<TNominator, TDenominator>(in Double value, in Quotient<TNominator, Power<TDenominator, {Exponent}>> measure)
        where TNominator : IUnit, I{Nominator}
        where TDenominator : IUnit, I{Denominator} => new(measure.Create(in value));

    static {Name} IFactory<{Name}>.Create(in Quantity value) => new(in value);

    public Boolean Equals({Name} other) => this.{fieldName}.Equals(other.{fieldName});

    public override Boolean Equals(Object? obj) => obj is {Name} {fieldName} && Equals({fieldName});

    public override Int32 GetHashCode() => this.{fieldName}.GetHashCode();

    public override String ToString() => this.{fieldName}.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.{fieldName}.ToString(format, provider);
}
```

**Notes**:
- Requires `using Atmoos.Quantities.Core.Numerics;` for `Two`, `Three`, etc.
- The quotient form uses `Power<TDenominator, {Exponent}>` in both `To` and `Of`.
- The constructor is `internal`.

---

### 5. Product (quantity defined as a product of two dimensions)

**When**: The quantity is dimensionally a product of two dimensions.

**Dimension**: `IProduct<TLeft, TRight>, IDerivedQuantity`.

**Struct implements**: `IQuantity<T>, IDimension, IProduct<T, IDimension, ILeftDimension, IRightDimension>`

**Examples**: `Energy` (Power × Time)

**Template** (using `Energy` as canonical example):

```csharp
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct {Name} : IQuantity<{Name}>, I{Name}, IProduct<{Name}, I{Name}, I{Left}, I{Right}>
{
    private readonly Quantity {fieldName};
    internal Quantity Value => this.{fieldName};
    Quantity IQuantity<{Name}>.Value => this.{fieldName};

    private {Name}(in Quantity value) => this.{fieldName} = value;

    public {Name} To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IUnit, I{Name} => new(other.Transform(in this.{fieldName}));

    public {Name} To<TLeft, TRight>(in Product<TLeft, TRight> other)
        where TLeft : IUnit, I{Left}
        where TRight : IUnit, I{Right} => new(other.Transform(in this.{fieldName}));

    public static {Name} Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IUnit, I{Name} => new(measure.Create(in value));

    public static {Name} Of<TLeft, TRight>(in Double value, in Product<TLeft, TRight> measure)
        where TLeft : IUnit, I{Left}
        where TRight : IUnit, I{Right} => new(measure.Create(in value));

    static {Name} IFactory<{Name}>.Create(in Quantity value) => new(in value);

    public Boolean Equals({Name} other) => this.{fieldName}.Equals(other.{fieldName});

    public override Boolean Equals(Object? obj) => obj is {Name} {fieldName} && Equals({fieldName});

    public override Int32 GetHashCode() => this.{fieldName}.GetHashCode();

    public override String ToString() => this.{fieldName}.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.{fieldName}.ToString(format, provider);
}
```

**Notes**:
- The constructor is `private` for product quantities.

---

### 6. Invertible (quantity that is the inverse of a base dimension)

**When**: The quantity is dimensionally the inverse of a single base dimension (exponent = -1).

**Dimension**: `IDimension<TInverse, Negative<One>>, ILinear, IDerivedQuantity`.

**Struct implements**: `IQuantity<T>, IDimension, IInvertible<T, IDimension, IInverse>`

**Examples**: `Frequency` (1/Time)

**Template** (using `Frequency` as canonical example):

```csharp
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct {Name} : IQuantity<{Name}>, I{Name}, IInvertible<{Name}, I{Name}, I{Inverse}>
{
    private readonly Quantity {fieldName};
    internal Quantity Value => this.{fieldName};
    Quantity IQuantity<{Name}>.Value => this.{fieldName};

    private {Name}(in Quantity value) => this.{fieldName} = value;

    public {Name} To<TUnit>(in Creation.Scalar<TUnit> other)
        where TUnit : I{Name}, IInvertible<I{Inverse}>, IUnit => new(other.Transform(in this.{fieldName}, static f => ref f.InverseOf<TUnit, I{Inverse}>()));

    public static {Name} Of<TUnit>(in Double value, in Creation.Scalar<TUnit> measure)
        where TUnit : I{Name}, IInvertible<I{Inverse}>, IUnit => new(measure.Create(in value, static f => ref f.InverseOf<TUnit, I{Inverse}>()));

    static {Name} IFactory<{Name}>.Create(in Quantity value) => new(in value);

    public Boolean Equals({Name} other) => this.{fieldName}.Equals(other.{fieldName});

    public override Boolean Equals(Object? obj) => obj is {Name} {fieldName} && Equals({fieldName});

    public override Int32 GetHashCode() => this.{fieldName}.GetHashCode();

    public override String ToString() => this.{fieldName}.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.{fieldName}.ToString(format, provider);
}
```

**Notes**:
- Uses `Creation.Scalar<TUnit>` (qualified) to avoid conflict with the struct name.
- The `To` and `Of` methods constrain `TUnit : IInvertible<I{Inverse}>` additionally.
- Uses `InverseOf` on the factory instead of `AliasOf`.
- The constructor is `private`.

---

## Step-by-Step Procedure

### Step 1: Determine the dimensional formula

Analyse the requested quantity using SI dimensional analysis. Express it in terms of existing base dimensions: `ILength`, `ITime`, `IMass`, `IElectricCurrent`, `ITemperature`, `IAmountOfSubstance`, `ILuminousIntensity`, or existing derived dimensions: `IArea`, `IVolume`, `IVelocity`, `IAcceleration`, `IForce`, `IPower`, `IEnergy`, `IFrequency`, `IPressure`, `IElectricPotential`, `IElectricalResistance`, `IAmountOfInformation`, `IInformationRate`.

### Step 2: Create or verify the dimension interface

If a matching dimension interface does not already exist:

- **Base quantities** → add to `source/Atmoos.Quantities/Dimensions/BaseDimensions.cs`
- **Derived quantities** → add to `source/Atmoos.Quantities/Dimensions/DerivedDimensions.cs`
- **Electrical quantities** → add to `source/Atmoos.Quantities/Dimensions/ElectricalDimesions.cs`

Use the existing patterns:

```csharp
// Base: linear, independent dimension
public interface IMyQuantity : ILinear<IMyQuantity>, IBaseQuantity;

// Derived: power of a base dimension
public interface IMyQuantity : IDimension<ILength, Two>, IDerivedQuantity;

// Derived: product of dimensions (used for quotients too)
public interface IMyQuantity : IProduct<ILength, IDimension<ITime, Negative<One>>>, IDerivedQuantity;
```

### Step 3: Create the quantity struct

Create the file `source/Atmoos.Quantities/Quantities/{Name}.cs` using the appropriate template from the categories above.

### Step 4: Add cross-quantity operators

Add operators that relate the new quantity to existing quantities according to SI rules. Place them in the appropriate file under `source/Atmoos.Quantities/Physics/`:

- `MechanicalEngineering.cs` → geometry (Length, Area, Volume) and kinematics (Velocity, Acceleration, Force, Energy, Power, Pressure)
- `ElectricalEngineering.cs` → electrical quantities (Ohm's law, Power laws)
- `ComputerScience.cs` → data and information rate
- `Generic.cs` → frequency/time inversion and other dimensionless relations

Use the extension method pattern:

```csharp
using static Atmoos.Quantities.Extensions;

// Inside appropriate static class:
extension({QuantityType})
{
    public static {ResultType} operator *|/(in {QuantityType} left, in {OtherType} right) =>
        Create<{ResultType}>(left.Value *|/ right.Value);
}
```

Key rules for operator placement:
- For `A = B * C`, define `operator *` on **both** `B` and `C` (commutativity).
- For `A = B / C`, define `operator /` on `B`.
- For each product `A = B * C`, also define the inverse divisions: `B = A / C` and `C = A / B` on `A`.

### Step 5: Verify

Build the solution to ensure everything compiles:

```
dotnet build source/Atmoos.Quantities.sln
```

---

## Quick Reference: Existing Dimension-Quantity Mappings

| Quantity              | Dimension Interface  | Category                   | SI Formula           |
| --------------------- | -------------------- | -------------------------- | -------------------- |
| Length                 | ILength              | Scalar (base)              | L                    |
| Time                  | ITime                | Scalar (base)              | T                    |
| Mass                  | IMass                | Scalar (base)              | M                    |
| ElectricCurrent        | IElectricCurrent     | Scalar (base)              | I                    |
| Temperature           | ITemperature         | Scalar (base)              | Θ                    |
| Area                  | IArea                | PowerOf(ILength, Two)      | L²                   |
| Volume                | IVolume              | PowerOf(ILength, Three)    | L³                   |
| Velocity              | IVelocity            | Quotient(ILength, ITime)   | L·T⁻¹               |
| Acceleration          | IAcceleration        | Quotient(ILength, ITime²)  | L·T⁻²               |
| Force                 | IForce               | Scalar (derived)           | M·L·T⁻²             |
| Power                 | IPower               | Scalar (derived)           | M·L²·T⁻³            |
| Energy                | IEnergy              | Product(IPower, ITime)     | M·L²·T⁻²            |
| Pressure              | IPressure            | Quotient(IForce, ILength²) | M·L⁻¹·T⁻²           |
| Frequency             | IFrequency           | Invertible(ITime)          | T⁻¹                  |
| ElectricPotential     | IElectricPotential   | Scalar (derived)           | M·L²·T⁻³·I⁻¹        |
| ElectricalResistance  | IElectricalResistance| Scalar (derived)           | M·L²·T⁻³·I⁻²        |
| Data                  | IAmountOfInformation | Scalar (derived)           | (information)        |
| DataRate              | IInformationRate     | Quotient(IAmountOfInformation, ITime) | information·T⁻¹ |

---

## Important Conventions

1. **Namespace**: All quantity structs live in `namespace Atmoos.Quantities;` (not a sub-namespace).
2. **File location**: All quantity struct files are in `source/Atmoos.Quantities/Quantities/`.
3. **Field naming**: The private field is always the camelCase of the quantity name (e.g., `velocity` for `Velocity`).
4. **Constructor access**: `private` for scalar, power-of, product, and invertible quantities; `internal` for quotient quantities.
5. **.NET type aliases**: Always use `Double`, `Boolean`, `Int32`, `String`, `Object` — never `double`, `bool`, `int`, `string`, `object`.
6. **`in` modifier**: Use `in` for all struct parameters consistently.
7. **Cross-quantity operators** use `Create<TQuantity>(...)` from `Extensions` via `using static Atmoos.Quantities.Extensions;`.
8. **Operators class**: Do NOT modify `Operators.cs` — it automatically provides `==`, `!=`, `>`, `>=`, `<`, `<=`, `+`, `-`, `*`, `/` for all `IQuantity<T>` types via extensions.
