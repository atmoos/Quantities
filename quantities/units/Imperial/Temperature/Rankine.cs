using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Temperature;

// [K] ≡ [°R] × 5/9
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct Rankine : IImperialUnit, ITemperature
{
    public static Double ToSi(in Double nonSiValue) => (Double)(5m * (Decimal)nonSiValue / 9m);
    public static Double FromSi(in Double siValue) => (Double)(9m * (Decimal)siValue / 5m);
    public static String Representation => "°R";
}
