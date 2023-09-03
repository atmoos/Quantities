using Quantities.Numerics;

namespace Quantities.Measures;

internal interface IDimension
{
    internal static abstract Polynomial Pow(in Polynomial value);
    public static abstract String Representation { get; }
}
internal readonly struct Linear : IDimension
{
    public static Polynomial Pow(in Polynomial value) => value;
    public static String Representation => String.Empty;
}
internal readonly struct Square : IDimension
{
    public static Polynomial Pow(in Polynomial value) => value * value;
    public static String Representation => "²";
}
internal readonly struct Cubic : IDimension
{
    public static Polynomial Pow(in Polynomial value) => value * value * value;
    public static String Representation => "³";
}
