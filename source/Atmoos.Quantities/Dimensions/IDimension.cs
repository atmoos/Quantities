using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Dimensions;

public interface IDimension
{
    internal static abstract Dimension D { get; }
}

public interface IMultiplicity<out TBase, out TNumber> : IDimension
    where TBase : IMultiplicity<TBase, One>
    where TNumber : INumber; // marker interface

// ToDo: Letting this dimension also implement IMultiplicity is possibly not optimal.
public interface IDimension<TBase, TMultiplicity> : IMultiplicity<TBase, TMultiplicity>, IDimension
    where TBase : IMultiplicity<TBase, One>, IDimension
    where TMultiplicity : INumber
{
    static Dimension IDimension.D { get; } = Scalar.Of<TBase>(TMultiplicity.Value);
}

// ToDo: Remove ILinear
public interface ILinear<TSelf> : ILinear, IDimension<TSelf, One>
    where TSelf : ILinear<TSelf>;
