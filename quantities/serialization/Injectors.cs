using Quantities.Measures;

namespace Quantities.Serialization;

internal sealed class ScalarInjector : IInject
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<TMeasure>();
    public IBuilder Inject<TMeasure, TAlias>()
        where TMeasure : IMeasure
        where TAlias : IInjector, new() => new AliasedBuilder<TMeasure, TAlias>();
}

internal sealed class PowerInjector<TDim> : IInject
    where TDim : IDimension
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => Inject<TMeasure, Linear<TMeasure>>();
    public IBuilder Inject<TMeasure, TAlias>()
        where TMeasure : IMeasure
        where TAlias : IInjector, new() => new AliasedBuilder<Power<TDim, TMeasure>, TAlias>();
}

internal sealed class FractionInjector : IInject
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Outer<TMeasure>();
    public IBuilder Inject<TMeasure, TAlias>() where TMeasure : IMeasure where TAlias : IInjector, new() => throw new KeepUnusedException(this);

    private sealed class Outer<TNominator> : IInject, IBuilder
        where TNominator : IMeasure
    {
        public Quant Build(in Double value) => Build<TNominator>.With(in value);
        public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<Fraction<TNominator, TMeasure>>();
        public IBuilder Inject<TMeasure, TAlias>() where TMeasure : IMeasure where TAlias : IInjector, new() => throw new KeepUnusedException(this);
    }
}

internal sealed class ProductInjector : IInject
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Left<TMeasure>();
    public IBuilder Inject<TMeasure, TAlias>() where TMeasure : IMeasure where TAlias : IInjector, new() => throw new KeepUnusedException(this);

    private sealed class Left<TLeft> : IInject, IBuilder
        where TLeft : IMeasure
    {
        public Quant Build(in Double value) => Build<TLeft>.With(in value);
        public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<Product<TLeft, TMeasure>>();
        public IBuilder Inject<TMeasure, TAlias>() where TMeasure : IMeasure where TAlias : IInjector, new() => throw new KeepUnusedException(this);
    }
}