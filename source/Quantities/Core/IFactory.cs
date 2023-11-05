using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities.Core;

public interface IFactory<out TSelf>
    where TSelf : IFactory<TSelf>
{
    internal static abstract TSelf Create(in Quantity value);
}

public interface IScalar<out TQuantity, in TDimension>
    where TQuantity : struct, IScalar<TQuantity, TDimension>, TDimension
    where TDimension : IDimension
{
    public TQuantity To<TUnit>(in Scalar<TUnit> other) where TUnit : TDimension, IUnit;
    public static abstract TQuantity Of<TUnit>(in Double value, in Scalar<TUnit> measure) where TUnit : TDimension, IUnit;
}

public interface IQuotient<out TQuantity, in TDimension, in TNominatorDimension, in TDenominatorDimension> : IScalar<TQuantity, TDimension>
    where TQuantity : struct, IQuotient<TQuantity, TDimension, TNominatorDimension, TDenominatorDimension>, TDimension
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
    where TQuantity : struct, IProduct<TQuantity, TDimension, TLeftDimension, TRightDimension>, TDimension
    where TDimension : IProduct<TLeftDimension, TRightDimension>
    where TLeftDimension : IDimension
    where TRightDimension : IDimension
{
    public TQuantity To<TNominator, TDenominator>(in Product<TNominator, TDenominator> other)
        where TNominator : TLeftDimension, IUnit
        where TDenominator : TRightDimension, IUnit;
    public static abstract TQuantity Of<TNominator, TDenominator>(in Double value, in Product<TNominator, TDenominator> measure)
        where TNominator : TLeftDimension, IUnit
        where TDenominator : TRightDimension, IUnit;
}

public interface ISquare<out TQuantity, in TDimension, TLinear> : IAlias<TQuantity, TDimension, TLinear>
    where TQuantity : struct, ISquare<TQuantity, TDimension, TLinear>, TDimension
    where TDimension : ISquare<TLinear>
    where TLinear : IDimension, ILinear
{
    public TQuantity To<TUnit>(in Square<TUnit> other) where TUnit : TLinear, IUnit;
    public static abstract TQuantity Of<TUnit>(in Double value, in Square<TUnit> measure) where TUnit : TLinear, IUnit;
}

public interface ICubic<out TQuantity, in TDimension, TLinear> : IAlias<TQuantity, TDimension, TLinear>
    where TQuantity : struct, ICubic<TQuantity, TDimension, TLinear>, TDimension
    where TDimension : ICubic<TLinear>
    where TLinear : IDimension, ILinear
{
    public TQuantity To<TUnit>(in Cubic<TUnit> other) where TUnit : TLinear, IUnit;
    public static abstract TQuantity Of<TUnit>(in Double value, in Cubic<TUnit> measure) where TUnit : TLinear, IUnit;
}

public interface IAlias<out TQuantity, in TDimension, TAliasedDimension>
    where TQuantity : struct, IAlias<TQuantity, TDimension, TAliasedDimension>, TDimension
    where TDimension : IDimension
    where TAliasedDimension : IDimension, ILinear
{
    public TQuantity To<TAlias>(in Alias<TAlias, TAliasedDimension> alias) where TAlias : TDimension, IAlias<TAliasedDimension>, IUnit;
    public static abstract TQuantity Of<TAlias>(in Double value, in Alias<TAlias, TAliasedDimension> alias) where TAlias : TDimension, IAlias<TAliasedDimension>, IUnit;
}
