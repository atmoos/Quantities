using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Measures;

internal static class Prod<TSelf>
    where TSelf : IMeasure
{
    public static Result Multiply<TRight>() where TRight : IMeasure
    {
        var result = TSelf.D * TRight.D;
        if (result is Unit) {
            return new(TSelf.Poly * TRight.Poly, Measure.Of<Identity>());
        }
        var injector = new Raise<Measure>(result, new ScalarInjector());
        if (result is Scalar s) {
            var (measure, conversion) = (s.CommonRoot(TSelf.D), s.CommonRoot(TRight.D)) switch {
                (true, _) => (TSelf.InjectLinear(injector), TSelf.InjectLinear(new Conversion<TRight>())),
                (false, true) => (TRight.InjectLinear(injector), TRight.InjectLinear(new Conversion<TSelf>())),
                _ => throw new NotSupportedException($"Unknown dimension '{result}' encountered.")
            };
            return new(conversion, measure);
        }
        if (result is Product p) {
            var left = new Raise<IInject<Measure>>(p.L, new LeftInjector<Measure>(injector));
            var right = new Raise<Measure>(p.R, TSelf.InjectLinear(left));
            var measure = TRight.InjectLinear(right);
            return new(Polynomial.One, measure);
        }
        throw new NotImplementedException($"Unknown type '{result.GetType().Name}' of dimension '{result}' encountered.");
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

file sealed class Conversion<TSelf> : IInject<Polynomial>
    where TSelf : IMeasure
{
    public Polynomial Inject<TMeasure>() where TMeasure : IMeasure => TSelf.Poly / TMeasure.Poly.Pow(TSelf.D.E - TMeasure.D.E);
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
