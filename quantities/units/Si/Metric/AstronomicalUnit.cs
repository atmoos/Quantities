using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

// https://en.wikipedia.org/wiki/Astronomical_unit
public readonly struct AstronomicalUnit : IMetricUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 149597870700 * self.RootedIn<Metre>();
    public static String Representation => "au";
}
