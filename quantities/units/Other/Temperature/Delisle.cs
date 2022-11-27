using Quantities.Dimensions;

namespace Quantities.Units.Other.Temperature;

// [K] = 373.15 − [°De] × ​2⁄3
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct Delisle : ITransform, IOther, ITemperature
{
    public static Double ToSi(in Double nonSiValue) => (Double)(373.15m - (2m * (Decimal)nonSiValue / 3m));
    public static Double FromSi(in Double siValue) => (Double)(-1.5m * ((Decimal)siValue - 373.15m));
    public static String Representation => "°De";
}
