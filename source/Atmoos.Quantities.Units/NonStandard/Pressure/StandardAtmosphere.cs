using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.NonStandard.Pressure;

public readonly struct StandardAtmosphere : INonStandardUnit, IPressure
{
    public static Transformation ToSi(Transformation value) => 101325 * value.RootedIn<Pascal>();

    public static String Representation => "atm";
}
