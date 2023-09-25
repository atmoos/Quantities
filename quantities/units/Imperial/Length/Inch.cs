using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Inch : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => self.DerivedFrom<Foot>() / 12;
    public static String Representation => "in";
}
