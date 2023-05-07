using Quantities.Measures;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Units;

public interface IInjectUnit<TDimension>
    where TDimension : Dimensions.IDimension
{
    static abstract T Inject<T>(in Creator<TDimension, T> inject, in Double self);
}

public readonly ref struct Creator<TAlias, T>
    where TAlias : Dimensions.IDimension
{
    private readonly IInject<T> injector;
    internal Creator(in IInject<T> injector) => this.injector = injector;
    public T Si<TInjectedUnit>(in Double value) where TInjectedUnit : ISiUnit, TAlias
    {
        return this.injector.Inject<Si<TInjectedUnit>>(in value);
    }
    public T Metric<TInjectedUnit>(in Double value) where TInjectedUnit : IMetricUnit, TAlias
    {
        return this.injector.Inject<Metric<TInjectedUnit>>(in value);
    }
    public T Imperial<TInjectedUnit>(in Double value) where TInjectedUnit : IImperialUnit, ITransform, TAlias
    {
        return this.injector.Inject<Imperial<TInjectedUnit>>(in value);
    }
    public T NonStandard<TInjectedUnit>(in Double value) where TInjectedUnit : INoSystemUnit, ITransform, TAlias
    {
        return this.injector.Inject<NonStandard<TInjectedUnit>>(in value);
    }
}
