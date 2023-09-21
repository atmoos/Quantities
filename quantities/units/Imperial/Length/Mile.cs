using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Mile : IImperialUnit<Mile, ILength>, ILength
{
    public static Transformation Derived(in From<ILength> from) => 5280 * from.Imperial<Foot>();
    public static String Representation => "mi";
}
