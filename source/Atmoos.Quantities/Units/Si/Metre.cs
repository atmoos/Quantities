using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si;

// See: https://en.wikipedia.org/wiki/SI_base_unit
// Spelling: https://en.wikipedia.org/wiki/Metre
public readonly struct Metre : ISiUnit, ILength
{
    public static String Representation => "m";
}
