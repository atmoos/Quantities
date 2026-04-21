using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived.Capacitance;

// See: https://en.wikipedia.org/wiki/Farad
[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct Farad : ISiUnit, ICapacitance
{
    public static String Representation => "F";
}
