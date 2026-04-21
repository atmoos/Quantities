using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si.Derived.Illuminance;

namespace Atmoos.Quantities.Units.NonStandard.Illuminance;

// See: https://en.wikipedia.org/wiki/Foot-candle
[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct FootCandle : INonStandardUnit, IIlluminance
{
    public static Transformation ToSi(Transformation self) => 100000000 * self.RootedIn<Lux>() / 9290304;

    public static String Representation => "fc";
}
