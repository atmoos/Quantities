using Quantities.Dimensions;
using Quantities.Measures;

namespace Quantities.Serialization;

internal sealed class ScalarInjector : IInject
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<TMeasure>();
}

internal sealed class PowerInjector<TDim> : IInject
    where TDim : IExponent
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<Power<TDim, TMeasure>>();
}

internal sealed class AliasInjector<TLinear> : IInject
    where TLinear : IMeasure, ILinear
{
    private readonly IInject inject;
    public AliasInjector(IInject inject) => this.inject = inject;
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => this.inject.Inject<Alias<TMeasure, TLinear>>();
}

internal sealed class QuotientInjector : IInject
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Nominator<TMeasure>();

    // the nominator is the first half of a fractional dimension to be created.
    // Hence, it must itself be an instance of IInject to inject ... 
    private sealed class Nominator<TNominator> : IInject, IBuilder
        where TNominator : IMeasure
    {
        public Quantity Build(in Double value) => Quantity.Of<TNominator>(in value);
        // ... the denominator in a second step.
        public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<Quotient<TNominator, TMeasure>>();
    }
}

internal sealed class ProductInjector : IInject
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new LeftTerm<TMeasure>();

    // the "left term" is the first half of a multiplicative dimension (product) to be created.
    // Hence, it must itself be an instance of IInject to inject ... 
    private sealed class LeftTerm<TLeft> : IInject, IBuilder
        where TLeft : IMeasure
    {
        public Quantity Build(in Double value) => Quantity.Of<TLeft>(in value);
        // ... the "right term" in a second step.
        public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<Product<TLeft, TMeasure>>();
    }
}

