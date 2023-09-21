using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

// https://en.wikipedia.org/wiki/Astronomical_unit
public readonly struct AstronomicalUnit : IMetricUnit<AstronomicalUnit, ILength>, ILength
{
    public static Transformation Derived(in From<ILength> from) => 149597870700 * from.Si<Metre>();
    public static String Representation => "au";
}
