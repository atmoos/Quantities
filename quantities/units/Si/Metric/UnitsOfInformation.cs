using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

/* There is no SI unit for "amount of information".
Wikipedia seems to suggest it's best to define the 'bit' as the base unit.
This is what we'll use here...
--> https://en.wikipedia.org/wiki/Byte "unit derived from bit"
*/
public readonly struct Bit : IMetricUnit, IAmountOfInformation
{
    public static Transformation ToSi(Transformation self) => self;
    public static String Representation => "bit";

}
public readonly struct Nibble : IMetricUnit, IAmountOfInformation
{
    public static Transformation ToSi(Transformation self) => 4 * self.DerivedFrom<Bit>();
    public static String Representation => "N";
}
public readonly struct Byte : IMetricUnit, IAmountOfInformation
{
    public static Transformation ToSi(Transformation self) => 8 * self.DerivedFrom<Bit>();
    public static String Representation => "B";
}
