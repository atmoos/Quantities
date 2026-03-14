using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Imperial.Power;

// See: https://en.wikipedia.org/wiki/Horsepower
public readonly struct HorsePower : IImperialUnit, IPower
{
    public static Transformation ToSi(Transformation self) => 76.0402249068 * 9.80665 * self.RootedIn<Watt>();

    public static String Representation => "hp";
}
