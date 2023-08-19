using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Mass;

public readonly struct Quarter : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 12.70058636 * self;
    public static String Representation => "qr";
}
