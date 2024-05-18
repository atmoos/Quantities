using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Metric;

// See: https://en.wikipedia.org/wiki/Hour
public readonly struct Hour : IMetricUnit, ITime
{
    public static Transformation ToSi(Transformation self) => 3600 * self.RootedIn<Second>();
    public static String Representation => "h";
}
