using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Mile : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 1609.344 * self;
    public static String Representation => "mi";
}
