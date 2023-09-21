using Quantities.Dimensions;

namespace Quantities.Units.Si.Derived;

// [K] ≡ [°C] + 273.15
// Celsius is officially an SI derived unit.
public readonly struct Celsius : IMetricUnit<Celsius, ITemperature>, ITemperature
{
    public static Transformation Derived(in From<ITemperature> from) => from.Si<Kelvin>() + 273.15d;
    public static String Representation => "°C";
}
