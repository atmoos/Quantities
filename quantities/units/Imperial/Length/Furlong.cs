﻿using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Furlong : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 22 * self.DerivedFrom<Mile>() / 8;
    public static String Representation => "fur";
}
