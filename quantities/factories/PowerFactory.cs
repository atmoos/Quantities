using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct PowerFactory<TQuantity, TCreate, TLinear> : ICompoundFactory<TQuantity, TLinear>
    where TCreate : struct, ICreate
    where TLinear : Dimensions.IDimension, ILinear
    where TQuantity : IFactory<TQuantity>
{
    private readonly TCreate creator;
    private readonly IInject<TCreate> inject;
    internal PowerFactory(in TCreate value, IInject<TCreate> inject)
    {
        this.creator = value;
        this.inject = inject;
    }

    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TLinear => TQuantity.Create(this.inject.Inject<Si<TUnit>>(in this.creator));
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TLinear => TQuantity.Create(this.inject.Inject<Si<TPrefix, TUnit>>(in this.creator));
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TLinear => TQuantity.Create(this.inject.Inject<Metric<TUnit>>(in this.creator));
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TLinear => TQuantity.Create(this.inject.Inject<Metric<TPrefix, TUnit>>(in this.creator));
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TLinear => TQuantity.Create(this.inject.Inject<Imperial<TUnit>>(in this.creator));
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TLinear => TQuantity.Create(this.inject.Inject<NonStandard<TUnit>>(in this.creator));

}
