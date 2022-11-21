using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Temperature;

// [K] ≡ [GM] × ​125⁄9 + 394.261 
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct GasMark : IImperial, ITemperature
{
    private static readonly LinearTransform transform = new(125m, 9m, (5m * 218m / 9m) + 273.15m);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "GM";
}
