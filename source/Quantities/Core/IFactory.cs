using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities.Core;

public interface IFactory<out TResult>
{
    internal static abstract TResult Create(in Quantity value);
}

public interface IScalar<out TResult, in TDimension>
    where TResult : IFactory<TResult>, TDimension
    where TDimension : IDimension
{
    public TResult To<TDim>(in Creation.Scalar<TDim> measure) where TDim : TDimension, IUnit;
    public static abstract TResult Of<TDim>(in Double value, in Creation.Scalar<TDim> measure) where TDim : TDimension, IUnit;
}
