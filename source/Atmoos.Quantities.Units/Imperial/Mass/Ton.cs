using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Imperial.Mass;

// See: https://en.wikipedia.org/wiki/Long_ton
public readonly struct Ton : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 20 * 8 * 14 * self.DerivedFrom<Pound>();

    public static String Representation => "LT";
}
