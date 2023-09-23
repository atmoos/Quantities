﻿using Quantities.Dimensions;
using Quantities.Units.Si;

namespace Quantities.Units.NonStandard.Mass;

// https://de.wikipedia.org/wiki/Zentner
public readonly struct Zentner : INoSystemUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 50 * self.RootedIn<Kilogram>();
    public static String Representation => "Ztr";
}
