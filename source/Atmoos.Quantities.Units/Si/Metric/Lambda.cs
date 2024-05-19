using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Metric;

// https://en.wikipedia.org/wiki/Lambda_(unit)
// https://en.wikipedia.org/wiki/List_of_metric_units
public readonly struct Lambda : IMetricUnit, IVolume, IPowerOf<ILength>
{
    internal const Double fromCubicMetre = 1e9; // 1e9 λ = 1 m³
    public static Transformation ToSi(Transformation self) => self / fromCubicMetre;
    static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();
    public static String Representation => "λ";
}
