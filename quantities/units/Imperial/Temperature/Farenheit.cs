using Quantities.Dimensions;

namespace Quantities.Unit.Imperial.Temperature;

// [K] ≡ ([°F] + 459.67) × ​5⁄9
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct Fahrenheit : IImperial, ITemperature
{
    private const Decimal kelvinOffset = 459.67m;
    private const Decimal scaleFromKelvin = 9m / 5m;
    public static Double ToSi(in Double nonSiValue) => (Double)(5m * ((Decimal)nonSiValue + kelvinOffset) / 9m);
    public static Double FromSi(in Double siValue) => (Double)((scaleFromKelvin * (Decimal)siValue) - kelvinOffset);
    public static String Representation => "°F";
}
