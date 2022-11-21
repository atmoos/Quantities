using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Unit;

namespace Quantities.Measures;

internal interface IInjector
{
    T Inject<T>(in ICreate<T> creator, in Double value);
}

internal sealed class Linear<TMeasure> : IInjector
    where TMeasure : IMeasure, ILinear
{
    public T Inject<T>(in ICreate<T> creator, in Double value) => creator.Create<TMeasure>(in value);
}

internal sealed class Alias<TUnit, TAlias> : IInjector
    where TUnit : IInjectUnit<TAlias>
    where TAlias : Dimensions.IDimension
{
    public T Inject<T>(in ICreate<T> creator, in Double value) => TUnit.Inject(new Creator<TAlias, T>(creator), in value);
}

internal sealed class Alias<TPrefix, TUnit, TAlias> : IInjector
    where TPrefix : IPrefix
    where TUnit : IInjectUnit<TAlias>
    where TAlias : Dimensions.IDimension
{
    public T Inject<T>(in ICreate<T> creator, in Double value) => TUnit.Inject(new Creator<TAlias, T>(creator), TPrefix.ToSi(in value));
}