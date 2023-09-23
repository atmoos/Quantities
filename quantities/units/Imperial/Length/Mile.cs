using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Mile : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 5280 * self.DerivedFrom<Foot>();
    public static String Representation => "mi";
}
