﻿using Quantities.Dimensions;

namespace Quantities.Units.Si;

// See: https://en.wikipedia.org/wiki/SI_base_unit
public readonly struct Kilogram : ISiUnit, IMass
{
    public static String Representation => "kg";
}
