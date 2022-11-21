using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Other.Temperature;

// [K] = [°N] × ​100⁄33 + 273.15
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct Newton : IOther, ITemperature
{
    private static readonly LinearTransform transform = new(100m, 33m, 273.15m);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "°N";
}
