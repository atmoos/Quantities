using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Yard : IImperialUnit<Yard, ILength>, ILength
{
    public static Transformation Derived(in From<ILength> from) => 3 * from.Imperial<Foot>();
    public static String Representation => "yd";
}
