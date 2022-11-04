using Quantities.Dimensions;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Other.Temperature;

// [K] = [°Ré] × ​5⁄4 + 273.15
// See: https://en.wikipedia.org/wiki/Conversion_of_units#Temperature
public readonly struct Réaumur : IOther, ITemperature
{
    private static readonly LinearTransform transform = new(5m, 4m, 273.15m);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "°Ré";
}
