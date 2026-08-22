using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived;

// See: https://en.wikipedia.org/wiki/Steradian
[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct Steradian : ISiUnit, ISolidAngle
{
    public static String Representation => "sr";
}
