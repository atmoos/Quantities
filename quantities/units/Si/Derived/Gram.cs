using Quantities.Dimensions;

namespace Quantities.Units.Si.Derived;

public readonly struct Gram : IMetricUnit, IMass
{
    public static Transformation ToSi(Transformation self) => self.RootedIn<Metre>() / 1000;
    public static String Representation => "g";

}
