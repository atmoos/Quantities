using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Units;

namespace Quantities.Serialization;

internal sealed class AliasInjectorFactory<TLinear> : ISystems<TLinear, IInject<IBuilder>>
    where TLinear : IDimension, ILinear
{
    private readonly IInject<IBuilder> injector;
    public AliasInjectorFactory(IInject<IBuilder> injector) => this.injector = injector;
    public IInject<IBuilder> Si<TUnit>()
        where TUnit : ISiUnit, TLinear
            => new AliasInjector<Si<TUnit>>(this.injector);
    public IInject<IBuilder> Metric<TUnit>()
        where TUnit : IMetricUnit, TLinear
            => new AliasInjector<Metric<TUnit>>(this.injector);
    public IInject<IBuilder> Imperial<TUnit>()
        where TUnit : IImperialUnit, TLinear
            => new AliasInjector<Imperial<TUnit>>(this.injector);
    public IInject<IBuilder> NonStandard<TUnit>()
        where TUnit : INonStandardUnit, TLinear
            => new AliasInjector<NonStandard<TUnit>>(this.injector);
}
