using Quantities.Dimensions;

namespace Quantities.Units.Si.Derived;

public sealed class Joule : ISiDerivedUnit, IEnergy<Kilogram, Metre, Second>
{
    public static String Representation => "J";
}
