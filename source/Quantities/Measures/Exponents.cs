namespace Quantities.Measures;

internal interface IInjectExponent<out TResult>
{
    public TResult Inject<TExponent>() where TExponent : IExponent;
}

internal interface IExponent
{
    public static abstract Int32 E { get; }
    public static abstract TResult Invert<TResult>(IInjectExponent<TResult> injector);
}

internal readonly struct Square : IExponent
{
    public static Int32 E => 2;
    public static TResult Invert<TResult>(IInjectExponent<TResult> injector) => injector.Inject<InverseExp<Square>>();
}

internal readonly struct Cubic : IExponent
{
    public static Int32 E => 3;
    public static TResult Invert<TResult>(IInjectExponent<TResult> injector) => injector.Inject<InverseExp<Cubic>>();
}

internal readonly struct InverseExp<TExp> : IExponent
 where TExp : IExponent
{
    public static Int32 E { get; } = -TExp.E;
    public static TResult Invert<TResult>(IInjectExponent<TResult> injector) => injector.Inject<TExp>();
}
