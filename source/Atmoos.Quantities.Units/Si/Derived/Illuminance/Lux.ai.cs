using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived.Illuminance;

// See: https://en.wikipedia.org/wiki/Lux
[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct Lux : ISiUnit, IIlluminance
{
    public static String Representation => "lx";
}
