using Quantities.Dimensions;

namespace Quantities.Unit.Si.Derived;

// [K] ≡ [°C] + 273.15
// Celsius is officially an SI derived unit.
public readonly struct Celsius : ISiDerivedUnit, ITransform, ITemperature
{
    private const Decimal kelvinOffset = 273.15m;
    public static Double ToSi(in Double nonSiValue) => (Double)((Decimal)nonSiValue + kelvinOffset);
    public static Double FromSi(in Double siValue) => (Double)((Decimal)siValue - kelvinOffset);
    public static String Representation => "°C";
}
