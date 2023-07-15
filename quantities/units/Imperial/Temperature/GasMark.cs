using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Temperature;

// [K] ≡ [GM] × ​125⁄9 + 394.261 
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct GasMark : IImperialUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => 125 * self / 9 + (5d * 218d / 9d) + 273.15;
    public static String Representation => "GM";
}
