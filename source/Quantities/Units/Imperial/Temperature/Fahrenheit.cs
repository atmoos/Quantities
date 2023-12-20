using Quantities.Dimensions;
using Quantities.Units.Si;

namespace Quantities.Units.Imperial.Temperature;

// [K] ≡ ([°F] + 459.67) × ​5⁄9
// See: - https://en.wikipedia.org/wiki/Fahrenheit
//      - https://en.wikipedia.org/wiki/Conversion_of_scales_of_temperature
public readonly struct Fahrenheit : IImperialUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => 5 * (self.RootedIn<Kelvin>() + 459.67) / 9;
    public static String Representation => "°F";
}
