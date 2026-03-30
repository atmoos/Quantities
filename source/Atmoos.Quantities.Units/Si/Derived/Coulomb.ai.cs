using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived;

// See: https://en.wikipedia.org/wiki/Coulomb
[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct Coulomb : ISiUnit, IElectricCharge
{
    public static String Representation => "C";
}
