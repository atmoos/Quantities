namespace Atmoos.Quantities.Core.Numerics;

public interface IPositive
{ /* marker interface */ }

public interface INegative
{ /* marker interface */ }

public interface INumber
{
    internal static abstract Int32 Value { get; }
}

public readonly record struct Negative<TNumber> : INumber, INegative
    where TNumber : INumber, IPositive
{
    static Int32 INumber.Value { get; } = -TNumber.Value;
}

public readonly record struct Zero : INumber, IPositive
{
    static Int32 INumber.Value => 0;
}

public readonly record struct One : INumber, IPositive
{
    static Int32 INumber.Value => 1;
}

public readonly record struct Two : INumber, IPositive
{
    static Int32 INumber.Value => 2;
}

public readonly record struct Three : INumber, IPositive
{
    static Int32 INumber.Value => 3;
}

public readonly record struct Four : INumber, IPositive
{
    static Int32 INumber.Value => 4;
}

public readonly record struct Five : INumber, IPositive
{
    static Int32 INumber.Value => 5;
}
