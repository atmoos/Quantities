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
    private readonly ICreate<T> creator;
    internal Creator(in ICreate<T> creator) => this.creator = creator;
    public T Si<TInjectedUnit>(in Double value) where TInjectedUnit : ISiBaseUnit, TAlias
    {
        return this.creator.Create<Si<TInjectedUnit>>(in value);
    }
    public T SiDerived<TInjectedUnit>(in Double value) where TInjectedUnit : ISiDerivedUnit, TAlias
    {
        return this.creator.Create<SiDerived<TInjectedUnit>>(in value);
    }
    public T Metric<TInjectedUnit>(in Double value) where TInjectedUnit : IMetricUnit, TAlias
    {
        return this.creator.Create<Metric<TInjectedUnit>>(in value);
    }
    public T Imperial<TInjectedUnit>(in Double value) where TInjectedUnit : IImperial, ITransform, TAlias
    {
        return this.creator.Create<Imperial<TInjectedUnit>>(in value);
    }
    public T NonStandard<TInjectedUnit>(in Double value) where TInjectedUnit : INoSystem, ITransform, TAlias
    {
        return this.creator.Create<NonStandard<TInjectedUnit>>(in value);
    }
}
