using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Chain : IImperialUnit<Chain, ILength>, ILength
{
    public static Transformation Derived(in From<ILength> from) => 22 * from.Imperial<Yard>();
    public static String Representation => "ch";
}
