using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public readonly struct SquareFactory<TQuantity, TCreate, TSquare, TLinear> : ISquareFactory<TQuantity, TSquare, TLinear>
    where TLinear : Dimensions.IDimension, ILinear
    where TSquare : ISquare<TLinear>, Dimensions.IDimension
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
{
    private readonly TCreate create;
    public PowerFactory<TQuantity, TCreate, TLinear> Square => new(in this.create, ZeroAllocation<Injector<TCreate, Square>>.Item);
    internal SquareFactory(in TCreate compound) => this.create = compound;

    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TSquare, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.create.Create<Metric<TUnit>, Alias<TUnit, TLinear>>());
    }
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TSquare, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.create.Create<Metric<TPrefix, TUnit>, Alias<TPrefix, TUnit, TLinear>>());
    }
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TSquare, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.create.Create<Imperial<TUnit>, Alias<TUnit, TLinear>>());
    }
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TSquare, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.create.Create<NonStandard<TUnit>, Alias<TUnit, TLinear>>());
    }
}
