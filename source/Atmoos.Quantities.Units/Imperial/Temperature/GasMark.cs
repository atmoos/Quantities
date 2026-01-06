using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Imperial.Temperature;

// [K] ≡ [GM] × ​125⁄9 + 394.261
// See: https://en.wikipedia.org/wiki/Gas_mark
public readonly struct GasMark : IImperialUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => self.FusedMultiplyAdd(125, 5d * 218d + 9 * 273.15d) / 9;

    public static String Representation => "GM";
}
