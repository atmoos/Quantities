using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct Composite<TCreate, TQuantity, TDimension> : IDefaultFactory<TQuantity, TDimension>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
    where TDimension : Dimensions.IDimension
{
    private readonly TCreate creator;
    private readonly IInject<TCreate> injector;
    internal Composite(in TCreate creator, IInject<TCreate> injector)
    {
        this.creator = creator;
        this.injector = injector;
    }

    public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDimension => TQuantity.Create(this.injector.Inject<Si<TUnit>>(in this.creator));
    public TQuantity Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension => TQuantity.Create(this.injector.Inject<Si<TPrefix, TUnit>>(in this.creator));
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.injector.Inject<Metric<TUnit>>(in this.creator));
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TDimension => TQuantity.Create(this.injector.Inject<Metric<TPrefix, TUnit>>(in this.creator));
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TDimension => TQuantity.Create(this.injector.Inject<Imperial<TUnit>>(in this.creator));
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TDimension => TQuantity.Create(this.injector.Inject<NonStandard<TUnit>>(in this.creator));
}
