using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

// See: https://en.wikipedia.org/wiki/Minute
public readonly struct Minute : IMetricUnit, ITime
{
    public static Transformation ToSi(Transformation self) => 60 * self.RootedIn<Second>();
    public static String Representation => "m";
}
