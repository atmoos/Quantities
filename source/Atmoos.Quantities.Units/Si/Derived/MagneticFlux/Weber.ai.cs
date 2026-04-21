using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived.MagneticFlux;

// See: https://en.wikipedia.org/wiki/Weber_(unit)
[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct Weber : ISiUnit, IMagneticFlux
{
    public static String Representation => "Wb";
}
