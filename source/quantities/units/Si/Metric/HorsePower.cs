using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

// See: https://en.wikipedia.org/wiki/Horsepower
public readonly struct HorsePower : IMetricUnit, IPower
{
    public static Transformation ToSi(Transformation self) => 9.80665 * 75 * self.RootedIn<Metre>();
    public static String Representation => "hp";
}
