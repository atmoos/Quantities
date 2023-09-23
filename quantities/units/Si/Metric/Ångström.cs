using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Ångström : IMetricUnit, ILength
{
    public static Transformation ToSi(Transformation self) => self.RootedIn<Metre>() / 1e10;
    public static String Representation => "Å";
}
