using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Are : IMetricUnit, IArea, IInjectUnit<ILength>
{
    internal const Double ToSquareMetre = 1e2; // a -> m²
    public static Transformation ToSi(Transformation value) => ToSquareMetre * value;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        return inject.Si<Metre>(self * ToSquareMetre);
    }
    public static String Representation => "a";
}
