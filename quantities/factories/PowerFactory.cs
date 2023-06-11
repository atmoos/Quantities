using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct PowerFactory<TQuantity, TCreate, TLinear> : ICompoundFactory<TQuantity, TLinear>, ICreate, IAliasingCreate
    where TCreate : ICreate, IAliasingCreate
    where TLinear : Dimensions.IDimension, ILinear
    where TQuantity : IFactory<TQuantity>
{
    private readonly TCreate creator;
    internal PowerFactory(in TCreate value) => this.creator = value;
    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TLinear => TQuantity.Create(this.creator.Create<Si<TUnit>>());
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TLinear => TQuantity.Create(this.creator.Create<Si<TPrefix, TUnit>>());
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TLinear => TQuantity.Create(this.creator.Create<Metric<TUnit>>());
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TLinear => TQuantity.Create(this.creator.Create<Metric<TPrefix, TUnit>>());
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TLinear => TQuantity.Create(this.creator.Create<Imperial<TUnit>>());
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TLinear => TQuantity.Create(this.creator.Create<NonStandard<TUnit>>());
    Quant ICreate.Create<TMeasure>() => this.creator.Create<TMeasure>();
    Quant IAliasingCreate.Create<TMeasure, TAlias>() => this.creator.Create<TMeasure, TAlias>();

}
