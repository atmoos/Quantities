using Quantities.Dimensions;
using Quantities.Prefixes;

namespace Quantities.Units.Si.Derived;

public readonly struct Gram : ISiDerivedUnit<Milli>, IMass
{
    public static String Representation => "g";
}
