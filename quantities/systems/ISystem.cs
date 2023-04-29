

using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Systems;

public interface ISystem
{

}

public interface IQuantityBuilder<out TSystem>
    where TSystem : ISystem
{
    public static abstract TSystem Of(in Double value);
}

public readonly struct Linear<TSelf, TDimension> : ISystem
    where TDimension : Dimensions.IDimension, ILinear
    where TSelf : struct, IQuantity<TSelf>, IQuantityFactory<TSelf, TDimension>, TDimension
{
    private readonly Double value;
    internal Linear(in Double value) => this.value = value;
    public TSelf Si<TUnit>() where TUnit : ISiBaseUnit, TDimension => TSelf.Create(this.value.As<Si<TUnit>>());
    public TSelf Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiBaseUnit, TDimension => TSelf.Create(this.value.As<Si<TPrefix, TUnit>>());
    public TSelf Metric<TUnit>() where TUnit : IMetricUnit, TDimension => TSelf.Create(this.value.As<Metric<TUnit>>());
    public TSelf Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TDimension => TSelf.Create(this.value.As<Metric<TUnit>>());

    public TSelf Imperial<TUnit>() where TUnit : IImperial, TDimension => TSelf.Create(this.value.As<Imperial<TUnit>>());
    public TSelf NonStandard<TUnit>() where TUnit : INoSystem, TDimension => TSelf.Create(this.value.As<NonStandard<TUnit>>());

}

