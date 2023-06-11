using Quantities.Dimensions;

namespace Quantities.Units.Si.Derived;

public sealed class Joule : ISiUnit, IEnergy<Kilogram, Metre, Second>
{
    public static String Representation => "J";
}
