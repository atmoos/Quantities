using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct LinearTo<TQuantity, TDimension> : ICompoundFactory<TQuantity, TDimension>
    where TQuantity : IFactory<TQuantity>
    where TDimension : Dimensions.IDimension, ILinear
{
    private readonly Quant value;
    internal LinearTo(in Quant value) => this.value = value;
    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension => TQuantity.Create(this.value.As<Si<TUnit>>());
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension => TQuantity.Create(this.value.As<Si<TPrefix, TUnit>>());
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.value.As<Metric<TUnit>>());
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.value.As<Metric<TPrefix, TUnit>>());
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TDimension => TQuantity.Create(this.value.As<Imperial<TUnit>>());
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TDimension => TQuantity.Create(this.value.As<NonStandard<TUnit>>());
}

public readonly struct LinearCreate<TQuantity, TDimension> : ICompoundFactory<TQuantity, TDimension>
    where TQuantity : IFactory<TQuantity>
    where TDimension : Dimensions.IDimension, ILinear
{
    private readonly Double value;
    internal LinearCreate(in Double value) => this.value = value;
    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension => TQuantity.Create(this.value.To<Si<TUnit>>());
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension => TQuantity.Create(this.value.To<Si<TPrefix, TUnit>>());
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.value.To<Metric<TUnit>>());
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.value.To<Metric<TPrefix, TUnit>>());
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TDimension => TQuantity.Create(this.value.To<Imperial<TUnit>>());
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TDimension => TQuantity.Create(this.value.To<NonStandard<TUnit>>());
}

public readonly struct LinearFactory<TQuantity, TDimension> : ICompoundFactory<TQuantity, TDimension>, ICreatable<LinearFactory<TQuantity, TDimension>>
    where TQuantity : IFactory<TQuantity>
    where TDimension : Dimensions.IDimension, ILinear
{
    private readonly ICreate creator;
    private LinearFactory(in ICreate value) => this.creator = value;
    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension => TQuantity.Create(this.creator.Create<Si<TUnit>>());
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension => TQuantity.Create(this.creator.Create<Si<TPrefix, TUnit>>());
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.creator.Create<Metric<TUnit>>());
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.creator.Create<Metric<TPrefix, TUnit>>());
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TDimension => TQuantity.Create(this.creator.Create<Imperial<TUnit>>());
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TDimension => TQuantity.Create(this.creator.Create<NonStandard<TUnit>>());
    static LinearFactory<TQuantity, TDimension> ICreatable<LinearFactory<TQuantity, TDimension>>.Create(in ICreate creator) => new(in creator);
}