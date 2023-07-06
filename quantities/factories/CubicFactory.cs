using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct CubicFactory<TQuantity, TCompound, TCubic, TLinear> : ICubicFactory<TQuantity, TCubic, TLinear>
    where TLinear : Dimensions.IDimension, ILinear
    where TCubic : ICubic<TLinear>, Dimensions.IDimension
    where TCompound : ICompoundFactory<TQuantity, TLinear>, IAliasingCreate
    where TQuantity : IFactory<TQuantity>
{
    private readonly TCompound cubicFactory;
    public TCompound Cubic => this.cubicFactory;
    internal CubicFactory(in TCompound compound) => this.cubicFactory = compound;
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TCubic, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.cubicFactory.Create<Metric<TUnit>, Alias<TUnit, TLinear>>());
    }
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TCubic, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.cubicFactory.Create<Metric<TPrefix, TUnit>, Alias<TPrefix, TUnit, TLinear>>());
    }
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TCubic, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.cubicFactory.Create<Imperial<TUnit>, Alias<TUnit, TLinear>>());
    }
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TCubic, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.cubicFactory.Create<NonStandard<TUnit>, Alias<TUnit, TLinear>>());
    }
}
