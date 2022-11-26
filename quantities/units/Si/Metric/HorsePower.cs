using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

// See: https://en.wikipedia.org/wiki/Horsepower
public readonly struct HorsePower : IMetricUnit, IPower
{
    internal const Double InWatt = 75d * 9.80665; // 735.49875 W in 1 hp
    public static Double FromSi(in Double value) => value / InWatt;
    public static Double ToSi(in Double self) => InWatt * self;
    public static String Representation => "hp";
}
