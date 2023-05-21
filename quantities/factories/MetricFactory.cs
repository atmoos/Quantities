using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct MetricTo<TQuantity, TDimension> : IMetricFactory<TQuantity, TDimension>
    where TDimension : Dimensions.IDimension, ILinear
    where TQuantity : IFactory<TQuantity>
{
    private readonly Quant value;
    internal MetricTo(in Quant value) => this.value = value;
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.value.As<Metric<TUnit>>());
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.value.As<Metric<TPrefix, TUnit>>());
}

public readonly struct MetricCreate<TQuantity, TScale, TDimension> : IMetricFactory<TQuantity, TDimension>
    where TDimension : Dimensions.IDimension, ILinear
    where TQuantity : IFactory<TQuantity>
{
    private readonly Double value;
    internal MetricCreate(in Double value) => this.value = value;
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.value.As<Metric<TUnit>>());
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.value.As<Metric<TPrefix, TUnit>>());
}
