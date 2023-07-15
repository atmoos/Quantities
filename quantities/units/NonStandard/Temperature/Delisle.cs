using Quantities.Dimensions;

namespace Quantities.Units.NonStandard.Temperature;

// [K] = 373.15 − [°De] × ​2⁄3
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct Delisle : ITransform, INoSystemUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => -2d * self / 3d + 373.15;
    public static String Representation => "°De";
}
