using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Ångström : IMetricUnit, ILength
{
    internal const Double metreToÅngström = 1e10; // m -> Å
    public static Transformation ToSi(Transformation self) => self / metreToÅngström;
    public static String Representation => "Å";
}
