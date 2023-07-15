using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Mass;

public readonly struct Grain : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 64.79891e-6 * self;
    public static String Representation => "gr";
}
