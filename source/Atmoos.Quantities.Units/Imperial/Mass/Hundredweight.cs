using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Imperial.Mass;

public readonly struct Hundredweight : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 50.80234544 * self;

    public static String Representation => "cwt";
}
