using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Metric;

public readonly struct Bar : IMetricUnit, IPressure
{
    internal const Double ToPascal = 1e5; // bar -> Pa
    public static Transformation ToSi(Transformation value) => ToPascal * value;
    public static String Representation => "bar";
}
