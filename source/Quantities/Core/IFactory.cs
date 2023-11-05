using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities.Core;

public interface IFactory<out TResult>
{
    internal static abstract TResult Create(in Quantity value);
}

public interface IScalar<out TQuantity, in TDimension>
    where TQuantity : struct, IScalar<TQuantity, TDimension>, TDimension
    where TDimension : IDimension
{
    public TQuantity To<TDim>(in Creation.Scalar<TDim> measure) where TDim : TDimension, IUnit;
    public static abstract TQuantity Of<TDim>(in Double value, in Creation.Scalar<TDim> measure) where TDim : TDimension, IUnit;
}

public interface ISquare<out TQuantity, in TDimension>
    where TQuantity : struct, ISquare<TQuantity, TDimension>, ISquare<TDimension>
    where TDimension : IDimension, ILinear
{
    public TQuantity To<TDim>(in Creation.Square<TDim> measure) where TDim : TDimension, IUnit;
    public static abstract TQuantity Of<TDim>(in Double value, in Creation.Square<TDim> measure) where TDim : TDimension, IUnit;
}

public interface ICubic<out TQuantity, in TDimension>
    where TQuantity : struct, ICubic<TQuantity, TDimension>, ICubic<TDimension>
    where TDimension : IDimension, ILinear
{
    public TQuantity To<TDim>(in Creation.Cubic<TDim> measure) where TDim : TDimension, IUnit;
    public static abstract TQuantity Of<TDim>(in Double value, in Creation.Cubic<TDim> measure) where TDim : TDimension, IUnit;
}

public interface IAlias<out TQuantity, in TDimension, TAliasedDimension>
    where TQuantity : struct, IAlias<TQuantity, TDimension, TAliasedDimension>, TDimension
    where TDimension : IDimension
    where TAliasedDimension : IDimension, ILinear
{
    public TQuantity To<TAlias>(in Creation.Alias<TAlias, TAliasedDimension> alias) where TAlias : TDimension, IAlias<TAliasedDimension>, IUnit;
    public static abstract TQuantity Of<TAlias>(in Double value, in Creation.Alias<TAlias, TAliasedDimension> alias) where TAlias : TDimension, IAlias<TAliasedDimension>, IUnit;
}
