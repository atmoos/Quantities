using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Litre : IMetricUnit, IVolume, IInjectUnit<ILength>
{
    internal const Double toCubicMetre = 1e-3; // 1 ℓ = 1e-3 m³
    internal const Double fromCubicMetre = 1e3; // 1e3 ℓ = 1 m³
    public static Double ToSi(in Double self) => self * toCubicMetre;
    public static Double FromSi(in Double value) => value * fromCubicMetre;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        return inject.Si<Metre>(self * toCubicMetre);
    }
    public static String Representation => "ℓ";
}
