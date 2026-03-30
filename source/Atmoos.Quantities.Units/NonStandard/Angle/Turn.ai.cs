using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.NonStandard.Angle;

// See: https://en.wikipedia.org/wiki/Turn_(angle)
[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct Turn : INonStandardUnit, IAngle
{
    public static Transformation ToSi(Transformation self) => Math.Tau * self.RootedIn<Radian>();

    public static String Representation => "rev";
}
