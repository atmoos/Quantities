using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si;

// See: https://en.wikipedia.org/wiki/SI_base_unit
public readonly struct Ampere : ISiUnit, IElectricCurrent
{
    public static String Representation => "A";
}
