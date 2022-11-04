using Quantities.Dimensions;

namespace Quantities.Unit.Other.Temperature;

// [K] = ([°Rø] − 7.5) × ​40⁄21 + 273.15
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct Rømer : IOther, ITemperature
{
    private const Decimal scaleFromSi = 21m / 40m;
    public static Double ToSi(in Double nonSiValue) => (Double)((40m * ((Decimal)nonSiValue - 7.5m) / 21m) + 273.15m);
    public static Double FromSi(in Double siValue) => (Double)((scaleFromSi * ((Decimal)siValue - 273.15m)) + 7.5m);
    public static String Representation => "°Rø";
}
