using Quantities.Measures;
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
    public T SiAccepted<TInjectedUnit>(in Double value) where TInjectedUnit : ISiAcceptedUnit, TAlias
    {
        return this.creator.Create<SiAccepted<TInjectedUnit>>(in value);
    }
    public T Other<TInjectedUnit>(in Double value) where TInjectedUnit : IUnit, ITransform, TAlias
    {
        return this.creator.Create<Other<TInjectedUnit>>(in value);
    }
}
