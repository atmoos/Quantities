using Quantities.Numerics;

namespace Quantities.Measures;

internal interface IExponent
{
    public static abstract Dimensions.Rank Rank { get; }
    public static abstract Polynomial Pow(in Polynomial value);
    public static abstract String Representation { get; }
}
internal readonly struct Square : IExponent
{
    public static Dimensions.Rank Rank => Dimensions.Rank.Square;
    public static Polynomial Pow(in Polynomial value) => value * value;
    public static String Representation => "²";
}
internal readonly struct Cubic : IExponent
{
    public static Dimensions.Rank Rank => Dimensions.Rank.Cubic;
    public static Polynomial Pow(in Polynomial value) => value * value * value;
    public static String Representation => "³";
}
