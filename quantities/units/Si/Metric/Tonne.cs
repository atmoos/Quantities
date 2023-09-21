using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Tonne : IMetricUnit<Tonne, IMass>, IMass
{
    public static Transformation Derived(in From<IMass> from) => 1000 * from.Si<Kilogram>();
    public static String Representation => "t";

}
