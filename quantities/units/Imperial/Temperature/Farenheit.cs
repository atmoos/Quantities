using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Temperature;

// [K] ≡ ([°F] + 459.67) × ​5⁄9
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct Fahrenheit : IImperialUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => 5 * (self + 459.67d) / 9;
    public static String Representation => "°F";
}
