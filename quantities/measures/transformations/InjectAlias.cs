using Quantities.Unit;

namespace Quantities.Measures.Transformations;

internal sealed class InjectAlias<TAlias, T> : IInject<TAlias, T>
{
    private readonly ICreateLinear<T> creator;
    public InjectAlias(in ICreateLinear<T> creator) => this.creator = creator;
    public T Inject<TInjectedUnit>(in Double value) where TInjectedUnit : IUnit, ITransform, TAlias
    {
        return this.creator.Create<Other<TInjectedUnit>>(in value);
    }
}