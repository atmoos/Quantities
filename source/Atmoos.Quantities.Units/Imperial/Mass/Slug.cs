using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Imperial.Mass;

public readonly struct Slug : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 14.59390294 * self;

    public static String Representation => "slug";
}
