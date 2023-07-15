using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Temperature;

// [K] ≡ [°R] × 5/9
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct Rankine : IImperialUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => 5d * self / 9d;
    public static String Representation => "°R";
}
