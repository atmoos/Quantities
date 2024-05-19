using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Imperial.Length;

public readonly struct Yard : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 3 * self.DerivedFrom<Foot>();
    public static String Representation => "yd";
}
