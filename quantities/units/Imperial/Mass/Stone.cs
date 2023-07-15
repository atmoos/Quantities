using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Mass;

public readonly struct Stone : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 6.35029318 * self;
    public static String Representation => "st";
}
