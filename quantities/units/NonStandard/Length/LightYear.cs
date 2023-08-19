using Quantities.Dimensions;

namespace Quantities.Units.NonStandard.Length;

// https://en.wikipedia.org/wiki/Light-year
public readonly struct LightYear : INoSystemUnit, ILength
{
    internal const Double lightYearToMetre = 9460730472580800; // ly -> m
    public static Transformation ToSi(Transformation self) => lightYearToMetre * self;
    public static String Representation => "ly";
}
