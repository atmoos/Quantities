using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Imperial.Mass;

// See: https://en.wikipedia.org/wiki/Pound_(mass)
public readonly struct Pound : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 45359237 * self / 100000000;

    public static String Representation => "lb";
}
