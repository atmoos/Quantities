﻿using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Metric;

public readonly struct Are : IMetricUnit, IArea, IPowerOf<ILength>
{
    internal const Double ToSquareMetre = 1e2; // a -> m²
    public static Transformation ToSi(Transformation value) => ToSquareMetre * value;
    static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();
    public static String Representation => "a";
}
