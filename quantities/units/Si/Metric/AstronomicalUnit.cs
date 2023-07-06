using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

// https://en.wikipedia.org/wiki/Astronomical_unit
public readonly struct AstronomicalUnit : IMetricUnit, ILength
{
    internal const Double astronomicalUnitToMetre = 149597870700; // au -> m
    public static Double ToSi(in Double self) => astronomicalUnitToMetre * self;
    public static Double FromSi(in Double value) => value / astronomicalUnitToMetre;
    public static String Representation => "au";
}
