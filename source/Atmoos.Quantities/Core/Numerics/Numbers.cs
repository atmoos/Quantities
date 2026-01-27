namespace Atmoos.Quantities.Core.Numerics;

public interface IPositive; // marker interface

public interface INegative; // marker interface

internal interface IInjectNumber<out TResult>
{
    public TResult Inject<TNumber>()
        where TNumber : INumber;
}

public interface INumber
{
    internal static abstract Int32 Value { get; }
    internal static abstract TResult Negate<TResult>(IInjectNumber<TResult> injector);
}

public interface IPositive<TNumber> : IPositive, INumber
    where TNumber : INumber, IPositive
{
    static TResult INumber.Negate<TResult>(IInjectNumber<TResult> injector) => injector.Inject<Negative<TNumber>>();
}

public readonly record struct Negative<TNumber> : INumber, INegative
    where TNumber : INumber, IPositive
{
    static Int32 INumber.Value { get; } = -TNumber.Value;

    static TResult INumber.Negate<TResult>(IInjectNumber<TResult> injector) => injector.Inject<TNumber>();
}

public readonly record struct Zero : INumber, IPositive
{
    static Int32 INumber.Value => 0;

    static TResult INumber.Negate<TResult>(IInjectNumber<TResult> injector) => injector.Inject<Zero>();
}

public readonly record struct One : INumber, IPositive<One>
{
    static Int32 INumber.Value => 1;
}

public readonly record struct Two : INumber, IPositive<Two>
{
    static Int32 INumber.Value => 2;
}

public readonly record struct Three : INumber, IPositive<Three>
{
    static Int32 INumber.Value => 3;
}

public readonly record struct Four : INumber, IPositive<Four>
{
    static Int32 INumber.Value => 4;
}

public readonly record struct Five : INumber, IPositive<Five>
{
    static Int32 INumber.Value => 5;
}
