using Quantities.Dimensions;

namespace Quantities.Units.Si.Derived;

public readonly struct Joule : ISiUnit, IEnergy<Kilogram, Metre, Second>
{
    public static String Representation => "J";
}
