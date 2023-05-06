using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct SquareFactory<TQuantity, TCompound, TSquare, TLinear> : ISquareFactory<TQuantity, TCompound, TSquare, TLinear>
    where TLinear : Dimensions.IDimension, ILinear
    where TSquare : Dimensions.IDimension
    where TCompound : ICompoundFactory<TQuantity, TLinear>, IInjectCreate
    where TQuantity : IFactory<TQuantity>
{
    private readonly TCompound squareFactory;
    public TCompound Square => this.squareFactory;
    internal SquareFactory(in TCompound compound) => this.squareFactory = compound;
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TSquare, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.squareFactory.Create<Metric<TUnit>, Alias<TUnit, TLinear>>());
    }
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TSquare, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.squareFactory.Create<Metric<TPrefix, TUnit>, Alias<TPrefix, TUnit, TLinear>>());
    }
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TSquare, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.squareFactory.Create<Imperial<TUnit>, Alias<TUnit, TLinear>>());
    }
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TSquare, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.squareFactory.Create<NonStandard<TUnit>, Alias<TUnit, TLinear>>());
    }
}
