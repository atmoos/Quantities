using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Power;

// See: https://en.wikipedia.org/wiki/Horsepower
public readonly struct HorsePower : IImperialUnit, IPower
{
    internal const Double InWatt = 76.0402249 * 9.80665; // ~745.700 W in 1 hp
    public static Transformation ToSi(Transformation self) => InWatt * self;
    public static String Representation => "hp";
}
