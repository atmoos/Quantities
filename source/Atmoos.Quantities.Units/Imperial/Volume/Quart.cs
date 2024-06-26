﻿using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Imperial.Length;

namespace Atmoos.Quantities.Units.Imperial.Volume;

// https://en.wikipedia.org/wiki/Quart
public readonly struct Quart : IImperialUnit, IVolume, IPowerOf<ILength>
{
    public static Transformation ToSi(Transformation self) => 1.1365225 * self / 1e3;
    static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Imperial<Inch>();
    public static String Representation => "qt";
}
