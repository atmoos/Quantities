using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public interface ISiFactory<out TQuantity, in TDimension> : IFactory
    where TDimension : IDimension
{
    TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension;
    TQuantity Si<TPrefix, TUnit>() where TPrefix : IMetricPrefix where TUnit : ISiUnit, TDimension;
}

public interface IMetricFactory<out TQuantity, in TDimension> : IFactory
    where TDimension : IDimension
{
    TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TDimension;
    TQuantity Metric<TPrefix, TUnit>() where TPrefix : IMetricPrefix where TUnit : IMetricUnit, TDimension;
}

public interface IBinaryFactory<out TQuantity, in TDimension> : IFactory
    where TDimension : IDimension
{
    TQuantity Binary<TPrefix, TUnit>() where TPrefix : IBinaryPrefix where TUnit : IMetricUnit, TDimension;
}

public interface IImperialFactory<out TQuantity, in TDimension> : IFactory
    where TDimension : IDimension
{
    TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TDimension;
}

public interface INonStandardFactory<out TQuantity, in TDimension> : IFactory
    where TDimension : IDimension
{
    TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TDimension;
}

public interface IDefaultFactory<out TQuantity, in TDimension>
    // Si, Imperial and "non standard" being the defaults
    : ISiFactory<TQuantity, TDimension>, IMetricFactory<TQuantity, TDimension>, IImperialFactory<TQuantity, TDimension>, INonStandardFactory<TQuantity, TDimension>
    where TDimension : IDimension
{

}
