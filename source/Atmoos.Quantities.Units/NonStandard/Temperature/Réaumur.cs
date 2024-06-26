﻿using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si;

namespace Atmoos.Quantities.Units.NonStandard.Temperature;

// [K] = [°Ré] × ​5⁄4 + 273.15
// See: https://en.wikipedia.org/wiki/Conversion_of_scales_of_temperature
public readonly struct Réaumur : INonStandardUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => 5 * self.RootedIn<Kelvin>() / 4 + 273.15;
    public static String Representation => "°Ré";
}
