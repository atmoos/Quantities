<!-- markdownlint-disable MD033 MD041 -->
<div align="center">
 <img src="./assets/images/atmoos.quantities.svg" height="160" alt="Logo">
</div>
<!-- markdownlint-enable MD033 MD041 -->

# Atmoos Quantities

A library to safely handle various types of quantities, typically physical quantities.

[![main status](https://github.com/atmoos/Quantities/actions/workflows/dotnet.yml/badge.svg)](https://github.com/atmoos/Quantities/actions/workflows/dotnet.yml)
[![nuget package](https://img.shields.io/nuget/v/Atmoos.Quantities.svg?logo=nuget)](https://www.nuget.org/packages/Atmoos.Quantities)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/atmoos/Quantities/blob/master/LICENSE)

## Examples

Usage is designed to be intuitive:

- Instantiation *of* quantities with static factory methods
  - `Quantity.Of(42, Metric<Unit>()): Quantity`
- Conversion *to* other units with instance conversion methods
  - `quantity.To(Imperial<Unit>())`

### Instantiation

Somewhere in your project, define this global static using directive:

```csharp
global using static Atmoos.Quantities.Systems;
```

Then, use quantities intuitively:

```csharp
Length metres = Length.Of(4, Si<Metre>());
Length miles = Length.Of(12, Imperial<Mile>());
Length kilometres = Length.Of(18, Si<Kilo, Metre>());
Velocity kilometresPerHour = Velocity.Of(4, Si<Kilo, Metre>().Per(Metric<Hour>()));
```

### Conversion

```csharp
Length miles = metres.To(Imperial<Mile>());
Length kilometres = metres.To(Si<Kilo, Metre>());
Velocity metresPerSecond = kilometresPerHour.To(Si<Metre>().Per(Si<Second>()));
Velocity milesPerHour = kilometresPerHour.To(Imperial<Mile>().Per(Metric<Hour>()));
```

### Operator Overloads

Quantities support common operations such as addition, subtraction, multiplication and division. The operations are "left associative", meaning the units of the left operand are "carried over" to the result when possible.

```csharp
Time time = Time.Of(3, Metric<Hour>());

Velocity metricVelocity = kilometres / time; // 6 km/h
Velocity imperialVelocity = miles / time; // 4 mi/h

Area metricArea = kilometres * miles; // 347.62 km²
Area imperialArea = miles * kilometres; // 134.22 mi²
Console.WriteLine($"Equal area: {metricArea.Equals(imperialArea)}"); // Equal area: True

Length metricSum = kilometres + miles - metres; // 37.308 km
Length imperialSum = miles + kilometres - metres; // 23.182 mi
Console.WriteLine($"Equal length: {imperialSum.Equals(metricSum)}"); // Equal length: True
```

### Type Safety

As one of the primary goals it to ensure safety when using quantities, type safety is essential.

Additive operations only work on instances of the same type

```csharp
Power power = Power.Of(36, Si<Watt>());
Mass mass = Mass.Of(0.2, Metric<Tonne>());

// Doesn't compile:
// Cannot implicitly convert type 'double' to 'Power'
Mass foo = mass + power;
Power bar = power + mass;
```

Multiplication of different quantities is very common, hence compile errors are less frequent. Type safety is always ensured, though.

```csharp
// Common operation: Ohm's Law
ElectricCurrent ampere = ElectricCurrent.Of(3, Si<Ampere>());
ElectricalResistance ohm = ElectricalResistance.Of(7, Si<Ohm>());

// U = R * I
// The multiplicative result is a different type: ElectricPotential
ElectricPotential potential = ohm * ampere; // 21 V

// Eccentric operation
Time time = Time.Of(5, Metric<Hour>());
Mass mass = Mass.Of(0.2, Metric<Tonne>());

// Doesn't compile
// Operator '*' is ambiguous on operands of type 'Mass' and 'Time'
Mass foo = mass * time;
Time bar = time * mass;
var fooBar = mass * time;
```

### Binary Prefixes

Different types of prefixes are also supported. This is useful for [IEC binary prefixes](https://en.wikipedia.org/wiki/Binary_prefix).

```csharp
Data kibiByte = Data.Of(1, Binary<Kibi, Byte>()); // 1 KiB, binary prefix
Data kiloByte = Data.Of(1.024, Metric<Kilo, Byte>()); // 1 kB, metric prefix
Console.WriteLine($"Equal amount of data: {kiloByte.Equals(kibiByte)}"); // Equal amount of data: True
```

## Should I use this Library?

Yes. The API has stabilised. Furthermore, the library outperforms naive implementations both in terms of performance and accuracy. Additionally, any combination of prefix and unit is supported out of the box for si and metric quantities.

Also, this library supports serialization for json with both `System.Text.Json` and `Newtonsoft.Json`. If a specific form of serialization is required, the library provides api's to extend it with custom serialization. (See [write](./source/Quantities/Core/Serialization/IWriter.cs) and [read](./source/Quantities/Serialization/QuantityFactory.cs) support.)

## Thanks

- to contributors for providing feedback and ideas.
- to my sister [Lucy Kägi](https://www.lucykaegi.ch/) for creating the [logo](assets/images/atmoos.quantities.svg).
