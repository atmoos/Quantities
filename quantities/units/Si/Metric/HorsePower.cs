using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

// See: https://en.wikipedia.org/wiki/Horsepower
public readonly struct HorsePower : IMetricUnit, IPower
{
    public static Transformation ToSi(Transformation self) => 75 * self * 9.80665; // ~735.49875 W in 1 hp
    public static String Representation => "hp";
}
