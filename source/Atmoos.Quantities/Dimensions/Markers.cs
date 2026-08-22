using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Dimensions;

public interface IBaseQuantity; // marker interface

public interface IBaseQuantity<TSelf> : IMultiplicity<TSelf, One>, IBaseQuantity, IDimension
    where TSelf : IBaseQuantity<TSelf>
{
    static Dimension IDimension.D { get; } = Scalar.Of<TSelf>();
    static ref readonly Kind IDimension.Kind => ref Kind.Of<TSelf>();
}

public interface IDerivedQuantity; // marker interface
public interface IDerivedQuantity<TSelf> : IMultiplicity<TSelf, One>, IDerivedQuantity, IDimension
    where TSelf : IDerivedQuantity<TSelf>
{
    static ref readonly Kind IDimension.Kind => ref Kind.Of<TSelf>();
}

// ToDo: Remove ILinear
public interface ILinear; // marker interface

public interface IProduct<out TLeft, out TRight> : IDimension
    where TLeft : IDimension
    where TRight : IDimension
{
    static Dimension IDimension.D { get; } = TLeft.D * TRight.D;
}

// ToDo: Will this hold?
public sealed class Factor<TDimension, TMultiplicity> : IDimension<TDimension, TMultiplicity>
    where TDimension : IMultiplicity<TDimension, One>, IDimension
    where TMultiplicity : INumber
{
    private Factor() { }
    static ref readonly Kind IDimension.Kind => ref Kind.Of<TDimension>();
}

public sealed class Times<TLeft, TRight> : IProduct<TLeft, TRight>
    where TLeft : IDimension
    where TRight : IDimension
{
    private Times() { }
    static ref readonly Kind IDimension.Kind => ref Kind.Of<Times<TLeft, TRight>>();
}
