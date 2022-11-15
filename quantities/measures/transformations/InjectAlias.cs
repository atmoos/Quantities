using Quantities.Unit;
using Quantities.Unit.Si;

namespace Quantities.Measures.Transformations;

internal sealed class InjectAlias<TAlias, T> : IInject<TAlias, T>
{
    private readonly ICreate<T> creator;
    public InjectAlias(in ICreate<T> creator) => this.creator = creator;
    public T InjectSi<TInjectedUnit>(in Double value) where TInjectedUnit : ISiBaseUnit, TAlias
    {
        return this.creator.Create<Si<TInjectedUnit>>(in value);
    }
    public T InjectSiDerived<TInjectedUnit>(in Double value) where TInjectedUnit : ISiDerivedUnit, TAlias
    {
        return this.creator.Create<SiDerived<TInjectedUnit>>(in value);
    }
    public T InjectSiAccepted<TInjectedUnit>(in Double value) where TInjectedUnit : ISiAcceptedUnit, TAlias
    {
        return this.creator.Create<SiAccepted<TInjectedUnit>>(in value);
    }
    public T InjectOther<TInjectedUnit>(in Double value) where TInjectedUnit : IUnit, ITransform, TAlias
    {
        return this.creator.Create<Other<TInjectedUnit>>(in value);
    }
}