using Quantities.Dimensions;
using Quantities.Units.NonStandard;

namespace Quantities.units.NonStandard.Velocity;

// https://en.wikipedia.org/wiki/Knot_(unit)
public readonly struct Knot : INoSystemUnit, IVelocity
{
    private const Double metres = 1852;
    private const Double seconds = 3600;
    public static Transformation ToSi(Transformation self) => metres * self / seconds;
    public static String Representation => "kn";
}
