using Quantities.Dimensions;

namespace Quantities.Units.Si.Derived;

// [K] ≡ [°C] + 273.15
// Celsius is officially an SI derived unit.
public readonly struct Celsius : IMetricUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => self.RootedIn<Kelvin>() + 273.15d;
    public static String Representation => "°C";
}
