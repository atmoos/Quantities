using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Litre : IMetricUnit, IVolume, IInjectUnit<ILength>
{
    internal const Double toCubicMetre = 1e-3; // 1 ℓ = 1e-3 m³
    public static Transformation ToSi(Transformation self) => self * toCubicMetre;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        return inject.Si<Metre>(self * toCubicMetre);
    }
    public static String Representation => "ℓ";
}
