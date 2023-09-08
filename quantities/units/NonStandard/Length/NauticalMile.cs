using Quantities.Dimensions;
using Quantities.Units.NonStandard;

namespace Quantities.units.NonStandard.Length;

// https://en.wikipedia.org/wiki/Nautical_mile
public readonly struct NauticalMile : INoSystemUnit, ILength
{
    private const Double oneNauticalMile = 1852; // m
    public static Transformation ToSi(Transformation self) => oneNauticalMile * self;
    public static String Representation => "nmi";
}
