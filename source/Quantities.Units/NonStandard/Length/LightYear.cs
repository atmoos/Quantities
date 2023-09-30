using Quantities.Dimensions;
using Quantities.Units.Si;

namespace Quantities.Units.NonStandard.Length;

// https://en.wikipedia.org/wiki/Light-year
public readonly struct LightYear : INonStandardUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 9460730472580800 * self.RootedIn<Metre>();
    public static String Representation => "ly";
}
