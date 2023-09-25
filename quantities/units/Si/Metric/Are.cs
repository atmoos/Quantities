using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Are : IMetricUnit, IArea, IAlias<ILength>
{
    internal const Double ToSquareMetre = 1e2; // a -> m²
    public static Transformation ToSi(Transformation value) => ToSquareMetre * value;
    static T IAlias<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();
    public static String Representation => "a";
}
