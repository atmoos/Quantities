using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Imperial.Length;

public readonly struct Chain : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 66 * self.DerivedFrom<Foot>();

    public static String Representation => "ch";
}
