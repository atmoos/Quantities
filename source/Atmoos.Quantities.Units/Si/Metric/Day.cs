using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Metric;

public readonly struct Day : IMetricUnit, ITime
{
    public static Transformation ToSi(Transformation self) => 24 * self.DerivedFrom<Hour>();
    public static String Representation => "d";
}
