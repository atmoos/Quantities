using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Temperature;

// [K] ≡ [GM] × ​125⁄9 + 394.261 
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct GasMark : IImperialUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => 125 * self / 9 + (Double)(5m * 218m / 9m + 273.15m);
    public static String Representation => "GM";
}
