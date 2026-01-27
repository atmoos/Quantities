using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Measures;

internal static class Arithmetic<TSelf>
    where TSelf : IMeasure
{
    private static readonly IVisitor injector = new Injector();

    public static Result? Map<TRight>(in Polynomial poly, Dimension target)
        where TRight : IMeasure
    {
        if (target is Unit) {
            return new(poly, in Measure.Of<Identity>());
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
    public IInject<TResult> Inject<TMeasure>()
        where TMeasure : IMeasure => new Left<TMeasure>(resultInjector);

    private sealed class Left<TLeft>(IInject<TResult> resultInjector) : IInject<TResult>
        where TLeft : IMeasure
    {
        public TResult Inject<TRight>()
            where TRight : IMeasure =>
            (TLeft.D.E, TRight.D.E) switch {
                (0, 0) => resultInjector.Inject<Identity>(),
                ( < 0, > 0) => resultInjector.Inject<Product<TRight, TLeft>>(),
                (0, _) => resultInjector.Inject<TRight>(),
                (_, 0) => resultInjector.Inject<TLeft>(),
                _ => resultInjector.Inject<Product<TLeft, TRight>>()
            };
    }
}

file sealed class Injector : IVisitor
{
    public Result? Build(Polynomial poly, Dimension target)
    {
        throw new InvalidOperationException($"Cannot build a result for '{target}'. The target dimension is not supported.");
    }

    public IVisitor Inject<TMeasure>()
        where TMeasure : IMeasure => new Scalar<TMeasure>();
}

file sealed class Scalar<TInjected>() : IVisitor
    where TInjected : IMeasure
{
    public Result? Build(Polynomial poly, Dimension target)
    {
        return new(poly / TInjected.Poly, in Measure.Of<TInjected>());
    }

    public IVisitor Inject<TMeasure>()
        where TMeasure : IMeasure => new Scalar<Product<TInjected, TMeasure>>();
}

file sealed class Visitor : IVisitor
{
    private static readonly Injector injector = new();
    private readonly IVisitor fallback;
    private readonly IVisitor inject;
    private readonly List<Scalar> targets;

    private Visitor(IVisitor inject, List<Scalar> targets, IVisitor fallback) => (this.inject, this.targets, this.fallback) = (inject, targets, fallback);

    public Visitor(IVisitor inject, IEnumerable<Scalar> targets) => (this.inject, this.targets, this.fallback) = (inject, targets.ToList(), new Fallback());

    public Result? Build(Polynomial poly, Dimension target) => this.targets.Count == 0 ? this.inject.Build(poly, target) : this.fallback.Build(poly, target);

    public IVisitor Inject<TMeasure>()
        where TMeasure : IMeasure
    {
        Scalar? match;
        this.fallback.Inject<TMeasure>();
        if ((match = this.targets.FirstOrDefault(t => t.CommonRoot(TMeasure.D))) != null) {
            this.targets.Remove(match);
            return new Visitor(TMeasure.Power(this.inject, match.E), this.targets, this.fallback);
        }
        return this;
    }

    private sealed class Fallback : IVisitor
    {
        private readonly List<IFallbackFactory> factories = [];

        public Result? Build(Polynomial poly, Dimension target)
        {
            var builder = this.factories.Select(f => f.Matches(target)).FirstOrDefault(b => b is not null);
            if (builder is not null) {
                return builder.Build(poly);
            }
            // ToDo: This is a bit wonky. Probably we should expand over all dimensions...
            if (target is Product p) {
                var leftArgument = this.factories.Select(f => f.Matches(p.L)).FirstOrDefault(b => b is not null);
                var rightArgument = this.factories.Select(f => f.Matches(p.R)).FirstOrDefault(b => b is not null);
                if (leftArgument is not null && rightArgument is not null) {
                    var chain = leftArgument.Chain(rightArgument);
                    return chain.Build(poly, target);
                }
            }
            return null;
        }

        public IVisitor Inject<TInjected>()
            where TInjected : IMeasure
        {
            this.factories.Add(new Fallback<TInjected>());
            return this;
        }
    }

    private interface IFallbackFactory
    {
        IFallbackBuilder? Matches(Dimension dimension);
    }

    private sealed class Fallback<TMeasure> : IFallbackFactory
        where TMeasure : IMeasure
    {
        public IFallbackBuilder? Matches(Dimension dimension) => TMeasure.D.CommonRoot(dimension) ? new Builder<TMeasure>(dimension) : null;
    }

    private interface IChain
    {
        Result? Build(Polynomial poly, Dimension target);
    }

    private interface IFallbackBuilder
    {
        Result? Build(Polynomial poly);
        IChain Chain(IFallbackBuilder next);
        IChain Chain<TOther>(Dimension other)
            where TOther : IMeasure;
    }

    private sealed class Builder<TMeasure>(Dimension target) : IFallbackBuilder
        where TMeasure : IMeasure
    {
        public Result? Build(Polynomial poly) => TMeasure.Power(new Injector(), target.E).Build(poly, target);

        public IChain Chain(IFallbackBuilder next) => next.Chain<TMeasure>(target);

        public IChain Chain<TOther>(Dimension other)
            where TOther : IMeasure => new Chain<TMeasure, TOther>(target, other);
    }

    private sealed class Chain<TLeft, TRight>(Dimension left, Dimension right) : IChain
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        public Result? Build(Polynomial poly, Dimension target)
        {
            var leftPower = TLeft.Power(injector, left.E);
            var combo = TRight.Power(leftPower, right.E);
            return combo.Build(poly, target);
        }
    }
}
