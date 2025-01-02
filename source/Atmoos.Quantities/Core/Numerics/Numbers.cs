
namespace Atmoos.Quantities.Core.Numerics;

public interface INumber
{
    internal static abstract Int32 Value { get; }
}

public readonly record struct Negative<TNumber> : INumber
    where TNumber : INumber
{
    static Int32 INumber.Value { get; } = -TNumber.Value;
}

public readonly record struct Zero : INumber
{
    static Int32 INumber.Value => 0;
}

public readonly record struct One : INumber
{
    static Int32 INumber.Value => 1;
}

public readonly record struct Two : INumber
{
    static Int32 INumber.Value => 2;
}

public readonly record struct Three : INumber
{
    static Int32 INumber.Value => 3;
}
