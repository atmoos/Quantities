using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct ThirdPower<TCreate, TQuantity, TCubic, TLinear> : ICubicFactory<TQuantity, TCubic, TLinear>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
    where TCubic : ICubic<TLinear>, Dimensions.IDimension
    where TLinear : Dimensions.IDimension, ILinear
{
    private readonly TCreate create;
    public Compound<TCreate, TQuantity, TLinear> Cubic => new(in this.create, AllocationFree<PowerInjector<TCreate, Cubic>>.Item);
    internal ThirdPower(in TCreate create) => this.create = create;
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
