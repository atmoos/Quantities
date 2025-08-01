using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Measures;

internal static class Prod<TSelf>
    where TSelf : IMeasure
{
    public static Result Map<TRight>(in Polynomial poly, Dimension target) where TRight : IMeasure
    {
        if (TSelf.D is Unit) {
            return new(Polynomial.One, Measure.Of<TRight>());
        }
        if (TRight.D is Unit) {
            return new(Polynomial.One, Measure.Of<TSelf>());
        }
        if (target is Unit) {
            return new(TSelf.Poly * TRight.Poly, Measure.Of<Identity>());
        }
        if (target is Scalar) {
            var visitor = new ScalarVisitor(new Raise<IVisitor>(target, new VisitorBuilder()));
            return TSelf.Visit(visitor, target).Build(poly)
                ?? TRight.Visit(visitor, target).Build(poly)
                ?? throw new InvalidOperationException($"Cannot build a result of dimension '{target}' with measures '{TSelf.Representation}' and '{TRight.Representation}'.");
        }
        if (target is Product p) {
            var injector = new Raise<Measure>(target, new ScalarInjector());
            var left = new Raise<IInject<Measure>>(p.L, new LeftInjector<Measure>(injector));
            var right = new Raise<Measure>(p.R, TSelf.InjectLinear(left));
            var measure = TRight.InjectLinear(right);
            return new(Polynomial.One, measure);
        }
        throw new NotImplementedException($"Unknown type '{target.GetType().Name}' of dimension '{target}' encountered.");
    }

    public static TResult Invert<TResult, TRight>(IInject<TResult> inject)
        where TRight : IMeasure
    {
        var left = TSelf.InjectInverse(new LeftInjector<TResult>(inject));
        return TRight.InjectInverse(left);
    }
}

file sealed class ScalarInjector : IInject<Measure>
{
    public Measure Inject<TMeasure>() where TMeasure : IMeasure => Measure.Of<TMeasure>();
}

file sealed class LeftInjector<TResult>(IInject<TResult> resultInjector) : IInject<IInject<TResult>>
{
    public IInject<TResult> Inject<TMeasure>() where TMeasure : IMeasure => new Left<TMeasure>(resultInjector);
    private sealed class Left<TLeft>(IInject<TResult> resultInjector) : IInject<TResult>
        where TLeft : IMeasure
    {
        public TResult Inject<TRight>() where TRight : IMeasure => resultInjector.Inject<Product<TLeft, TRight>>();
    }
}

file sealed class ScalarVisitor(IInject<IVisitor> raise) : IVisitor
{
    public Result? Build(Polynomial poly) => null;
    public IVisitor Inject<TMeasure>()
        where TMeasure : IMeasure => raise.Inject<TMeasure>();
}

file sealed class VisitorBuilder : IInject<IVisitor>
{
    public IVisitor Inject<TMeasure>() where TMeasure : IMeasure
        => new ScalarVisitor<TMeasure>();
    private sealed class ScalarVisitor<TInjected>() : IVisitor
    where TInjected : IMeasure
    {
        public Result Build(Polynomial poly)
        {
            return new(poly / TInjected.Poly, Measure.Of<TInjected>());
        }
        public IVisitor Inject<TMeasure>() where TMeasure : IMeasure => this;
    }
}

file sealed class Raise<TResult>(Dimension d, IInject<TResult> inner) : IInject<TResult>
{
    public TResult Inject<TMeasure>() where TMeasure : IMeasure
    => (d.E * Double.CopySign(1d, TMeasure.D.E)) switch {
        3 => inner.Inject<Power<Numerator<Three>, TMeasure>>(),
        2 => inner.Inject<Power<Numerator<Two>, TMeasure>>(),
        1 => inner.Inject<TMeasure>(),
        0 => inner.Inject<Identity>(),
        -1 => TMeasure.InjectInverse(inner),
        -2 => inner.Inject<Power<Denominator<Two>, TMeasure>>(),
        -3 => inner.Inject<Power<Denominator<Three>, TMeasure>>(),
        _ => throw new InvalidOperationException($"Cannot map '{d}' to a new measure. Exponent '{d.E}' is not supported.")
    };
}
