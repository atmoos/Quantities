using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric.UnitsOfInformation;

public readonly struct Byte : IMetricUnit, IAmountOfInformation
{
    public static Transformation ToSi(Transformation self) => 8 * self.DerivedFrom<Bit>();
    public static String Representation => "B";
}
