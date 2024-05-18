using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived;

// [K] ≡ [°C] + 273.15
// Celsius is officially an SI derived unit.
// See: - https://en.wikipedia.org/wiki/Celsius
//      - https://en.wikipedia.org/wiki/International_System_of_Units#Derived_units
public readonly struct Celsius : IMetricUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => self.RootedIn<Kelvin>() + 273.15d;
    public static String Representation => "°C";
}
