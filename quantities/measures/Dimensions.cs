namespace Quantities.Measures;

internal interface IDimension
{
    internal static abstract Transformation Pow(Transformation value);
    internal static abstract Double Power(in Double value);
    internal static abstract Double Lower(in Double value);
    public static abstract String Representation { get; }
}
internal readonly struct Linear : IDimension
{
    public static Transformation Pow(Transformation value) => value;
    public static Double Power(in Double value) => value;
    public static Double Lower(in Double value) => value;
    public static String Representation => String.Empty;
}
internal readonly struct Square : IDimension
{
    public static Transformation Pow(Transformation value) => value.Pow(2);
    public static Double Power(in Double value) => value * value;
    public static Double Lower(in Double value) => Math.Sqrt(value);
    public static String Representation => "²";
}
internal readonly struct Cubic : IDimension
{
    public static Transformation Pow(Transformation value) => value.Pow(3);
    public static Double Power(in Double value) => value * value * value;
    public static Double Lower(in Double value) => Math.Cbrt(value);
    public static String Representation => "³";
}
