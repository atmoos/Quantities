using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Si.Metric;

public readonly struct Minute : IMetricUnit, ITime
{
    internal const Double ToSeconds = 60; // min -> s
    private static readonly Transform transform = new(ToSeconds);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "m";
}
public readonly struct Hour : IMetricUnit, ITime
{
    internal const Double ToSeconds = 60 * Minute.ToSeconds; // Hour -> s
    private static readonly Transform transform = new(ToSeconds);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "h";
}
public readonly struct Day : IMetricUnit, ITime
{
    internal const Double ToSeconds = 24 * Hour.ToSeconds; // Day -> s
    private static readonly Transform transform = new(ToSeconds);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "d";
}
public readonly struct Week : IMetricUnit, ITime
{
    internal const Double ToSeconds = 7 * Day.ToSeconds; // Week -> s
    private static readonly Transform transform = new(ToSeconds);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "w";
}
