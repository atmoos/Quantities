using Quantities.Numerics;

namespace Quantities.Measures;

internal interface IExponent
{
    public static abstract Int32 E { get; }
    public static abstract Dimensions.Rank Rank { get; }
    public static abstract Polynomial Pow(in Polynomial value);
    public static abstract String Representation { get; }
}
internal readonly struct Square : IExponent
{
    public static Int32 E => 2;
    public static Dimensions.Rank Rank => Dimensions.Rank.Two;
    public static Polynomial Pow(in Polynomial value) => value * value;
    public static String Representation => "²";

}
internal readonly struct Cubic : IExponent
{
    public static Int32 E => 3;
    public static Dimensions.Rank Rank => Dimensions.Rank.Three;
    public static Polynomial Pow(in Polynomial value) => value * value * value;
    public static String Representation => "³";
}
