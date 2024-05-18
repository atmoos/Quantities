using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si;

// See: https://en.wikipedia.org/wiki/SI_base_unit
public readonly struct Candela : ISiUnit, ILuminousIntensity
{
    public static String Representation => "cd";
}
