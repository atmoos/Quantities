using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Si.Metric;

// See: https://en.wikipedia.org/wiki/Horsepower
public readonly struct HorsePower : IMetricUnit, IPower
{
    public static Transformation ToSi(Transformation self) => 9.80665 * 75 * self.RootedIn<Watt>();

    public static String Representation => "PS";
}
