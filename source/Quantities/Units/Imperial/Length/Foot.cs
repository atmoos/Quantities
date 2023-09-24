using Quantities.Dimensions;
using Quantities.Units.Si;

namespace Quantities.Units.Imperial.Length;

public readonly struct Foot : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 3048 * self.RootedIn<Metre>() / 1e4;
    public static String Representation => "ft";
}
