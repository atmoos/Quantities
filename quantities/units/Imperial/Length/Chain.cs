using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Chain : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 20.1168 * self;
    public static String Representation => "ch";
}
