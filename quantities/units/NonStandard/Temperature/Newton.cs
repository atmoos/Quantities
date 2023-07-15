using Quantities.Dimensions;

namespace Quantities.Units.NonStandard.Temperature;

// [K] = [°N] × ​100⁄33 + 273.15
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct Newton : INoSystemUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => 100 * self / 33 + 273.15;
    public static String Representation => "°N";
}
