using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Ångström : IMetricUnit, ILength
{
    internal const Double MetreToÅngström = 1e10; // m -> Å
    public static Double ToSi(in Double value) => value / MetreToÅngström;
    public static Double FromSi(in Double value) => value * MetreToÅngström;
    public static String Representation => "Å";
}
