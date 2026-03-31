# Units — How They Work

## Directory Layout

```
Atmoos.Quantities.Units/
├── Si/                        # SI base units (Metre, Second, Kilogram, Ampere, Kelvin, Mole, Candela)
│   ├── Derived/               # SI derived units (Newton, Joule, Watt, Hertz, Pascal, Volt, Ohm, Coulomb, Radian, …)
│   └── Metric/                # Metric (accepted by SI but not themselves SI): Minute, Hour, Day, Week, Litre, Gram, Ångström, Tonne, Bar, Are, Stere, HorsePower, Lambda, …
│       └── UnitsOfInformation/
├── Imperial/                  # British imperial: Length/{Foot,Inch,Mile}, Mass/Pound, Volume/Pint, Temperature/Fahrenheit, Force/PoundForce, Power/HorsePower, Area/…
└── NonStandard/               # Units without a system: Angle/{Degree,Gradian,Turn}, Length/{NauticalMile,LightYear}, Mass/{Pfund,Zentner}, Area/Morgen, Pressure/{StandardAtmosphere,Torr}, Velocity/Knot, ElectricCharge/, Temperature/…
```

Tests live in `Atmoos.Quantities.Units.Test/` — one test class per quantity dimension, e.g. `LengthTest.cs`, `ForceTest.cs`, `AngleTest.ai.cs`.

## Marker Interfaces (in Atmoos.Quantities/Units/)

| Interface | Extends | Meaning |
|---|---|---|
| `IUnit` | `IRepresentable` | Root marker; every unit has a `static String Representation`. |
| `ISiUnit` | `IUnit` | SI base & derived units. No `ToSi` needed (identity). |
| `IMetricUnit` | `IUnit, ITransform` | Metric units accepted by SI. Must implement `static Transformation ToSi(Transformation)`. |
| `IImperialUnit` | `IUnit, ITransform` | Imperial units. Must implement `ToSi`. |
| `INonStandardUnit` | `IUnit, ITransform` | Units without a system. Must implement `ToSi`. |
| `ISystemInject<TDimension>` | — | For units that inject a basis dimension (e.g. `Hertz` injects `ITime`). |
| `IInvertible<TDimension>` | `ISystemInject<TDimension>` | Marker for inverse-dimension units (e.g. `Hertz` is `IInvertible<ITime>`). |
| `IPowerOf<TDimension>` | `ISystemInject<TDimension>` | Marker for power-dimension units. |

## Creating a New Unit

### SI Derived (identity to SI) — e.g. Newton, Coulomb, Radian

```csharp
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived;

public readonly struct Newton : ISiUnit, IForce
{
    public static String Representation => "N";
}
```

No `ToSi` method — `ISiUnit` has identity transformation.

### Metric (non-identity) — e.g. Minute, Hour

```csharp
public readonly struct Minute : IMetricUnit, ITime
{
    public static Transformation ToSi(Transformation self) => 60 * self.RootedIn<Second>();
    public static String Representation => "min";
}
```

`RootedIn<TSi>()` documents which SI unit the conversion is relative to (it is a no-op that returns `self`).

### Imperial — e.g. Foot

```csharp
public readonly struct Foot : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 3048 * self.RootedIn<Metre>() / 1e4;
    public static String Representation => "ft";
}
```

### NonStandard — e.g. Knot (compound derived)

```csharp
public readonly struct Knot : INonStandardUnit, IVelocity
{
    public static Transformation ToSi(Transformation self) => self.DerivedFrom<NauticalMile>() / ValueOf<Hour>();
    public static String Representation => "kn";
}
```

`DerivedFrom<T>()` chains through another unit's `ToSi`. `ValueOf<T>()` evaluates a unit's SI polynomial to a scalar.

### Inverse-dimension units — e.g. Hertz

```csharp
public readonly struct Hertz : ISiUnit, IFrequency, IInvertible<ITime>
{
    static T ISystemInject<ITime>.Inject<T>(ISystems<ITime, T> basis) => basis.Si<Second>();
    public static String Representation => "Hz";
}
```

## Systems.cs — Unit Registration & Caching

`Systems` is the static entry-point users call to obtain `Scalar<TUnit>` references:

- `Si<TUnit>()` / `Si<TPrefix, TUnit>()` — SI units, with optional metric prefix.
- `Metric<TUnit>()` / `Metric<TPrefix, TUnit>()` — Metric units.
- `Binary<TPrefix, TUnit>()` — Binary-prefixed metric units.
- `Imperial<TUnit>()` — Imperial units.
- `NonStandard<TUnit>()` — Non-standard units.
- `Square<TUnit>(…)` / `Cubic<TUnit>(…)` — Power helpers.

All are backed by a file-scoped `Cache<TUnit, TMeasure>` that lazily creates and caches a `Scalar<TUnit>`.

## Dimension Interfaces (in Atmoos.Quantities/Dimensions/)

- **Base**: `ITime`, `ILength`, `IMass`, `IElectricCurrent`, `ITemperature`, `IAmountOfSubstance`, `ILuminousIntensity` — each extends `ILinear<TSelf>, IBaseQuantity`.
- **Derived**: `IArea`, `IVolume`, `IVelocity`, `IAcceleration`, `IForce`, `IPower`, `IEnergy`, `IFrequency`, `IPressure`, `IDensity`, `IVolumetricFlowRate`, `IMassFlowRate`, `IMomentum`, `IImpulse`, `ISpecificEnergy` — each extends `IDerivedQuantity` with appropriate product/power composition.
- **Electrical**: `IElectricPotential`, `IElectricalResistance`, `IElectricCharge`, `IAmountOfInformation`, `IInformationRate`.
- **Dimensionless**: `IAngle` (via `IDimensionless<IAngle>`).

A unit struct implements **both** a system interface (`ISiUnit`, `IMetricUnit`, etc.) **and** a dimension interface (`ILength`, `IForce`, etc.).

## Transformation Helpers (Extensions.cs)

| Helper | Purpose |
|---|---|
| `self.RootedIn<TSi>()` | Documents the SI reference unit (no-op, returns self). |
| `self.DerivedFrom<TBasis>()` | Chains through `TBasis.ToSi(self)` for derived conversions. |
| `ValueOf<T>(exponent)` | Evaluates `T.ToSi` polynomial to a `Double` scalar, raised to `exponent`. |

## Test Patterns (Units.Test)

- `FormattingMatches(v => Quantity.Of(v, measure), "symbol")` — verifies `ToString()` output.
- `PrecisionIsBounded(expected, actual)` — bounded floating-point comparison.
- `.Matches(expected)` — approximate equality assertion.
- `.Equal(expected)` — exact equality via `Assert.Equal`.
- `Usings.cs` globally imports: `Atmoos.Quantities.Physics`, `Atmoos.Quantities.Prefixes`, `Atmoos.Quantities.Units.Imperial.Length`, `Atmoos.Quantities.Units.Si`, `Systems`, `Convenience`, `Traits`.

## AI Transparency for Units

- Fully AI-generated files use `.ai.` infix: `Coulomb.ai.cs`, `AngleTest.ai.cs`.
- AI-generated types carry `[Ai(Model = "…", Version = "…", Variant = "…")]`.
