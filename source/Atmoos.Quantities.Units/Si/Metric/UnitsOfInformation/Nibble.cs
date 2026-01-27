using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Metric.UnitsOfInformation;

public readonly struct Nibble : IMetricUnit, IAmountOfInformation
{
    public static Transformation ToSi(Transformation self) => 4 * self.DerivedFrom<Bit>();

    public static String Representation => "N";
}
