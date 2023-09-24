using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Mass;

public readonly struct Pound : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 45359237 * self / 100000000;
    public static String Representation => "lb";
}
