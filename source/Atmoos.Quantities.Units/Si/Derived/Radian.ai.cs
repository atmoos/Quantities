using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived;

// See: https://en.wikipedia.org/wiki/Radian
[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct Radian : ISiUnit, IAngle
{
    public static String Representation => "rad";
}
