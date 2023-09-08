﻿using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

// https://en.wikipedia.org/wiki/Lambda_(unit)
// https://en.wikipedia.org/wiki/List_of_metric_units
public readonly struct Lambda : IMetricUnit, IVolume, IInjectUnit<ILength>
{
    internal const Double fromCubicMetre = 1e9; // 1e9 λ = 1 m³
    public static Transformation ToSi(Transformation self) => self / fromCubicMetre;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        // 1 λ == 1 mm³
        return inject.Si<Metre>(self / fromCubicMetre);
    }
    public static String Representation => "λ";
}
