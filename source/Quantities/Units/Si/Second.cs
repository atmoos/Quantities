using Quantities.Dimensions;

namespace Quantities.Units.Si;

// See: https://en.wikipedia.org/wiki/SI_base_unit
public readonly struct Second : ISiUnit, ITime
{
    public static String Representation => "s";
}
