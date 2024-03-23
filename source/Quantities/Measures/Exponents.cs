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

internal readonly struct InverseExp<TExp> : IExponent
 where TExp : IExponent
{
    public static Int32 E { get; } = -TExp.E;
}
