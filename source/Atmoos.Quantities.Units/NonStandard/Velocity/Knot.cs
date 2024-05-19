using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.NonStandard.Length;
using Atmoos.Quantities.Units.Si.Metric;
using static Atmoos.Quantities.Extensions;

namespace Atmoos.Quantities.Units.NonStandard.Velocity;

// https://en.wikipedia.org/wiki/Knot_(unit)
public readonly struct Knot : INonStandardUnit, IVelocity
{
    public static Transformation ToSi(Transformation self) => self.DerivedFrom<NauticalMile>() / ValueOf<Hour>();
    public static String Representation => "kn";
}
