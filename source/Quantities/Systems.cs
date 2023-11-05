using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;

namespace Quantities;

public static class Systems
{
    public static Scalar<TDim> Si<TDim>()
        where TDim : ISiUnit, IDimension => new(Factory.Of<Si<TDim>>());
    public static Scalar<TDim> Si<TPrefix, TDim>()
        where TPrefix : IMetricPrefix
        where TDim : ISiUnit, IDimension => new(Factory.Of<Si<TPrefix, TDim>>());
    public static Scalar<TDim> Metric<TDim>()
        where TDim : IMetricUnit, IDimension => new(Factory.Of<Metric<TDim>>());
    public static Scalar<TDim> Metric<TPrefix, TDim>()
        where TPrefix : IMetricPrefix
        where TDim : IMetricUnit, IDimension => new(Factory.Of<Metric<TPrefix, TDim>>());
    public static Scalar<TDim> Binary<TPrefix, TDim>()
        where TPrefix : IBinaryPrefix
        where TDim : IMetricUnit, IDimension => new(Factory.Of<Metric<TPrefix, TDim>>());
    public static Scalar<TDim> Imperial<TDim>()
        where TDim : IImperialUnit, IDimension => new(Factory.Of<Imperial<TDim>>());
    public static Scalar<TDim> NonStandard<TDim>()
        where TDim : INonStandardUnit, IDimension => new(Factory.Of<NonStandard<TDim>>());
    public static Cubic<TDim> Cubic<TDim>(in Scalar<TDim> scalar) where TDim : IUnit, IDimension => new(scalar.Factory);
    public static Square<TDim> Square<TDim>(in Scalar<TDim> scalar) where TDim : IUnit, IDimension => new(scalar.Factory);
}

public static class AliasOf<TLinear>
    where TLinear : IDimension
{
    public static Creation.Alias<TDim, TLinear> Si<TDim>()
        where TDim : ISiUnit, IAlias<TLinear>, IDimension
            => new(TDim.Inject(AllocationFree<AliasInjectionFactory<TLinear, Si<TDim>>>.Item));
    public static Creation.Alias<TDim, TLinear> Si<TPrefix, TDim>()
        where TPrefix : IMetricPrefix
        where TDim : ISiUnit, IAlias<TLinear>, IDimension
            => new(TDim.Inject(AllocationFree<AliasInjectionFactory<TLinear, Si<TPrefix, TDim>>>.Item));
    public static Creation.Alias<TDim, TLinear> Metric<TDim>()
        where TDim : IMetricUnit, IAlias<TLinear>, IDimension
            => new(TDim.Inject(AllocationFree<AliasInjectionFactory<TLinear, Metric<TDim>>>.Item));
    public static Creation.Alias<TDim, TLinear> Metric<TPrefix, TDim>()
        where TPrefix : IMetricPrefix
        where TDim : IMetricUnit, IAlias<TLinear>, IDimension
            => new(TDim.Inject(AllocationFree<AliasInjectionFactory<TLinear, Metric<TPrefix, TDim>>>.Item));
    public static Creation.Alias<TDim, TLinear> Imperial<TDim>()
        where TDim : IImperialUnit, IAlias<TLinear>, IDimension
            => new(TDim.Inject(AllocationFree<AliasInjectionFactory<TLinear, Imperial<TDim>>>.Item));
    public static Creation.Alias<TDim, TLinear> NonStandard<TDim>()
        where TDim : INonStandardUnit, IAlias<TLinear>, IDimension
            => new(TDim.Inject(AllocationFree<AliasInjectionFactory<TLinear, NonStandard<TDim>>>.Item));
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
