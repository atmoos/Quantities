using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si;

namespace Atmoos.Quantities.Units.Imperial.Temperature;

// [K] ≡ [°R] × 5/9
// See: https://en.wikipedia.org/wiki/Conversion_of_scales_of_temperature
public readonly struct Rankine : IImperialUnit, ITemperature
{
    public static Transformation ToSi(Transformation self) => 5d * self.RootedIn<Kelvin>() / 9d;
    public static String Representation => "°R";
}
