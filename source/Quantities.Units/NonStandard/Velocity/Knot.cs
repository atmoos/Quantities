using Quantities.Dimensions;
using Quantities.Units.NonStandard.Length;
using Quantities.Units.Si.Metric;
using static Quantities.Extensions;

namespace Quantities.Units.NonStandard.Velocity;

// https://en.wikipedia.org/wiki/Knot_(unit)
public readonly struct Knot : INonStandardUnit, IVelocity
{
    public static Transformation ToSi(Transformation self) => self.DerivedFrom<NauticalMile>() / ValueOf<Hour>();
    public static String Representation => "kn";
}
