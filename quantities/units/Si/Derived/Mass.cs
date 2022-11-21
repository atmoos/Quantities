using Quantities.Dimensions;
using Quantities.Prefixes;

namespace Quantities.Units.Si.Derived;

public readonly struct Gram : ISiDerivedUnit<Milli>, IMass
{
    public static String Representation => "g";
}
public readonly struct Tonne : ISiDerivedUnit<Kilo>, IMass
{
    public static String Representation => "t";
}
