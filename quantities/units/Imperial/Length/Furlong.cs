using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Furlong : IImperialUnit<Furlong, ILength>, ILength
{
    public static Transformation Derived(in From<ILength> from) => from.Imperial<Mile>() / 8;
    public static String Representation => "fur";
}
