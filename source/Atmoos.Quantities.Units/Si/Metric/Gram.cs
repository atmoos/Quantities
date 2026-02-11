using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Metric;

public readonly struct Gram : IMetricUnit, IMass
{
    public static Transformation ToSi(Transformation self) => self.RootedIn<Kilogram>() / 1000;

    public static String Representation => "g";
}
