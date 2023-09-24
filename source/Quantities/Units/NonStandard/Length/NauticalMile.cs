using Quantities.Dimensions;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.units.NonStandard.Length;

// https://en.wikipedia.org/wiki/Nautical_mile
public readonly struct NauticalMile : INoSystemUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 1852 * self.RootedIn<Metre>();
    public static String Representation => "nmi";
}
