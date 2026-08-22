using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.NonStandard.DynamicViscosity;

// See: https://en.wikipedia.org/wiki/Poise_(unit)
[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct Poise : INonStandardUnit, IDynamicViscosity
{
    public static Transformation ToSi(Transformation self) => self / 10;

    public static String Representation => "P";
}
