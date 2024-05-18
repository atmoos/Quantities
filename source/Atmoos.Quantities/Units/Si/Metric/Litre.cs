using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Metric;

// See: https://en.wikipedia.org/wiki/Litre
public readonly struct Litre : IMetricUnit, IVolume, IPowerOf<ILength>
{
    internal const Double fromCubicMetre = 1e3; // 1e3 ℓ = 1 m³
    public static Transformation ToSi(Transformation self) => self / fromCubicMetre;
    static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();
    public static String Representation => "ℓ"; // SI symbols are actually: L or l
}
