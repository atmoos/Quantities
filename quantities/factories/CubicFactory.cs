using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct CubicFactory<TQuantity, TCreate, TCubic, TLinear> : ICubicFactory<TQuantity, TCubic, TLinear>
    where TLinear : Dimensions.IDimension, ILinear
    where TCubic : ICubic<TLinear>, Dimensions.IDimension
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
{
    private readonly TCreate create;
    public PowerFactory<TQuantity, TCreate, TLinear> Cubic => new(in this.create, ZeroAllocation<Injector<TCreate, Cubic>>.Item);
    internal CubicFactory(in TCreate create) => this.create = create;
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TCubic, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.create.Create<Metric<TUnit>, Alias<TUnit, TLinear>>());
    }
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TCubic, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.create.Create<Metric<TPrefix, TUnit>, Alias<TPrefix, TUnit, TLinear>>());
    }
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TCubic, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.create.Create<Imperial<TUnit>, Alias<TUnit, TLinear>>());
    }
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TCubic, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.create.Create<NonStandard<TUnit>, Alias<TUnit, TLinear>>());
    }
}
