using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Imperial.Force;

public readonly struct PoundForce : IImperialUnit, IForce
{
    public static Transformation ToSi(Transformation self) => 8896443230521 * self.RootedIn<Newton>() / 2000000000000;

    public static String Representation => "lbf";
}
