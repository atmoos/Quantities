﻿using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si;

// See: https://en.wikipedia.org/wiki/SI_base_unit
public readonly struct Kelvin : ISiUnit, ITemperature
{
    public static String Representation => "K";
}
