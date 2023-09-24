using Quantities.Dimensions;
using Quantities.Units.Si;

namespace Quantities.Units.NonStandard.Temperature;

// [K] = ([°Rø] − 7.5) × ​40⁄21 + 273.15
// See: https://en.wikipedia.org/wiki/Conversion_of_scales_of_temperature
public readonly struct Rømer : INonStandardUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => 40 * (self.RootedIn<Kelvin>() - 7.5) / 21 + 273.15;
    public static String Representation => "°Rø";
}
