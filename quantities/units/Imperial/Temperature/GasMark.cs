using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Temperature;

// [K] ≡ [GM] × ​125⁄9 + 394.261 
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct GasMark : IImperialUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => self.FusedMultiplyAdd(125, 5d * 218d + 9 * 273.15d) / 9;
    public static String Representation => "GM";
}
