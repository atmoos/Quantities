using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.NonStandard.Angle;

// See: https://en.wikipedia.org/wiki/Gradian
[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct Gradian : INonStandardUnit, IAngle
{
    public static Transformation ToSi(Transformation self) => Math.PI * self.RootedIn<Radian>() / 200;

    public static String Representation => "gon";
}
