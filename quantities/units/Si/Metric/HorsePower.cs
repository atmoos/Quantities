using Quantities.Dimensions;
using Quantities.Units.Si.Derived;

namespace Quantities.Units.Si.Metric;

// See: https://en.wikipedia.org/wiki/Horsepower
public readonly struct HorsePower : IMetricUnit<HorsePower, IPower>, IPower
{
    public static Transformation Derived(in From<IPower> basis) => 75 * basis.Si<Watt>() * 9.80665;
    public static String Representation => "hp";
}
