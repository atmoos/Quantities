using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Serialization;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities.Core.Construction;

internal interface ICreateInjectable
{
    static abstract IInject<IBuilder> Create<TMeasure>(IInject<IBuilder> builder)
        where TMeasure : IMeasure, ILinear;
}

internal sealed class InjectorFactory<TInjector, TLinear>(IInject<IBuilder> injector) : ISystems<TLinear, IInject<IBuilder>>
    where TInjector : ICreateInjectable
    where TLinear : IDimension, ILinear
{
    public IInject<IBuilder> Si<TUnit>()
        where TUnit : ISiUnit, TLinear => TInjector.Create<Si<TUnit>>(injector);

    public IInject<IBuilder> Metric<TUnit>()
        where TUnit : IMetricUnit, TLinear => TInjector.Create<Metric<TUnit>>(injector);

    public IInject<IBuilder> Imperial<TUnit>()
        where TUnit : IImperialUnit, TLinear => TInjector.Create<Imperial<TUnit>>(injector);

    public IInject<IBuilder> NonStandard<TUnit>()
        where TUnit : INonStandardUnit, TLinear => TInjector.Create<NonStandard<TUnit>>(injector);
}
