using Quantities.Dimensions;

namespace Quantities.Units.NonStandard.Length;

// https://en.wikipedia.org/wiki/Light-year
public readonly struct LightYear : INoSystemUnit, ILength
{
    internal const Double lightYearToMetre = 9460730472580800; // ly -> m
    public static Double ToSi(in Double self) => lightYearToMetre * self;
    public static Double FromSi(in Double value) => value / lightYearToMetre;
    public static String Representation => "ly";
}
