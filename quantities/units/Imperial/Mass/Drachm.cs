using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Mass;

public readonly struct Drachm : IImperialUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 1.7718451953125 * self / 1e3;
    public static String Representation => "dr";
}
