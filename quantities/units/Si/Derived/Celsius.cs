using Quantities.Dimensions;

namespace Quantities.Units.Si.Derived;

// [K] ≡ [°C] + 273.15
// Celsius is officially an SI derived unit.
public readonly struct Celsius : IMetricUnit, ITemperature
{
    private const Double kelvinOffset = 273.15d;
    public static Transformation ToSi(Transformation self) => self + kelvinOffset;
    public static String Representation => "°C";
}
