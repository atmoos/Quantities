using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Litre : IMetricUnit, IVolume, IAlias<ILength>
{
    internal const Double fromCubicMetre = 1e3; // 1e3 ℓ = 1 m³
    public static Transformation ToSi(Transformation self) => self / fromCubicMetre;
    static T IAlias<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();
    public static String Representation => "ℓ";
}
