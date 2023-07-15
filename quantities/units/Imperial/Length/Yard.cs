using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Yard : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 0.9144 * self;
    public static String Representation => "yd";
}
