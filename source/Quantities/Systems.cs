using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;

namespace Quantities;

public static class Systems
{
    public static Scalar<TUnit> Si<TUnit>()
        where TUnit : ISiUnit, IDimension => new(Factory.Of<Si<TUnit>>());
    public static Scalar<TUnit> Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IDimension => new(Factory.Of<Si<TPrefix, TUnit>>());
    public static Scalar<TUnit> Metric<TUnit>()
        where TUnit : IMetricUnit, IDimension => new(Factory.Of<Metric<TUnit>>());
    public static Scalar<TUnit> Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, IDimension => new(Factory.Of<Metric<TPrefix, TUnit>>());
    public static Scalar<TUnit> Binary<TPrefix, TUnit>()
        where TPrefix : IBinaryPrefix
        where TUnit : IMetricUnit, IDimension => new(Factory.Of<Metric<TPrefix, TUnit>>());
    public static Scalar<TUnit> Imperial<TUnit>()
        where TUnit : IImperialUnit, IDimension => new(Factory.Of<Imperial<TUnit>>());
    public static Scalar<TUnit> NonStandard<TUnit>()
        where TUnit : INonStandardUnit, IDimension => new(Factory.Of<NonStandard<TUnit>>());
    public static Cubic<TUnit> Cubic<TUnit>(in Scalar<TUnit> scalar) where TUnit : IUnit, IDimension => new(scalar.Factory);
    public static Square<TUnit> Square<TUnit>(in Scalar<TUnit> scalar) where TUnit : IUnit, IDimension => new(scalar.Factory);
}

public static class AliasOf<TLinear>
    where TLinear : IDimension
{
    public static Alias<TUnit> Si<TUnit>()
        where TUnit : ISiUnit, IAlias<TLinear>, IDimension
            => new(TUnit.Inject(AllocationFree<AliasInjectionFactory<TLinear, Si<TUnit>>>.Item));
    public static Alias<TUnit> Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IAlias<TLinear>, IDimension
            => new(TUnit.Inject(AllocationFree<AliasInjectionFactory<TLinear, Si<TPrefix, TUnit>>>.Item));
    public static Alias<TUnit> Metric<TUnit>()
        where TUnit : IMetricUnit, IAlias<TLinear>, IDimension
            => new(TUnit.Inject(AllocationFree<AliasInjectionFactory<TLinear, Metric<TUnit>>>.Item));
    public static Alias<TUnit> Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, IAlias<TLinear>, IDimension
            => new(TUnit.Inject(AllocationFree<AliasInjectionFactory<TLinear, Metric<TPrefix, TUnit>>>.Item));
    public static Alias<TUnit> Imperial<TUnit>()
        where TUnit : IImperialUnit, IAlias<TLinear>, IDimension
            => new(TUnit.Inject(AllocationFree<AliasInjectionFactory<TLinear, Imperial<TUnit>>>.Item));
    public static Alias<TUnit> NonStandard<TUnit>()
        where TUnit : INonStandardUnit, IAlias<TLinear>, IDimension
            => new(TUnit.Inject(AllocationFree<AliasInjectionFactory<TLinear, NonStandard<TUnit>>>.Item));
}


file sealed class AliasInjectionFactory<TLinear, TOuter> : ISystems<TLinear, Measure>
    where TOuter : IMeasure
    where TLinear : IDimension
{
    public Measure Si<TUnit>()
        where TUnit : ISiUnit, TLinear
            => Measure.Of<Measures.Alias<TOuter, Si<TUnit>>>();
    public Measure Metric<TUnit>()
        where TUnit : IMetricUnit, TLinear
            => Measure.Of<Measures.Alias<TOuter, Metric<TUnit>>>();
    public Measure Imperial<TUnit>()
        where TUnit : IImperialUnit, TLinear
            => Measure.Of<Measures.Alias<TOuter, Imperial<TUnit>>>();
    public Measure NonStandard<TUnit>()
        where TUnit : INonStandardUnit, TLinear
            => Measure.Of<Measures.Alias<TOuter, NonStandard<TUnit>>>();
}
