using Quantities.Unit;

namespace Quantities.Measures.Other;

internal sealed class AliasInject<TAlias, T> : IInject<TAlias, T>
{
    private readonly ICreateLinear<T> creator;
    public AliasInject(in ICreateLinear<T> creator) => this.creator = creator;
    public T Inject<TInjectedUnit>(in Double value) where TInjectedUnit : IUnit, ITransform, TAlias
    {
        return this.creator.CreateOther<Other<TInjectedUnit>>(in value);
    }
}