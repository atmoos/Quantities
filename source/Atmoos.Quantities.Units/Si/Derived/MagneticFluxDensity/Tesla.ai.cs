using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived.MagneticFluxDensity;

// See: https://en.wikipedia.org/wiki/Tesla_(unit)
[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct Tesla : ISiUnit, IMagneticFluxDensity
{
    public static String Representation => "T";
}
