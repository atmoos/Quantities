using Quantities.Dimensions;

namespace Quantities.Units.NonStandard.Acceleration;

// See: https://en.wikipedia.org/wiki/Standard_gravity
public class StandardGravity : INonStandardUnit, IAcceleration
{
    public static Transformation ToSi(Transformation self) => 9.80665 * self; // 9.80665 m/s2
    public static String Representation => "gₙ";
}
