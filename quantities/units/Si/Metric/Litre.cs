using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Litre : IMetricUnit, IVolume<ILength>, IInjectUnit<ILength>
{
    internal const Double InCubicMetre = 1e3; // 1000 ℓ in 1 m³
    public static Double ToSi(in Double value) => value / InCubicMetre;
    public static Double FromSi(in Double value) => value * InCubicMetre;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        return inject.Si<Metre>(self / InCubicMetre);
    }
    public static String Representation => "ℓ";
}
