using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

// https://en.wikipedia.org/wiki/Stere
// https://en.wikipedia.org/wiki/List_of_metric_units
public readonly struct Stere : IMetricUnit, IVolume, IInjectUnit<ILength>
{
    // one stere is defined as one cubic metre.
    public static Double ToSi(in Double value) => value;
    public static Double FromSi(in Double value) => value;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        return inject.Si<Metre>(in self);
    }
    public static String Representation => "st";
}
