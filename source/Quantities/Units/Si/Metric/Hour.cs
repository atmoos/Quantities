using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Hour : IMetricUnit, ITime
{
    public static Transformation ToSi(Transformation self) => 60 * self.DerivedFrom<Minute>();
    public static String Representation => "h";
}
