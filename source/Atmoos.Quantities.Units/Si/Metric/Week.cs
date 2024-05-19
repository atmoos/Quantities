using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Metric;

public readonly struct Week : IMetricUnit, ITime
{
    public static Transformation ToSi(Transformation self) => 7 * self.DerivedFrom<Day>();
    public static String Representation => "w";
}
