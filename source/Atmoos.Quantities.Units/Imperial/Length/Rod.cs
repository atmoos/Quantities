﻿using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Imperial.Length;

// https://en.wikipedia.org/wiki/Rod_(unit)
// https://en.wikipedia.org/wiki/Imperial_units
public readonly struct Rod : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 66 * self.DerivedFrom<Foot>() / 4;
    public static String Representation => "rod";
}
