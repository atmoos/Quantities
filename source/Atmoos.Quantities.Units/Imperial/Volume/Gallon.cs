﻿using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Imperial.Length;

namespace Atmoos.Quantities.Units.Imperial.Volume;

// https://en.wikipedia.org/wiki/Gallon
public readonly struct Gallon : IImperialUnit, IVolume, IPowerOf<ILength>
{
    public static Transformation ToSi(Transformation self) => 4.54609 * self / 1e3;
    static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Imperial<Foot>();
    public static String Representation => "gal";
}
