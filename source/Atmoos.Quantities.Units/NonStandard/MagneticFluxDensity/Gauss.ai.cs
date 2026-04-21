using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si.Derived.MagneticFluxDensity;

namespace Atmoos.Quantities.Units.NonStandard.MagneticFluxDensity;

// See: https://en.wikipedia.org/wiki/Gauss_(unit)
[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct Gauss : INonStandardUnit, IMagneticFluxDensity
{
    public static Transformation ToSi(Transformation self) => self.RootedIn<Tesla>() / 10000;

    public static String Representation => "G";
}
