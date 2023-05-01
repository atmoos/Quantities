using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct LinearCreateFactory<TQuantity, TDimension> : ICompoundFactory<TQuantity, TDimension>
    where TDimension : Dimensions.IDimension, ILinear
    where TQuantity : struct, IQuantity<TQuantity>, IQuantityFactory<TQuantity, TDimension>, TDimension
{
    private readonly Double value;
    internal LinearCreateFactory(in Double builder) => this.value = builder;
    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension => TQuantity.Create(this.value.As<Si<TUnit>>());
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension => TQuantity.Create(this.value.As<Si<TPrefix, TUnit>>());
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.value.As<Metric<TUnit>>());
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.value.As<Metric<TUnit>>());

    public TQuantity Imperial<TUnit>() where TUnit : IImperial, TDimension => TQuantity.Create(this.value.As<Imperial<TUnit>>());
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystem, TDimension => TQuantity.Create(this.value.As<NonStandard<TUnit>>());
}

