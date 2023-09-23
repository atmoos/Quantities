using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

// Name inspired by: https://en.wikipedia.org/wiki/Degree_of_a_polynomial#Names_of_polynomials_by_degree
public readonly struct Quadratic<TCreate, TQuantity, TSquare, TLinear> : IQuadraticFactory<TQuantity, TSquare, TLinear>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
    where TSquare : ISquare<TLinear>, IDimension
    where TLinear : IDimension
{
    private readonly TCreate creator;
    public Composite<TCreate, TQuantity, TLinear> Square => new(in this.creator, AllocationFree<PowerInjector<TCreate, Square>>.Item);
    internal Quadratic(in TCreate creator) => this.creator = creator;
    public TQuantity Metric<TUnit>()
        where TUnit : IMetricUnit, TSquare, IAlias<TLinear>
            => TQuantity.Create(Injector<TUnit>().Inject<Metric<TUnit>>(in this.creator));
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TSquare, IAlias<TLinear>
            => TQuantity.Create(Injector<TUnit>().Inject<Metric<TPrefix, TUnit>>(in this.creator));
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TSquare, IAlias<TLinear>
        => TQuantity.Create(Injector<TUnit>().Inject<Imperial<TUnit>>(in this.creator));
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TSquare, IAlias<TLinear>
        => TQuantity.Create(Injector<TUnit>().Inject<NonStandard<TUnit>>(in this.creator));

    private static IInject<TCreate> Injector<TUnit>()
        where TUnit : IAlias<TLinear> => TUnit.Inject(AllocationFree<AliasInjectionFactory<TCreate, TLinear>>.Item);
}
