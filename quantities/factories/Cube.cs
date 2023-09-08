using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

// Name inspired by: https://en.wikipedia.org/wiki/Degree_of_a_polynomial#Names_of_polynomials_by_degree
// However, this guy is an exception to the rule due to the fluent verb already being "cubic".
public readonly struct Cube<TCreate, TQuantity, TCubic, TLinear> : ICubicFactory<TQuantity, TCubic, TLinear>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
    where TCubic : ICubic<TLinear>, Dimensions.IDimension
    where TLinear : Dimensions.IDimension, ILinear
{
    private readonly TCreate creator;
    public Composite<TCreate, TQuantity, TLinear> Cubic => new(in this.creator, AllocationFree<PowerInjector<TCreate, Cubic>>.Item);
    internal Cube(in TCreate creator) => this.creator = creator;
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TCubic, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.creator.Create<Metric<TUnit>, Alias<TUnit, TLinear>>());
    }
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TCubic, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.creator.Create<Metric<TPrefix, TUnit>, Alias<TPrefix, TUnit, TLinear>>());
    }
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TCubic, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.creator.Create<Imperial<TUnit>, Alias<TUnit, TLinear>>());
    }
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TCubic, IInjectUnit<TLinear>
    {
        return TQuantity.Create(this.creator.Create<NonStandard<TUnit>, Alias<TUnit, TLinear>>());
    }
}
