using Quantities.Dimensions;
using Quantities.Units.Si;

namespace Quantities.Units.NonStandard.Temperature;

// [K] = [°N] × ​100⁄33 + 273.15
// See: https://en.wikipedia.org/wiki/Conversion_of_scales_of_temperature
public readonly struct Newton : INonStandardUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => 100d / 33 * self.RootedIn<Kelvin>() + 273.15;
    public static String Representation => "°N";
}
