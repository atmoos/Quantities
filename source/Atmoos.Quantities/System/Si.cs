using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Si;

using static Atmoos.Quantities.Systems;

namespace Atmoos.Quantities.System;

public static class Si
{
    public static Length m { get; } = Length.Of(1, Si<Metre>());
    public static Length Km { get; } = Length.Of(1, Si<Kilo, Metre>());
    public static Length cm { get; } = Length.Of(1, Si<Centi, Metre>());
    public static Length mm { get; } = Length.Of(1, Si<Milli, Metre>());
    public static Length μm { get; } = Length.Of(1, Si<Micro, Metre>());
}

