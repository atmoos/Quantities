using Quantities.Dimensions;
using Quantities.Prefixes;

namespace Quantities.Units.Si.Metric;

public readonly struct Tonne : IMetricUnit<Kilo>, IMass
{
    public static String Representation => "t";
}
