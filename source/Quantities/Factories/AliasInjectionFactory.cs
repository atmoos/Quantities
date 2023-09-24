using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Units;

namespace Quantities.Factories;

file sealed class AliasInjector<TCreate, TLinear> : IInject<TCreate>
    where TCreate : struct, ICreate
    where TLinear : IMeasure, ILinear
{
    public Quantity Inject<TMeasure>(in TCreate create)
        where TMeasure : IMeasure => create.Create<Alias<TMeasure, TLinear>>();
}

internal sealed class AliasInjectionFactory<TCreate, TLinear> : ISystems<TLinear, IInject<TCreate>>
    where TCreate : struct, ICreate
    where TLinear : IDimension
{
    public IInject<TCreate> Si<TUnit>()
        where TUnit : ISiUnit, TLinear
            => AllocationFree<AliasInjector<TCreate, Si<TUnit>>>.Item;
    public IInject<TCreate> Metric<TUnit>()
        where TUnit : IMetricUnit, TLinear
            => AllocationFree<AliasInjector<TCreate, Metric<TUnit>>>.Item;
    public IInject<TCreate> Imperial<TUnit>()
        where TUnit : IImperialUnit, TLinear
            => AllocationFree<AliasInjector<TCreate, Imperial<TUnit>>>.Item;
    public IInject<TCreate> NonStandard<TUnit>()
        where TUnit : INonStandardUnit, TLinear
            => AllocationFree<AliasInjector<TCreate, NonStandard<TUnit>>>.Item;
}
