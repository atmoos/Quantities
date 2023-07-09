using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public interface ISiFactory<out TQuantity, in TDimension> : IFactory
    where TDimension : IDimension
{
    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension;
    public TQuantity Si<TPrefix, TUnit>() where TPrefix : IMetricPrefix where TUnit : ISiUnit, TDimension;
}

public interface IMetricFactory<out TQuantity, in TDimension> : IFactory
    where TDimension : IDimension
{
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TDimension;
    public TQuantity Metric<TPrefix, TUnit>() where TPrefix : IMetricPrefix where TUnit : IMetricUnit, TDimension;
}

public interface IBinaryFactory<out TQuantity, in TDimension> : IFactory
    where TDimension : IDimension
{
    public TQuantity Binary<TPrefix, TUnit>() where TPrefix : IBinaryPrefix where TUnit : IMetricUnit, TDimension;
}

public interface IImperialFactory<out TQuantity, in TDimension> : IFactory
    where TDimension : IDimension
{
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TDimension;
}

public interface INonStandardFactory<out TQuantity, in TDimension> : IFactory
    where TDimension : IDimension
{
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TDimension;
}

public interface ICompoundFactory<out TResult, in TDimension>
    : ISiFactory<TResult, TDimension>, IMetricFactory<TResult, TDimension>, IImperialFactory<TResult, TDimension>, INonStandardFactory<TResult, TDimension>
    where TDimension : IDimension
{

}
