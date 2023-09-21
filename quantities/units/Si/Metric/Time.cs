using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Minute : IMetricUnit<Minute, ITime>, ITime
{
    public static Transformation Derived(in From<ITime> from) => 60 * from.Si<Second>();
    public static String Representation => "m";
}
public readonly struct Hour : IMetricUnit<Hour, ITime>, ITime
{
    public static Transformation Derived(in From<ITime> from) => 60 * from.Metric<Minute>();
    public static String Representation => "h";
}
public readonly struct Day : IMetricUnit<Day, ITime>, ITime
{
    public static Transformation Derived(in From<ITime> from) => 24 * from.Metric<Hour>();
    public static String Representation => "d";
}
public readonly struct Week : IMetricUnit<Week, ITime>, ITime
{
    public static Transformation Derived(in From<ITime> from) => 7 * from.Metric<Day>();
    public static String Representation => "w";
}
