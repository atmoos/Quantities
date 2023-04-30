using Quantities.Dimensions;
using Quantities.Prefixes;

namespace Quantities.Units.Si.Derived;

public readonly struct Gram : IMetricUnit<Milli>, IMass
{
    public static String Representation => "g";
}
