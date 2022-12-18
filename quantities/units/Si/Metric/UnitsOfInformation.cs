using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

/* There is no SI unit for "amount of information".
Wikipedia seems to suggest it's best to define the 'bit' as the base unit.
This is what we'll use here...
--> https://en.wikipedia.org/wiki/Byte "unit derived from bit"
*/
public readonly struct Bit : IMetricUnit, IAmountOfInformation
{
    public static String Representation => "bit";
}
public readonly struct Nibble : IMetricUnit, IAmountOfInformation
{
    private const Double toBit = 4d;
    public static Double ToSi(in Double value) => value * toBit;
    public static Double FromSi(in Double value) => value / toBit;
    public static String Representation => "N";
}
public readonly struct Byte : IMetricUnit, IAmountOfInformation
{
    private const Double toBit = 8d;
    public static Double ToSi(in Double value) => value * toBit;
    public static Double FromSi(in Double value) => value / toBit;
    public static String Representation => "B";
}
