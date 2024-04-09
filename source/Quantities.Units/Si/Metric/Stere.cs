using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

// https://en.wikipedia.org/wiki/Stere
// https://en.wikipedia.org/wiki/List_of_metric_units
public readonly struct Stere : IMetricUnit, IVolume, IAlias<ILength>
{
    // one stere is defined as one cubic metre.
    public static Transformation ToSi(Transformation value) => value;
    static T IAlias<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();
    public static String Representation => "st";
}
