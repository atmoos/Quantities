# Architecture

## Concepts

Every...

- quantity is a struct
- quantity has an _internal_ [unit](../quantities/units/IUnit.cs) that is either
  - [SI](../quantities/units/Si/ISiUnit.cs) based
  - [Imperial](../quantities/units/Imperial/IImperial.cs)
  - [other](../quantities/units/Other/IOther.cs)
- SI quantity supports [prefixing](../quantities/prefixes/IPrefix.cs) with
  - a decimal prefix
  - a binary prefix
  - or both
- quantity represents a unique [dimension](../quantities/dimensions/IDimension.cs)
- quantity can only be instantiated using _generic_ factory methods
  - that are parameterized by a combination of generic prefix and unit parameters.
  - The unit parameter is constrained by the same dimension the quantity implements
- quantity is implicitly convertible to a Double
- quantity supports
  - additive operations
  - scalar multiplicative operations
  - comparison to quantities of the same type
- quantity is left associative
  - a compound expression will take the prefix and unit of the left most term
- quantity can be converted to any other valid combination of prefix and unit

Therefore, the actual underlying unit and/or prefix of a given type is an irrelevant detail of any quantity.

## Public API

This library was built around an API I had in mind. The API should rely heavily on generics, which would make it very easy to use and extend for an arbitrary amount of units and (metric) prefixes.

The minimal API for creation looks like this:

```csharp
public struct TQuantity : IMyDimension
{
    public static TQuantity Create<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix
        where TUnit : IMyDimension, IUnit;
}
```

Conversion between units should follow the same pattern, hence:

```csharp
public struct TQuantity : IMyDimension
{
    public TQuantity To<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : IMyDimension, IUnit;
}
```

Notice, how the use of the type parameters resembles how one would normally use function arguments. This is a key concept!
Also the generic constraints define what types may be used. For length quantities for instance, it does not make sense to use a unit of `Litre`, as litres are a measure of volume, not length.

Using UML the concept may be illustrated as follows:

```mermaid
---
title: Public API using Length as Example
---
classDiagram
    ILength <|.. Length
    IUnit <|.. Metre
    ILength <|.. Metre
    IUnit <|.. Foot
    ILength <|.. Foot
    IPrefix <|.. Kilo
    IPrefix <|.. Centi
    IPrefix <|.. Micro
    class ILength{
        <<interface>>
    }
    class IPrefix{
        <<interface>>
    }
    class IUnit{
        <<interface>>
    }
    class Length{
        <<struct>>
        +To#lt;TPrefix, TUnit#gt;() Length
        +Create#lt;TPrefix, TUnit#gt;(Double value)$ Length
    }
```

Note, that `Length` implements `ILength`, but not `IUnit`!

Creating one kilometre is then as simple as:

```csharp
Length oneKm = Length.Create<Kilo, Metre>(1d);
```

## Decomposition

The implementation of various quantities revolves around a single struct the `Quant` (please excuse the name! There are already too many quantities...).

The quant provides for all the generic conversion capabilities that actual quantities might need to make use of.

Each actual quantity ([Length](../quantities/quantities/Length.cs), [Time](../quantities/quantities/Time.cs), [Velocity](../quantities/quantities/Velocity.cs), etc.) wrap a 'Quant' instance, effectively restricting the operations allowed on that quantity.

```mermaid
classDiagram
    IQuantity~TQ~ <|.. Quantity
    IQuantity~TQ~ <|.. IQuantity~TQ~
    Quant --o Quantity
    Map --o Quant
    class IQuantity~TQ~{
        <<interface>>
    }
    class Quantity{
        <<struct>>
        -Quant quant
        +To#lt;P, U#gt;() Quantity
        +Create#lt;P, U#gt;(Double value)$ Quantity
    }
    class Quant{
        <<struct>>
        ~Map map
        ~Double Value
        -Project(in Quant other)
    }
    class Map{
        <<singleton>>
        ~String Representation
        ~ToSi()
        ~FromSi()
    }
```

## Realization of Formulae

U = R * I

V = L / T

etc...

**ToDo!!!**

## Precision

For now, double precision is used. Once all quantities are implemented, it may be considered making the underlying data type a generic parameter.

For now, it is already complicated enough.

## Definitions & Spelling

All definitions are rooted in the [international system of units - SI](https://en.wikipedia.org/wiki/International_System_of_Units). When there are conflicting definitions, SI always takes precedence.

This also holds true for spelling. SI spelling takes precedence.

For imperial units the british definition and spelling are used.
