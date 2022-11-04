namespace Quantities.Measures;

public interface IDimension
{
    internal static abstract Int32 Exponent { get; }
    public static abstract String Representation { get; }
}
public sealed class Linear : IDimension
{
    static Int32 IDimension.Exponent => 1;
    public static String Representation => String.Empty;
}
public sealed class Square : IDimension
{
    static Int32 IDimension.Exponent => 2;
    public static String Representation => "²";
}
public sealed class Cube : IDimension
{
    static Int32 IDimension.Exponent => 3;
    public static String Representation => "³";
}
