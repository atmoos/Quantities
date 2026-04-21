using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si.Derived.MagneticFlux;

namespace Atmoos.Quantities.Units.NonStandard.MagneticFlux;

// See: https://en.wikipedia.org/wiki/Maxwell_(unit)
[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct Maxwell : INonStandardUnit, IMagneticFlux
{
    public static Transformation ToSi(Transformation self) => self.RootedIn<Weber>() / 100000000;

    public static String Representation => "Mx";
}
