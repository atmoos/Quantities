using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

// https://en.wikipedia.org/wiki/Rod_(unit)
// https://en.wikipedia.org/wiki/Imperial_units
public readonly struct Rod : IImperialUnit<Rod, ILength>, ILength
{
    public static Transformation Derived(in From<ILength> from) => 66 * from.Imperial<Foot>() / 4;
    public static String Representation => "rod";
}
