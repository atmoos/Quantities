using Quantities.Dimensions;
using Quantities.Units.NonStandard;

namespace Quantities.units.NonStandard.Length;

// https://en.wikipedia.org/wiki/Nautical_mile
public readonly struct NauticalMile : INoSystemUnit, ILength
{
    private const Double toMetres = 1852;
    public static Double FromSi(in Double value) => value / toMetres;
    public static Double ToSi(in Double self) => self * toMetres;
    public static String Representation => "nmi";
}
