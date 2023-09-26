using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Day : IMetricUnit, ITime
{
    public static Transformation ToSi(Transformation self) => 24 * self.DerivedFrom<Hour>();
    public static String Representation => "d";
}

public readonly struct Week : IMetricUnit, ITime
{
    public static Transformation ToSi(Transformation self) => 7 * self.DerivedFrom<Day>();
    public static String Representation => "w";
}
