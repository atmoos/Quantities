using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Metric;

public readonly struct Tonne : IMetricUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 1000 * self.RootedIn<Kilogram>();

    public static String Representation => "t";
}
