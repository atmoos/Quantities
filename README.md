# Quantities
A library to safely handle various types of quantities, typically physical quantities.

[![master status](https://github.com/atmoos/Quantities/actions/workflows/dotnet.yml/badge.svg)](https://github.com/atmoos/Quantities/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/atmoos/Quantities/blob/master/LICENSE)

## Project Goals
Dealing with quantities (Metre, Yard, etc.) is not trivial. There are many areas where things can go wrong, such as forgetting to convert from one unit to the next or converting the wrong way.
This library set's out to remove that burden from developers and API designers alike.

### A Generic API 
This is primarily a project that lets me explore how far one can push C#'s generics in an API. The goal is to create an api where generics apply naturally and enhance readability.
On the flip side, some implementation details in this library are plain out scary and weird, but heaps of fun to explore.

### Why Physical Quantities?
Using physical quantities as test subject seemed appropriate, as there are a limited number of units and SI-prefixes. Using generics, these prefixes and units can be combined neatly to create all sorts of representations of quantities. The generic constraints then allow for the API to restrict the prefixes and units to a subset that actually make sense on a particular quantity.
A concrete example helps to illustrate that point: A length may be represented in the SI-unit of metres or imperial units feet, but not with a unit that is used to represent time. Furthermore, it is standard usage to use the SI-units with prefixes, such as "Kilo" or "Milli", but not on imperial units. Hence, the generic constraints are set accordingly.

## Should I Use It?
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

## Examples
Usage is designed to be intuitive using:
- instantiation with static factory methods
- instance conversion methods
- operator overloads

### Instantiation

```csharp
Length metres = Length.Si<Metre>(4);
Length miles = Length.Imperial<Mile>(12);
Length kilometres = Length.Si<Kilo, Metre>(18);
Velocity kilometresPerHour = Velocity.Si<Kilo, Metre>(4).Per<Hour>();
```

### Conversion
```csharp
Length miles = metres.ToImperial<Mile>();
Length kilometres = metres.ToSi<Kilo, Metre>();
Velocity metresPerSecond = kilometresPerHour.To<Metre>().PerSecond();
Velocity milesPerHour = kilometresPerHour.ToImperial<Mile>().Per<Hour>();
```

### Operator Overloads
Quantities support common operations such as addition, subtraction, multiplication and division. The operations are "left associative", meaning the units of the left operand are "carried over" to the result when possible.
```csharp
Time time = Time.In<Hour>(3);

Velocity metricVelocity = kilometres / time; // 6 km/h
Velocity imperialVelocity = miles / time; // 4 mi/h

Area metricArea = kilometres * miles; // 347.62 Km²
Area imperialArea = miles * kilometres ; // 134.22 mi²
Console.WriteLine($"Equal area: {metricArea.Equals(imperialArea)}"); // Equal area: true

Length metricSum = kilometres + miles - metres; // 37.308 Km
Length imperialSum = miles + kilometres - metres; // 23.182 mi
Console.WriteLine($"Equal length: {imperialSum.Equals(metricSum)}"); // Equal length: true
```

### Type Safety
As one of the primary goals it to ensure safety when using quantities, type safety is essential.

Additive operations only work on instances of the same type
```csharp
Power power = Power.Si<Watt>(36);
Mass mass = Mass.Metric<Tonne>(0.2);

// Doesn't compile:
// Cannot implicitly convert type 'double' to 'Power'
Mass foo = mass + power;
Power bar = power + mass;
```

Multiplication of different quantities is very common, hence compile errors are less frequent. Type safety is always ensured, though.
```csharp
// Common operation: Ohm's Law
ElectricCurrent ampere = ElectricCurrent.Si<Ampere>(3);
ElectricalResistance ohm = ElectricalResistance.Si<Ohm>(7);

// U = R * I
// The multiplicative result is a different type: ElectricPotential
ElectricPotential potential = ohm * ampere; // 21 V

// Eccentric operation
Time time = Time.In<Hour>(5);
Mass mass = Mass.Metric<Tonne>(0.2);

// Doesn't compile
// Operator '*' is ambiguous on operands of type 'Mass' and 'Time'
Mass foo = mass * time;
Time bar = time * mass;
var fooBar = mass * time;
```
