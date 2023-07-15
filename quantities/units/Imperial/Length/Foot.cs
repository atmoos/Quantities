using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Foot : IImperialUnit, ILength
{
    public const Double ToMetre = 0.3048;
    public static Transformation ToSi(Transformation self) => ToMetre * self;
    public static String Representation => "ft";
}
