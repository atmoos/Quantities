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

internal sealed class QuotientInjector : IInject
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Nominator<TMeasure>();
    public IBuilder Inject<TMeasure, TAlias>() where TMeasure : IMeasure where TAlias : IInjector, new() => throw new ShouldBeUnusedException(this);

    // the nominator is the first half of a fractional dimension to be created.
    // Hence, it must itself be an instance of IInject to inject ... 
    private sealed class Nominator<TNominator> : IInject, IBuilder
        where TNominator : IMeasure
    {
        public Quant Build(in Double value) => Build<TNominator>.With(in value);

        // ... the denominator in a second step.
        public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<Quotient<TNominator, TMeasure>>();
        public IBuilder Inject<TMeasure, TAlias>() where TMeasure : IMeasure where TAlias : IInjector, new() => throw new ShouldBeUnusedException(this);
    }
}

internal sealed class ProductInjector : IInject
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new LeftTerm<TMeasure>();
    public IBuilder Inject<TMeasure, TAlias>() where TMeasure : IMeasure where TAlias : IInjector, new() => throw new ShouldBeUnusedException(this);

    // the "left term" is the first half of a multiplicative dimension (product) to be created.
    // Hence, it must itself be an instance of IInject to inject ... 
    private sealed class LeftTerm<TLeft> : IInject, IBuilder
        where TLeft : IMeasure
    {
        public Quant Build(in Double value) => Build<TLeft>.With(in value);

        // ... the "right term" in a second step.
        public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<Product<TLeft, TMeasure>>();
        public IBuilder Inject<TMeasure, TAlias>() where TMeasure : IMeasure where TAlias : IInjector, new() => throw new ShouldBeUnusedException(this);
    }
}