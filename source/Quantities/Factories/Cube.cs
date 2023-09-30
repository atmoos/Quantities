using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;

namespace Quantities.Factories;

// Name inspired by: https://en.wikipedia.org/wiki/Degree_of_a_polynomial#Names_of_polynomials_by_degree
// However, this guy is an exception to the rule due to the fluent verb already being "cubic".
public readonly struct Cube<TCreate, TQuantity, TCubic, TLinear> : ICubicFactory<TQuantity, TCubic, TLinear>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
    where TCubic : ICubic<TLinear>, IDimension
    where TLinear : IDimension
{
    private readonly TCreate creator;
    public Composite<TCreate, TQuantity, TLinear> Cubic => new(in this.creator, AllocationFree<PowerInjector<TCreate, Cubic>>.Item);
    internal Cube(in TCreate creator) => this.creator = creator;
    public TQuantity Metric<TUnit>()
        where TUnit : IMetricUnit, TCubic, IAlias<TLinear>
            => TQuantity.Create(Injector<TUnit>().Inject<Metric<TUnit>>(in this.creator));
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TCubic, IAlias<TLinear>
            => TQuantity.Create(Injector<TUnit>().Inject<Metric<TPrefix, TUnit>>(in this.creator));
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TCubic, IAlias<TLinear>
        => TQuantity.Create(Injector<TUnit>().Inject<Imperial<TUnit>>(in this.creator));
    public TQuantity NonStandard<TUnit>() where TUnit : INonStandardUnit, TCubic, IAlias<TLinear>
        => TQuantity.Create(Injector<TUnit>().Inject<NonStandard<TUnit>>(in this.creator));

    private static IInject<TCreate> Injector<TUnit>()
        where TUnit : IAlias<TLinear> => TUnit.Inject(AllocationFree<AliasInjectionFactory<TCreate, TLinear>>.Item);
}
