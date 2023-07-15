using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Mass;

public readonly struct Pound : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 0.45359237 * self;
    public static String Representation => "lb";
}
