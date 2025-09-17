# Atmoos.Quantities

This library enables type-safe handling of quantities. It is a general purpose library that allows third parties to define their own units if needed. Many units are predefined in this library or are available on nuget [here](https://www.nuget.org/packages/Atmoos.Quantities.Units/).

It is based on the [international system of units](https://en.wikipedia.org/wiki/International_System_of_Units) (SI), but also supports many other units, such as imperial units.

Design principles:

- Type-safe by default.
- General purpose, i.e. extendable.
  - Broad set of units available in [Atmoos.Quantities.Units](https://www.nuget.org/packages/Atmoos.Quantities.Units/).
  - Custom units are supported, too.
  - All metric and binary prefixes are supported.
- Serialization is supported via *Atmoos.Quantities.Serialization*
  - [System.Text.Json](https://www.nuget.org/packages/Atmoos.Quantities.Serialization.Text.Json/)
  - [Newtonsoft](https://www.nuget.org/packages/Atmoos.Quantities.Serialization.Newtonsoft/)
- Optimized for
  - **accuracy**: numerically more stable than just using doubles.
  - **low memory footprint**: all quantities are structs and don't allocate on the heap.
  - **speed**: likely faster than naive implementations.

## Usage Patterns

Add this global using somewhere in your project.

```csharp
global using static Quantities.Systems;
```

Then, quantities can be used like this:

```csharp
Time hours = Time.Of(4, Metric<Hour>());
Length feet = Length.Of(2, Imperial<Foot>());
Volume volume = Volume.Of(3, Cubic(Si<Metre>()));
Length kilometres = Length.Of(16, Si<Kilo, Metre>());
Velocity kilometresPerHour = Velocity.Of(4, Si<Kilo, Metre>().Per(Metric<Hour>()));
Velocity arithmeticVelocity = kilometres / hours; // 4 km/h
Area area = volume / feet; // 4.921... m²
```

## Supported Si Prefixes

- All [metric prefixes](https://en.wikipedia.org/wiki/Metric_prefix) `Quecto` through to `Quetta`.
- All [binary prefixes](https://en.wikipedia.org/wiki/Binary_prefix) `Kibi` through to `Yobi`.

They are all compatible with each other.

## Supported Quantities

The currently supported quantities are:

```text quantities
Atmoos.Quantities
├── Area
├── Data
├── DataRate
├── ElectricalResistance
├── ElectricCurrent
├── ElectricPotential
├── Energy
├── Force
├── Frequency
├── Length
├── Mass
├── Power
├── Temperature
├── Time
├── Velocity
└── Volume
```

They support a broad range of compatible units as indicated by the examples above.

## Supported Units of Measurement

The following units are included in this library. Please see [Atmoos.Quantities.Units](https://www.nuget.org/packages/Atmoos.Quantities.Units/) for many more units.

```text units
Atmoos.Quantities.Units
├── Si
│   ├── Ampere
│   ├── Candela
│   ├── Kelvin
│   ├── Kilogram
│   ├── Metre
│   ├── Mole
│   ├── Second
│   └── Metric
│       ├── Temperature
│       │   └── Celsius
│       ├── Time
│       │   ├── Hour
│       │   └── Minute
│       └── Volume
│           └── Litre
└── Imperial
    ├── Length
    │   ├── Foot
    │   ├── Inch
    │   └── Mile
    ├── Mass
    │   └── Pound
    ├── Temperature
    │   └── Fahrenheit
    └── Volume
        └── Pint
```
