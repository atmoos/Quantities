namespace Quantities.Measures;

internal interface IExponent
{
    public static abstract Int32 E { get; }
}

internal readonly struct Square : IExponent
{
    public static Int32 E => 2;
}

internal readonly struct Cubic : IExponent
{
    public static Int32 E => 3;
}
