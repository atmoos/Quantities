using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Mass;

public readonly struct Ounce : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 28.349523125 * self / 1e3;
    public static String Representation => "oz";
}
