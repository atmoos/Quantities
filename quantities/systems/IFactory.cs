using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Systems;

public interface IFactory { /* marker interface */ }

public interface IFactory<out TFactory> : IFactory
    where TFactory : IFactory
{
    public static abstract TFactory Of(in Double value);
}
public interface ISiFactory<TSelf, TDimension> : IFactory
    where TDimension : IDimension, ILinear
{
    public TSelf Si<TUnit>() where TUnit : ISiUnit, TDimension;
    public TSelf Si<TPrefix, TUnit>() where TPrefix : IMetricPrefix where TUnit : ISiUnit, TDimension;
}

public interface IMetricFactory<TSelf, TDimension> : IFactory
    where TDimension : IDimension, ILinear
{
    public TSelf Metric<TUnit>() where TUnit : IMetricUnit, TDimension;
    public TSelf Metric<TPrefix, TUnit>() where TPrefix : IMetricPrefix where TUnit : IMetricUnit, TDimension;
}

public interface IImperialFactory<TSelf, TDimension>
{
    public TSelf Imperial<TUnit>() where TUnit : IImperial, TDimension;
}

public interface INonStandardFactory<TSelf, TDimension>
{
    public TSelf NonStandard<TUnit>() where TUnit : INoSystem, TDimension;
}