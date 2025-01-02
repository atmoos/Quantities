using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities.Core;

public interface IFactory<out TSelf>
    where TSelf : IFactory<TSelf>
{
    internal static abstract TSelf Create(in Quantity value);
}

public interface IScalar<out TQuantity, in TDimension>
    where TQuantity : IScalar<TQuantity, TDimension>, TDimension
    where TDimension : IDimension
{
    public TQuantity To<TUnit>(in Scalar<TUnit> other) where TUnit : TDimension, IUnit;
    public static abstract TQuantity Of<TUnit>(in Double value, in Scalar<TUnit> measure) where TUnit : TDimension, IUnit;
}

public interface IQuotient<out TQuantity, in TDimension, in TNominatorDimension, in TDenominatorDimension> : IScalar<TQuantity, TDimension>
    where TQuantity : IQuotient<TQuantity, TDimension, TNominatorDimension, TDenominatorDimension>, TDimension
    where TDimension : IQuotient<TNominatorDimension, TDenominatorDimension>
    where TNominatorDimension : IDimension
    where TDenominatorDimension : IDimension
{
    public TQuantity To<TNominator, TDenominator>(in Quotient<TNominator, TDenominator> other)
        where TNominator : TNominatorDimension, IUnit
        where TDenominator : TDenominatorDimension, IUnit;
    public static abstract TQuantity Of<TNominator, TDenominator>(in Double value, in Quotient<TNominator, TDenominator> measure)
        where TNominator : TNominatorDimension, IUnit
        where TDenominator : TDenominatorDimension, IUnit;
}

public interface IProduct<out TQuantity, in TDimension, in TLeftDimension, in TRightDimension> : IScalar<TQuantity, TDimension>
    where TQuantity : IProduct<TQuantity, TDimension, TLeftDimension, TRightDimension>, TDimension
    where TDimension : IProduct<TLeftDimension, TRightDimension>
    where TLeftDimension : IDimension
    where TRightDimension : IDimension
{
    public TQuantity To<TLeft, TRight>(in Product<TLeft, TRight> other)
        where TLeft : TLeftDimension, IUnit
        where TRight : TRightDimension, IUnit;
    public static abstract TQuantity Of<TLeft, TRight>(in Double value, in Product<TLeft, TRight> measure)
        where TLeft : TLeftDimension, IUnit
        where TRight : TRightDimension, IUnit;
}

public interface IInvertible<out TQuantity, in TDimension, in TInverse>
    where TQuantity : IInvertible<TQuantity, TDimension, TInverse>, TDimension
    where TDimension : IDimension, IInverse<TInverse>, ILinear
    where TInverse : IDimension, ILinear
{
    public TQuantity To<TUnit>(in Scalar<TUnit> other) where TUnit : TDimension, IInvertible<TInverse>, IUnit;
    public static abstract TQuantity Of<TUnit>(in Double value, in Scalar<TUnit> measure) where TUnit : TDimension, IInvertible<TInverse>, IUnit;
}

public interface IPowerOf<out TQuantity, in TDimension, in TLinear, TExponent>
    where TQuantity : IPowerOf<TQuantity, TDimension, TLinear, TExponent>, TDimension
    where TDimension : IDimension<TLinear, TExponent>
    where TLinear : IDimension, ILinear
    where TExponent : INumber
{
    public TQuantity To<TUnit>(in Power<TUnit, TExponent> other) where TUnit : TLinear, IUnit;
    public TQuantity To<TUnit>(in Scalar<TUnit> alias) where TUnit : TDimension, IPowerOf<TLinear>, IUnit;
    public static abstract TQuantity Of<TUnit>(in Double value, in Power<TUnit, TExponent> measure) where TUnit : TLinear, IUnit;
    public static abstract TQuantity Of<TUnit>(in Double value, in Scalar<TUnit> alias) where TUnit : TDimension, IPowerOf<TLinear>, IUnit;
}
