using Quantities.Dimensions;

namespace Quantities.Units.Si.Metric;

public readonly struct Minute : IMetricUnit, ITime
{
    internal const Double toSeconds = 60; // min -> s
    public static Transformation ToSi(Transformation self) => self * toSeconds;
    public static String Representation => "m";
}
public readonly struct Hour : IMetricUnit, ITime
{
    internal const Double toSeconds = 60 * Minute.toSeconds; // Hour -> s
    public static Transformation ToSi(Transformation self) => self * toSeconds;
    public static String Representation => "h";
}
public readonly struct Day : IMetricUnit, ITime
{
    internal const Double toSeconds = 24 * Hour.toSeconds; // Day -> s
    public static Transformation ToSi(Transformation self) => self * toSeconds;
    public static String Representation => "d";
}
public readonly struct Week : IMetricUnit, ITime
{
    internal const Double toSeconds = 7 * Day.toSeconds; // Week -> s
    public static Transformation ToSi(Transformation self) => self * toSeconds;
    public static String Representation => "w";
}
