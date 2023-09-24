using Quantities.Dimensions;
using Quantities.Units.Si;

namespace Quantities.Units.NonStandard.Temperature;

// [K] = 373.15 − [°De] × ​2⁄3
// See: https://en.wikipedia.org/wiki/Conversion_of_scales_of_temperature
public readonly struct Delisle : ITransform, INonStandardUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => -2d / 3 * self.RootedIn<Kelvin>() + 373.15;
    public static String Representation => "°De";
}
