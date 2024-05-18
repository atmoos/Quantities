using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si;

// See: https://en.wikipedia.org/wiki/SI_base_unit
public readonly struct Mole : ISiUnit, IAmountOfSubstance
{
    public static String Representation => "mol";
}
