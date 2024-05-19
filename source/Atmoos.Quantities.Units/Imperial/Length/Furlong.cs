using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Imperial.Length;

public readonly struct Furlong : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => self.DerivedFrom<Mile>() / 8;
    public static String Representation => "fur";
}
