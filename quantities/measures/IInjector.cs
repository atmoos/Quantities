using Quantities.Numerics;
using Quantities.Prefixes;
using Quantities.Units;

namespace Quantities.Measures;

internal interface IFactory<out TResult>
{
    TResult Create<TMeasure>(in Double value) where TMeasure : IMeasure;
}

internal interface IInjector
{
    T Inject<T>(in IFactory<T> factory, in Double value);
}

internal sealed class Linear<TMeasure> : IInjector
    where TMeasure : IMeasure
{
    public T Inject<T>(in IFactory<T> factory, in Double value) => factory.Create<TMeasure>(in value);
}

internal sealed class Alias<TUnit, TAlias> : IInjector
    where TUnit : IInjectUnit<TAlias>
    where TAlias : Dimensions.IDimension
{
    public T Inject<T>(in IFactory<T> factory, in Double value) => TUnit.Inject(new Creator<TAlias, T>(in factory), in value);
}

internal sealed class Alias<TPrefix, TUnit, TAlias> : IInjector
    where TPrefix : IPrefix
    where TUnit : IInjectUnit<TAlias>
    where TAlias : Dimensions.IDimension
{
    private static readonly Polynomial prefixScaling = Polynomial.Of<TPrefix>();
    public T Inject<T>(in IFactory<T> creator, in Double value) => TUnit.Inject(new Creator<TAlias, T>(in creator), prefixScaling * value);
}
