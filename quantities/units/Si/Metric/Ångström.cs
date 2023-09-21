using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Ångström : IMetricUnit<Ångström, ILength>, ILength
{
    public static Transformation Derived(in From<ILength> from) => from.Si<Metre>() / 1e10;
    public static String Representation => "Å";
}
