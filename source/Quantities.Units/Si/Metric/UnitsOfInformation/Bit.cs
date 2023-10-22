using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric.UnitsOfInformation;

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
