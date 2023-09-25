using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Serialization;

internal sealed class AliasInjectorFactory<TLinear> : ISystems<TLinear, IInject>
    where TLinear : IDimension, ILinear
{
    private readonly IInject injector;
    public AliasInjectorFactory(IInject injector) => this.injector = injector;
    public IInject Si<TUnit>()
        where TUnit : ISiUnit, TLinear
            => new AliasInjector<Si<TUnit>>(this.injector);
    public IInject Metric<TUnit>()
        where TUnit : IMetricUnit, TLinear
            => new AliasInjector<Metric<TUnit>>(this.injector);
    public IInject Imperial<TUnit>()
        where TUnit : IImperialUnit, TLinear
            => new AliasInjector<Imperial<TUnit>>(this.injector);
    public IInject NonStandard<TUnit>()
        where TUnit : INoSystemUnit, TLinear
            => new AliasInjector<NonStandard<TUnit>>(this.injector);
}
