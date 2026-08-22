using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived.LuminousFlux;

// See: https://en.wikipedia.org/wiki/Lumen_(unit)
[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct Lumen : ISiUnit, ILuminousFlux
{
    public static String Representation => "lm";
}
