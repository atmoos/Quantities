using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct SecondPower<TCreate, TQuantity, TSquare, TLinear> : ISquareFactory<TQuantity, TSquare, TLinear>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
    where TSquare : ISquare<TLinear>, Dimensions.IDimension
    where TLinear : Dimensions.IDimension, ILinear
{
    private readonly TCreate creator;
    public Compound<TCreate, TQuantity, TLinear> Square => new(in this.creator, AllocationFree<PowerInjector<TCreate, Square>>.Item);
    internal SecondPower(in TCreate creator) => this.creator = creator;
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TSquare, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.creator.Create<Metric<TUnit>, Alias<TUnit, TLinear>>());
    }
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TSquare, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.creator.Create<Metric<TPrefix, TUnit>, Alias<TPrefix, TUnit, TLinear>>());
    }
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TSquare, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.creator.Create<Imperial<TUnit>, Alias<TUnit, TLinear>>());
    }
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TSquare, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.creator.Create<NonStandard<TUnit>, Alias<TUnit, TLinear>>());
    }
}
