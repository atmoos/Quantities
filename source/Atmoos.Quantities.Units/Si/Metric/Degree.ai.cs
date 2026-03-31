using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Si.Metric;

// See: https://en.wikipedia.org/wiki/Degree_(angle)
[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct Degree : IMetricUnit, IAngle
{
    public static Transformation ToSi(Transformation self) => Math.PI * self.RootedIn<Radian>() / 180;

    public static String Representation => "°";
}
