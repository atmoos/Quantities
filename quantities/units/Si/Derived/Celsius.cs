using Quantities.Dimensions;

namespace Quantities.Units.Si.Derived;

// [K] ≡ [°C] + 273.15
// Celsius is officially an SI derived unit.
public readonly struct Celsius : IMetricUnit, ITemperature
{
    private const Decimal kelvinOffset = 273.15m;
    static Double ITransform.ToSi(in Double nonSiValue) => (Double)((Decimal)nonSiValue + kelvinOffset);
    static Double ITransform.FromSi(in Double siValue) => (Double)((Decimal)siValue - kelvinOffset);
    public static String Representation => "°C";
}
