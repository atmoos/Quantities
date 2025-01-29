using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;

namespace Atmoos.Quantities.Serialization;

internal sealed class ScalarInjector : IInject<IBuilder>
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<TMeasure>();
}

internal sealed class PowerInjector<TDim> : IInject<IBuilder>
    where TDim : IExponent
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<Power<TDim, TMeasure>>();
}

internal sealed class AliasInjector<TLinear>(IInject<IBuilder> injector) : IInject<IBuilder>
    where TLinear : IMeasure, ILinear
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => injector.Inject<Alias<TMeasure, TLinear>>();
}

internal sealed class InverseInjector : IInject<IBuilder>
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<Inverse<TMeasure>>();
}

internal sealed class InvertibleInjector<TInverse>(IInject<IBuilder> injector) : IInject<IBuilder>
    where TInverse : IMeasure, ILinear
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => injector.Inject<Invertible<TMeasure, TInverse>>();
}

internal sealed class QuotientInjector : IInject<IBuilder>
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Nominator<TMeasure>();

    // the nominator is the first half of a fractional dimension to be created.
    // Hence, it must itself be an instance of IInject<IBuilder> to inject ...
    private sealed class Nominator<TNominator> : IInject<IBuilder>, IBuilder
        where TNominator : IMeasure
    {
        public Quantity Build(in Double value) => Quantity.Of<TNominator>(in value);
        // ... the denominator in a second step.
        public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<Product<TNominator, Inverse<TMeasure>>>();
    }
}

internal sealed class ProductInjector : IInject<IBuilder>
{
    public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new LeftTerm<TMeasure>();

    // the "left term" is the first half of a multiplicative dimension (product) to be created.
    // Hence, it must itself be an instance of IInject<IBuilder> to inject ...
    private sealed class LeftTerm<TLeft> : IInject<IBuilder>, IBuilder
        where TLeft : IMeasure
    {
        public Quantity Build(in Double value) => Quantity.Of<TLeft>(in value);
        // ... the "right term" in a second step.
        public IBuilder Inject<TMeasure>() where TMeasure : IMeasure => new Builder<Product<TLeft, TMeasure>>();
    }
}
