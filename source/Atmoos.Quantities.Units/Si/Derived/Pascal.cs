using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived;

// See: - https://en.wikipedia.org/wiki/Pascal_(unit)
//      - https://en.wikipedia.org/wiki/International_System_of_Units#Derived_units
public readonly struct Pascal : ISiUnit, IPressure
{
    public static String Representation => "Pa";
}
