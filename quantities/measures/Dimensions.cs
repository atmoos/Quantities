namespace Quantities.Measures;

internal interface IDimension
{
    internal static abstract Int32 Exponent { get; }
    public static abstract String Representation { get; }
}
internal readonly struct Linear : IDimension
{
    static Int32 IDimension.Exponent => 1;
    public static String Representation => String.Empty;
}
internal readonly struct Square : IDimension
{
    static Int32 IDimension.Exponent => 2;
    public static String Representation => "²";
}
internal readonly struct Cube : IDimension
{
    static Int32 IDimension.Exponent => 3;
    public static String Representation => "³";
}
