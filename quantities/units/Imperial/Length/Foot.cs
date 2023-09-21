using Quantities.Dimensions;
using Quantities.Units.Si;

namespace Quantities.Units.Imperial.Length;

public readonly struct Foot : IImperialUnit<Foot, ILength>, ILength
{
    public static Transformation Derived(in From<ILength> from) => 3048 * from.Si<Metre>() / 10000;
    public static String Representation => "ft";
}
