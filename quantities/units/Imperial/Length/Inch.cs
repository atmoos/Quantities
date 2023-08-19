using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Inch : IImperialUnit, ILength
{
    public const Double ToMetre = 0.0254;
    public static Transformation ToSi(Transformation self) => ToMetre * self;
    public static String Representation => "in";
}
