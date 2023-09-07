using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Mass;

public readonly struct Grain : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 64.79891 * self / 1e6;
    public static String Representation => "gr";
}
