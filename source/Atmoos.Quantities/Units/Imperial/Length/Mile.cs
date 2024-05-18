using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Imperial.Length;

// See: https://en.wikipedia.org/wiki/Mile
public readonly struct Mile : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 5280 * self.DerivedFrom<Foot>();
    public static String Representation => "mi";
}
