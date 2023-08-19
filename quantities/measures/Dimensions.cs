namespace Quantities.Measures;

internal interface IDimension
{
    internal static abstract Transformation Pow(Transformation value);
    public static abstract String Representation { get; }
}
internal readonly struct Linear : IDimension
{
    public static Transformation Pow(Transformation value) => value;
    public static String Representation => String.Empty;
}
internal readonly struct Square : IDimension
{
    public static Transformation Pow(Transformation value) => value.Pow(2);
    public static String Representation => "²";
}
internal readonly struct Cubic : IDimension
{
    public static Transformation Pow(Transformation value) => value.Pow(3);
    public static String Representation => "³";
}
