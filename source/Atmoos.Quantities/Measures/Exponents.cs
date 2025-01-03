using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Measures;

internal interface IInjectExponent<out TResult>
{
    public TResult Inject<TExponent>() where TExponent : IExponent;
}

// ToDo: Remove interface or clean up ambiguity of Numerator and Denominator
//       Or add an invert function to INumber...
internal interface IExponent
{
    public static abstract Int32 E { get; }
    public static abstract TResult Invert<TResult>(IInjectExponent<TResult> injector);
}

internal readonly struct Numerator<TNumber> : IExponent, IPositive
    where TNumber : INumber
{
    public static Int32 E => TNumber.Value;
    public static TResult Invert<TResult>(IInjectExponent<TResult> injector) => injector.Inject<Denominator<TNumber>>();
}

internal readonly struct Denominator<TNumber> : IExponent, INegative
    where TNumber : INumber
{
    public static Int32 E { get; } = -TNumber.Value;
    public static TResult Invert<TResult>(IInjectExponent<TResult> injector) => injector.Inject<Numerator<TNumber>>();
}
