using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Furlong : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 201.168 * self;
    public static String Representation => "fur";
}
