using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Mass;

public readonly struct Ounce : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 28.349523125e-3 * self;
    public static String Representation => "oz";
}
