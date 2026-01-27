using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Imperial.Mass;

public readonly struct Ton : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 1016.0469088 * self;

    public static String Representation => "t";
}
