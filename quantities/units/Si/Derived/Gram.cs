using Quantities.Dimensions;

namespace Quantities.Units.Si.Derived;

public readonly struct Gram : IMetricUnit<Gram, IMass>, IMass
{
    public static Transformation Derived(in From<IMass> from) => from.Si<Kilogram>() / 1000;
    public static String Representation => "g";

}
