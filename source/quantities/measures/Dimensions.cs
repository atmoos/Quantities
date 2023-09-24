namespace Quantities.Measures;

internal interface IExponent
{
    public static abstract Int32 E { get; }
    public static abstract String Representation { get; }
}
internal readonly struct Square : IExponent
{
    public static Int32 E => 2;
    public static String Representation => "²";

}
internal readonly struct Cubic : IExponent
{
    public static Int32 E => 3;
    public static String Representation => "³";
}
