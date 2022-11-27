using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Power;

// See: https://en.wikipedia.org/wiki/Horsepower
public readonly struct HorsePower : IImperial, IPower
{
    internal const Double InWatt = 76.0402249 * 9.80665; // ~745.700 W in 1 hp
    public static Double FromSi(in Double value) => value / InWatt;
    public static Double ToSi(in Double self) => InWatt * self;
    public static String Representation => "hp";
}
