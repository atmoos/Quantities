using Quantities.Numerics;
using Quantities.Prefixes;
using Quantities.Units;
using static Quantities.Extensions;

namespace Quantities.Measures;

internal interface IInject<out TResult>
{
    TResult Inject<TMeasure>(in Double value) where TMeasure : IMeasure;
}

internal interface IInjector
{
    T Inject<T>(in IInject<T> creator, in Double value);
}

internal sealed class Linear<TMeasure> : IInjector
    where TMeasure : IMeasure
{
    public T Inject<T>(in IInject<T> creator, in Double value) => creator.Inject<TMeasure>(in value);
}

internal sealed class Alias<TUnit, TAlias> : IInjector
    where TUnit : IInjectUnit<TAlias>
    where TAlias : Dimensions.IDimension
{
    public T Inject<T>(in IInject<T> creator, in Double value) => TUnit.Inject(new Creator<TAlias, T>(in creator), in value);
}

internal sealed class Alias<TPrefix, TUnit, TAlias> : IInjector
    where TPrefix : IPrefix
    where TUnit : IInjectUnit<TAlias>
    where TAlias : Dimensions.IDimension
{
    private static readonly Polynomial prefixScaling = PolynomialOf<TPrefix>();
    public T Inject<T>(in IInject<T> creator, in Double value) => TUnit.Inject(new Creator<TAlias, T>(in creator), prefixScaling.Evaluate(in value));
}