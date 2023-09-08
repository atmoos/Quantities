using Quantities.Dimensions;
using Quantities.Units.NonStandard;

namespace Quantities.units.NonStandard.Velocity;

// https://en.wikipedia.org/wiki/Knot_(unit)
public readonly struct Knot : INoSystemUnit, IVelocity
{
    private const Double oneHour = 3600; // s
    private const Double oneNauticalMile = 1852; // m
    public static Transformation ToSi(Transformation self) => oneNauticalMile * self / oneHour;
    public static String Representation => "kn";
}
