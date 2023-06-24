namespace Quantities.Measures;

internal interface IDimension
{
    internal static abstract Double Pow(in Double value);
    public static abstract String Representation { get; }
}
internal readonly struct Linear : IDimension
{
    public static Double Pow(in Double value) => value;
    public static String Representation => String.Empty;
}
internal readonly struct Square : IDimension
{
    public static Double Pow(in Double value) => value * value;
    public static String Representation => "²";
}
internal readonly struct Cubic : IDimension
{
    public static Double Pow(in Double value) => value * value * value;
    public static String Representation => "³";
}
