# Atmoos Quantities

This library enables typesafe handling of quantities. It is a general purpose library that allows third parties to define their own units if needed. Many units are predefined in this library or are available on nuget.

It's based on the international system of measurements (SI), but also supports many other units.

Design principles:

- Typesafe
- General purpose, i.e. extendable.
- Supports serialization.
- Optimized for
  - speed
    - likely faster than naive implementations.
  - low memory footprint
    - all quantities are structs and don't allocate on the heap.
  - accuracy
    - numerically more stable than just using doubles.

## Usage Patterns

```csharp
Time hours = Time.Of(4).Metric<Hour>();
Length feet = Length.Of(2).Imperial<Foot>();
Volume volume = Volume.Of(3).Cubic.Si<Metre>();
Length kilometres = Length.Of(16).Si<Kilo, Metre>();
Velocity kilometresPerHour = Velocity.Of(4).Si<Kilo, Metre>().Per.Metric<Hour>();
Velocity arithmeticVelocity = kilometres / hours; // 4 km/h
Area area = volume / feet; // 4.921... m²
```

## Supported Si Prefixes

- All metric prefixes Quecto through to Quetta.
- All binary prefixes Kibi through to Yobi.

They are all compatible with each other.

## Supported Units of Measurement

```text
├── Si
│   ├── Ampere
│   ├── Candela
│   ├── Derived
│   │   └── Celsius
│   ├── Kelvin
│   ├── Kilogram
│   ├── Metre
│   ├── Metric
│   │   ├── Hour
│   │   ├── Litre
│   │   └── Minute
│   ├── Mole
│   └── Second
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
