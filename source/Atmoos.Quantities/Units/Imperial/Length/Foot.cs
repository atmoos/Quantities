using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si;

namespace Atmoos.Quantities.Units.Imperial.Length;

// See: https://en.wikipedia.org/wiki/Foot_(unit)
public readonly struct Foot : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 3048 * self.RootedIn<Metre>() / 1e4;
    public static String Representation => "ft";
}
