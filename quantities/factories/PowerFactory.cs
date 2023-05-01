using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct PowerFactory<TQuantity, TCreate, TLinear> : ICompoundFactory<TQuantity, TLinear>, IPowerCreate<TQuantity>
    where TCreate : IPowerCreate<TQuantity>
    where TLinear : Dimensions.IDimension, ILinear
{
    private readonly TCreate creator;
    internal PowerFactory(in TCreate value) => this.creator = value;
    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TLinear => this.creator.Create<Si<TUnit>>();
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TLinear => this.creator.Create<Si<TPrefix, TUnit>>();
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TLinear => this.creator.Create<Metric<TUnit>>();
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TLinear => this.creator.Create<Metric<TPrefix, TUnit>>();
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TLinear => this.creator.Create<Imperial<TUnit>>();
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TLinear => this.creator.Create<NonStandard<TUnit>>();
    TQuantity IPowerCreate<TQuantity>.Create<TMeasure>() => this.creator.Create<TMeasure>();
    TQuantity IPowerCreate<TQuantity>.Create<TMeasure, TAlias>() => this.creator.Create<TMeasure, TAlias>();

}
