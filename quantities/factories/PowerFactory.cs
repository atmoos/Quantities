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
    private readonly INewInject inject;
    internal PowerFactory(in TCreate value, INewInject inject)
    {
        this.creator = value;
        this.inject = inject;
    }

    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TLinear => TQuantity.Create(this.inject.Inject<TCreate, Si<TUnit>>(in this.creator));
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TLinear => TQuantity.Create(this.inject.Inject<TCreate, Si<TPrefix, TUnit>>(in this.creator));
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TLinear => TQuantity.Create(this.inject.Inject<TCreate, Metric<TUnit>>(in this.creator));
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TLinear => TQuantity.Create(this.inject.Inject<TCreate, Metric<TPrefix, TUnit>>(in this.creator));
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TLinear => TQuantity.Create(this.inject.Inject<TCreate, Imperial<TUnit>>(in this.creator));
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TLinear => TQuantity.Create(this.inject.Inject<TCreate, NonStandard<TUnit>>(in this.creator));

}
