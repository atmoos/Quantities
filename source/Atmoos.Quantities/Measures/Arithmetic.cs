using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Measures;

internal static class Arithmetic<TSelf>
    where TSelf : IMeasure
{
    private static readonly IVisitor injector = new Injector();
    public static Result Map<TRight>(in Polynomial poly, Dimension target) where TRight : IMeasure
    {
        if (target is Unit) {
            return new(poly, Measure.Of<Identity>());
        }
        var visitor = new Visitor(injector, target);
        var left = TSelf.InjectLinear(visitor);
        var builder = TRight.InjectLinear(left);
        return builder.Build(poly, target);
    }

    public static TResult Invert<TResult, TRight>(IInject<TResult> inject)
        where TRight : IMeasure
    {
        var left = TSelf.InjectInverse(new Injector<TResult>(inject));
        return TRight.InjectInverse(left);
    }
}

file sealed class Injector<TResult>(IInject<TResult> resultInjector) : IInject<IInject<TResult>>
{
    public IInject<TResult> Inject<TMeasure>() where TMeasure : IMeasure => new Left<TMeasure>(resultInjector);
    private sealed class Left<TLeft>(IInject<TResult> resultInjector) : IInject<TResult>
        where TLeft : IMeasure
    {
        public TResult Inject<TRight>() where TRight : IMeasure => resultInjector.Inject<Product<TLeft, TRight>>();
    }
}

file sealed class Injector : IVisitor
{
    public Result Build(Polynomial poly, Dimension target)
    {
        throw new InvalidOperationException($"Cannot build a result for '{target}'. The target dimension is not supported.");
    }

    public IVisitor Inject<TMeasure>() where TMeasure : IMeasure
        => new Scalar<TMeasure>();
    private sealed class Scalar<TInjected>() : IVisitor
        where TInjected : IMeasure
    {
        public Result Build(Polynomial poly, Dimension target)
        {
            return new(poly / TInjected.Poly, Measure.Of<TInjected>());
        }
        public IVisitor Inject<TMeasure>() where TMeasure : IMeasure => new Scalar<Product<TInjected, TMeasure>>();
    }
}

file sealed class Visitor : IVisitor
{
    private IVisitor fallback;
    private readonly IVisitor inject;
    private readonly List<Scalar> targets;

    private Visitor(IVisitor inject, List<Scalar> targets, IVisitor fallback) => (this.inject, this.targets, this.fallback) = (inject, targets, fallback);
    public Visitor(IVisitor inject, IEnumerable<Scalar> targets) => (this.inject, this.targets, this.fallback) = (inject, targets.ToList(), new Fallback());
    public Result Build(Polynomial poly, Dimension target)
        => this.targets.Count == 0 ? this.inject.Build(poly, target) : this.fallback.Build(poly, target);

    public IVisitor Inject<TMeasure>()
        where TMeasure : IMeasure
    {
        Scalar? match;
        if ((match = this.targets.FirstOrDefault(t => t.CommonRoot(TMeasure.D))) != null) {
            this.targets.Remove(match);
            return new Visitor(this.inject.Raise<TMeasure>(match), this.targets, this.fallback);
        }
        this.fallback = this.fallback.Inject<TMeasure>();
        return this;
    }

    private sealed class Fallback : IVisitor
    {
        public Result Build(Polynomial poly, Dimension target)
        {
            throw new InvalidOperationException($"Cannot build a result for '{target}'. The target dimension is not supported.");
        }

        public IVisitor Inject<TInjected>() where TInjected : IMeasure => new Fallback<TInjected>();
    }

    private sealed class Fallback<TMeasure> : IVisitor
        where TMeasure : IMeasure
    {
        public Result Build(Polynomial poly, Dimension target)
        {
            return new(poly / TMeasure.Poly, Measure.Of<TMeasure>());
        }

        public IVisitor Inject<TInjected>()
            where TInjected : IMeasure
                => new Fallback<Product<TMeasure, TInjected>>();
    }
}

file static class Convenience
{
    public static IVisitor Raise<TMeasure>(this IVisitor inner, Dimension d) where TMeasure : IMeasure
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
