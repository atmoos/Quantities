using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Serialization;

namespace Atmoos.Quantities.Core.Construction;

internal sealed class ScalarInjector : IInject<IBuilder>
{
    public IBuilder Inject<TMeasure>()
        where TMeasure : IMeasure => new Builder<TMeasure>();
}

internal sealed class PowerInjector<TDim> : IInject<IBuilder>
    where TDim : INumber
{
    public IBuilder Inject<TMeasure>()
        where TMeasure : IMeasure => new Builder<Power<TMeasure, TDim>>();
}

internal sealed class AliasInjector<TLinear>(IInject<IBuilder> injector) : IInject<IBuilder>
    where TLinear : IMeasure, ILinear
{
    public IBuilder Inject<TMeasure>()
        where TMeasure : IMeasure => injector.Inject<Alias<TMeasure, TLinear>>();
}

internal sealed class InverseInjector : IInject<IBuilder>
{
    public IBuilder Inject<TMeasure>()
        where TMeasure : IMeasure => new Builder<Power<TMeasure, Negative<One>>>();
}

internal sealed class InvertibleInjector<TInverse>(IInject<IBuilder> injector) : IInject<IBuilder>
    where TInverse : IMeasure, ILinear
{
    public IBuilder Inject<TMeasure>()
        where TMeasure : IMeasure => injector.Inject<Invertible<TMeasure, TInverse>>();
}

internal sealed class ProductInjector(Int32 leftExp, Int32 rightExp) : IInject<IBuilder>
{
    public IBuilder Inject<TMeasure>()
        where TMeasure : IMeasure => new LeftTerm<TMeasure>(leftExp, rightExp);

    // the "left term" is the first half of a multiplicative dimension (product) to be created.
    // Hence, it must itself be an instance of IInject<IBuilder> to inject ...
    private sealed class LeftTerm<TLeft>(Int32 leftExp, Int32 rightExp) : IInject<IBuilder>, IBuilder
        where TLeft : IMeasure
    {
        public Quantity Build(in Double value) => Quantity.Of<TLeft>(in value);

        // ... the "right term" in a second step.
        public IBuilder Inject<TMeasure>()
            where TMeasure : IMeasure =>
            (leftExp, rightExp) switch {
                ( > 0, > 0) => new Builder<Product<TLeft, TMeasure>>(),
                ( > 0, < 0) => new Builder<Product<TLeft, Power<TMeasure, Negative<One>>>>(),
                ( < 0, > 0) => new Builder<Product<Power<TLeft, Negative<One>>, TMeasure>>(),
                ( < 0, < 0) => new Builder<Power<Product<TLeft, TMeasure>, Negative<One>>>(),
                _ => throw new NotSupportedException($"Cannot build a quantity product from dimensions; '{TLeft.D}', '{TMeasure.D}'."),
            };
    }
}
