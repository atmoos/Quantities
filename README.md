# Quantities

A library to safely handle various types of quantities, typically physical quantities.

[![master status](https://github.com/atmoos/Quantities/actions/workflows/dotnet.yml/badge.svg)](https://github.com/atmoos/Quantities/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/atmoos/Quantities/blob/master/LICENSE)

## Examples

Usage is designed to be intuitive:

- Instantiation *of* quantities with static factory methods
  - `Quantity.Of(42).Metric<Unit>()`
- Conversion *to* other units with instance conversion methods
  - `quantity.To.Imperial<Unit>()`
- Use of operator overloads

### Instantiation

```csharp
Length metres = Length.Of(4).Si<Metre>();
Length miles = Length.Of(12).Imperial<Mile>();
Length kilometres = Length.Of(18).Si<Kilo, Metre>();
Velocity kilometresPerHour = Velocity.Of(4).Si<Kilo, Metre>().Per.Metric<Hour>();
```

### Conversion

```csharp
Length miles = metres.To.Imperial<Mile>();
Length kilometres = metres.To.Si<Kilo, Metre>();
Velocity metresPerSecond = kilometresPerHour.To.Si<Metre>().Per.Si<Second>();
Velocity milesPerHour = kilometresPerHour.To.Imperial<Mile>().Per.Metric<Hour>();
```

### Operator Overloads

Quantities support common operations such as addition, subtraction, multiplication and division. The operations are "left associative", meaning the units of the left operand are "carried over" to the result when possible.

```csharp
Time time = Time.Of(3).Metric<Hour>();

Velocity metricVelocity = kilometres / time; // 6 km/h
Velocity imperialVelocity = miles / time; // 4 mi/h

Area metricArea = kilometres * miles; // 347.62 Km²
Area imperialArea = miles * kilometres; // 134.22 mi²
Console.WriteLine($"Equal area: {metricArea.Equals(imperialArea)}"); // Equal area: True

Length metricSum = kilometres + miles - metres; // 37.308 Km
Length imperialSum = miles + kilometres - metres; // 23.182 mi
Console.WriteLine($"Equal length: {imperialSum.Equals(metricSum)}"); // Equal length: True
```

### Type Safety

As one of the primary goals it to ensure safety when using quantities, type safety is essential.

Additive operations only work on instances of the same type

```csharp
Power power = Power.Of(36).Si<Watt>();
Mass mass = Mass.Of(0.2).Metric<Tonne>();

// Doesn't compile:
// Cannot implicitly convert type 'double' to 'Power'
Mass foo = mass + power;
Power bar = power + mass;
```

Multiplication of different quantities is very common, hence compile errors are less frequent. Type safety is always ensured, though.

```csharp
// Common operation: Ohm's Law
ElectricCurrent ampere = ElectricCurrent.Of(3).Si<Ampere>();
ElectricalResistance ohm = ElectricalResistance.Of(7).Si<Ohm>();

// U = R * I
// The multiplicative result is a different type: ElectricPotential
ElectricPotential potential = ohm * ampere; // 21 V

// Eccentric operation
Time time = Time.Of(5).Metric<Hour>();
Mass mass = Mass.Of(0.2).Metric<Tonne>();

// Doesn't compile
// Operator '*' is ambiguous on operands of type 'Mass' and 'Time'
Mass foo = mass * time;
Time bar = time * mass;
var fooBar = mass * time;
```

### Binary Prefixes

Different types of prefixes are also supported. This is useful for [IEC binary prefixes](https://en.wikipedia.org/wiki/Binary_prefix).

```csharp
Data kibiByte = Data.Of(1).Binary<Kibi, Byte>(); // 1 KiB, binary prefix
Data kiloByte = Data.Of(1.024).Metric<Kilo, Byte>(); // 1 KB, metric prefix
Console.WriteLine($"Equal amount of data: {kiloByte.Equals(kibiByte)}"); // Equal amount of data: True
```

## Design Philosophy

This library is intended to solve the ambiguity faced when dealing with physical units. Represented here by the umbrella term "quantities". Within a narrow scope (a method or private members of a class) it often suffices to declare the units that are in use with a comment and just use a `double` or a `float` to represent any quantity within that scope. The real issue arises when **designing APIs** where quantities with their associated units start to play a relevant role.
This is where this library comes into play, enabling a type-safe means of declaring quantities on an API as "drop in replacements" for, say `double` or `float`.

### A Motivating Example

Let's look at a simple example of calculating a velocity:

```csharp
public Double CalculateVelocity(Double lengthInMeters, Double timeInSeconds) => lengthInMeters / timeInSeconds;
```

This method declares what units are expected in the names of the arguments and relies on any caller to heed the indicated units. Also, it assumes the caller has enough knowledge of physics that he or she will be able to infer that the returned units are *probably* "m/s", although there is *no guarantee*.

With this library the API would read much clearer:

```csharp
public Velocity CalculateVelocity(Length length, Time time) => length / time;
```

The units have become irrelevant, but the expected quantities are declared in a type-safe way. Also, the returned quantity is now obvious, it's `Velocity`. But "what about the units" you may be asking yourself? Well they are a priori and de facto irrelevant for the caller and the method implementation. However, both the caller and implementer have the power to explicity *choose* whatever makes most sense for their use case or domain.

Again, let's look at an example for the caller and assume she wants to print the result to the console. Let's assume she's a researcher so she'll likely be wanting the SI unit "m/s", which is what she's easily able to express:

```csharp
Velocity velocity = CalculateVelocity(/* any values */); // The units in which the calculation is performed are irrelevant.

Console.WriteLine($"The velocity is: {velocity.To.Si<Metre>().Per.Si<Second>()}"); // The velocity is: 42 m/s;
```

Let's look at an example for someone implementing the `CalculateVelocity` method. This time let's assume it's train manufacturer, so "Kilometres" and "Hours" might be most familiar to them. Let's further assume that complex logic is required that may even involve some other dependency which lets them choose to do the actual calculation using `double`:

```csharp
public Velocity CalculateVelocity(Length length, Time time)
{
  Double lengthInKilometres = length.To.Si<Kilo, Metre>();
  Double timeInHours = time.To.Metric<Hour>();

  Double velocityInKilometresPerHours = /* complex logic */;

  return Velocity.Of(velocityInKilometresPerHours).Si<Kilo, Metre>().Per.Metric<Hour>();
}
```

To maintainers of the above code snippet it will always be clear what units are in use in the implementation, as the scope is narrow. For callers the API is clear and expressive: the quantity is obvious (Velocity) and the units can be anything they chose.

### Design Principles

With the above example established, we'd like to state some design principles:

- This library is domain agnostic.
  - Hence, we make no assumptions of what users might wan't to model.
  - Nor what kind of data would be modelled.
- This library does not validate input.
  - As long as it's a valid `Double`, we'll take it.
  - Example: A negative `Length` is a valid value. (It's a valid floating point value.)
  - If users need to constrain the value of a quantity, the'll need to do that themselves.
  - This includes "Divide by zero" scenarios, which we leave to .Net to handle.
- This library is designed to be fast.
  - It's not as fast as using `Double` directly though.
  - However, precision wins out over speed in some cases.
- This library aims to avoid memory allocations.
  - This holds true for many quantities.
- Units and prefixes are represented by types, not values.
  - This allows users to easily [use their own](./quantities.test/UserDefined.cs) units.
  - We don't see a scenario for user defined prefixes, but it's possible to do none the less.

### Naming

The naming of units and prefixes follows the definitions given by the [International System of Units](https://en.wikipedia.org/wiki/International_System_of_Units) (SI). If no naming can be found there, the consensus formed on the corresponding english Wikipedia page will be used.
This leads to the following list of naming conventions:

- We use the *international* name as defined by SI or Wikipedia
  - Many units are named after individuals. We respect the way they spell their own name.
  - Hence we use [Ångström](./quantities/units/Si/Metric/Ångström.cs), not "Angstrom".
  - We can do this since C# source code is UTF-8 and supports special characters
- Potential duplicate names are resolved via namespaces.
  - Examples are the well known unit of force, the [Newton](./quantities/units/Si/Derived/Newton.cs) and the lesser known unit of temperature, the [Newton](./quantities/units/NonStandard/Temperature/Newton.cs).

## Should I use this Library?

It's a library that is still evolving rapidly. Try at your own risk or - even better - contribute :-)

## ToDo

- [x] Enable [binary prefixes](https://en.wikipedia.org/wiki/Binary_prefix).
  - Enabling things like "KiB", i.e. "kibi Byte".
- [ ] Enable serialisation
- [ ] Extend unit tests
- [ ] More rigours benchmarking
- [ ] Add more quantities
- [ ] Add "Zero" and "One/Unit" static properties
  - i.e. enabling additive and multiplicative identities.
- [ ] Add a "Normalize()" method to each quantity
  - This should then generate a "human readable" representation
  - example: 3'456 Km/d => 40 m/s
- [ ] Rename the [Quant](quantities/measures/Quant.cs) type
  - Top candidate: "Amount"
