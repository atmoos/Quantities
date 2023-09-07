using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Litre : IMetricUnit, IVolume, IInjectUnit<ILength>
{
    internal const Double fromCubicMetre = 1e3; // 1e3 ℓ = 1 m³
    public static Transformation ToSi(Transformation self) => self / fromCubicMetre;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        return inject.Si<Metre>(self / fromCubicMetre);
    }
    public static String Representation => "ℓ";
}
