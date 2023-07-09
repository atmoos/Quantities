using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct Compound<TCreate, TQuantity, TDimension> : ICompoundFactory<TQuantity, TDimension>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
    where TDimension : Dimensions.IDimension, ILinear
{
    private readonly TCreate creator;
    private readonly IInject<TCreate> inject;
    internal Compound(in TCreate creator, IInject<TCreate> inject)
    {
        this.creator = creator;
        this.inject = inject;
    }

    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension => TQuantity.Create(this.inject.Inject<Si<TUnit>>(in this.creator));
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension => TQuantity.Create(this.inject.Inject<Si<TPrefix, TUnit>>(in this.creator));
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.inject.Inject<Metric<TUnit>>(in this.creator));
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.inject.Inject<Metric<TPrefix, TUnit>>(in this.creator));
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TDimension => TQuantity.Create(this.inject.Inject<Imperial<TUnit>>(in this.creator));
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TDimension => TQuantity.Create(this.inject.Inject<NonStandard<TUnit>>(in this.creator));
}