using Quantities.Dimensions;

namespace Quantities.Units.NonStandard.Temperature;

// [K] = ([°Rø] − 7.5) × ​40⁄21 + 273.15
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct Rømer : INoSystemUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => 40 * (self - 7.5) / 21 + 273.15;
    public static String Representation => "°Rø";
}
