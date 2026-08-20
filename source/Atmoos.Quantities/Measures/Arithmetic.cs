using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Measures;

internal static class Arithmetic<TSelf>
    where TSelf : IMeasure
{
    // invertRight distinguishes division (TRight is the denominator) from multiplication, so that named
    // dimensionless measures (e.g. Radian) surviving on the denominator side cancel rather than accumulate.
    public static Result? Map<TRight>(in Polynomial poly, Dimension target, Boolean invertRight = false)
        where TRight : IMeasure
    {
        var visitor = new Visitor(AllocationFree<IVisitor, Scalar<Identity>>.Item, target);
        var left = TSelf.InjectLinear(visitor);
        var builder = TRight.InjectLinear(invertRight ? left.AsDenominator() : left);
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

file sealed class Scalar<TInjected> : IVisitor
    where TInjected : IMeasure
{
    public Result? Build(Polynomial poly, Dimension target)
    {
        return new(poly / TInjected.Poly, in Measure.Of<TInjected>());
    }

    public IVisitor Inject<TMeasure>()
        where TMeasure : IMeasure => typeof(TInjected) == typeof(Identity)
            ? AllocationFree<IVisitor, Scalar<TMeasure>>.Item
            : AllocationFree<IVisitor, Scalar<Product<TInjected, TMeasure>>>.Item;
}

file sealed class Visitor : IVisitor
{
    private readonly IVisitor fallback;
    private readonly IVisitor inject;
    private readonly List<Scalar> targets;
    private readonly Dictionary<Type, IDimensionless> dimensionless;
    private readonly Boolean invert;

    private Visitor(IVisitor inject, List<Scalar> targets, IVisitor fallback, Dictionary<Type, IDimensionless> dimensionless, Boolean invert) =>
        (this.inject, this.targets, this.fallback, this.dimensionless, this.invert) = (inject, targets, fallback, dimensionless, invert);

    public Visitor(IVisitor inject, IEnumerable<Scalar> targets) : this(inject, targets.ToList(), new Fallback(), [], false) { }

    // Marks subsequent measures as denominator-side, so a named dimensionless measure (e.g. Radian)
    // cancels rather than adds to one already captured from the numerator.
    public IVisitor AsDenominator() => new Visitor(this.inject, this.targets, this.fallback, this.dimensionless, true);

    public Result? Build(Polynomial poly, Dimension target)
    {
        var accumulator = this.dimensionless.Values.Aggregate(this.inject, (acc, d) => d.Apply(acc));
        return this.targets.Count == 0 ? accumulator.Build(poly, target) : this.fallback.Build(poly, target);
    }

    public IVisitor Inject<TMeasure>()
        where TMeasure : IMeasure
    {
        Scalar? match;
        this.fallback.Inject<TMeasure>();
        // A named but dimensionless measure (e.g. Radian) carries no dimension to match against, yet must
        // still be preserved; its net exponent (accounting for numerator/denominator polarity) is only
        // resolved once traversal completes, so equal and opposite contributions correctly cancel.
        if (TMeasure.D is Unit && typeof(TMeasure) != typeof(Identity)) {
            return new Visitor(this.inject, this.targets, this.fallback, Accumulate<TMeasure>(this.dimensionless, this.invert), this.invert);
        }
        if ((match = this.targets.FirstOrDefault(t => t.CommonRoot(TMeasure.D))) != null) {
            this.targets.Remove(match);
            return new Visitor(TMeasure.Power(this.inject, match.E), this.targets, this.fallback, this.dimensionless, this.invert);
        }
        return this;
    }

    private static Dictionary<Type, IDimensionless> Accumulate<TMeasure>(Dictionary<Type, IDimensionless> dimensionless, Boolean invert)
        where TMeasure : IMeasure
    {
        var delta = invert ? -1 : 1;
        var key = typeof(TMeasure);
        dimensionless[key] = dimensionless.TryGetValue(key, out var existing) ? existing.IncrementExponent(delta) : new Dimensionless<TMeasure>(delta);
        return dimensionless;
    }

    private interface IDimensionless
    {
        IDimensionless IncrementExponent(Int32 increment);
        IVisitor Apply(IVisitor accumulator);
    }

    private sealed class Dimensionless<TMeasure>(Int32 exponent) : IDimensionless
        where TMeasure : IMeasure
    {
        public IDimensionless IncrementExponent(Int32 increment) => new Dimensionless<TMeasure>(exponent + increment);
        public IVisitor Apply(IVisitor accumulator) => exponent == 0 ? accumulator : TMeasure.Power(accumulator, exponent);
    }

    private sealed class Fallback : IVisitor
    {
        private readonly List<IFallbackFactory> factories = [];

        public Result? Build(Polynomial poly, Dimension target)
        {
            var builder = Find(target);
            if (builder is not null) {
                return builder.Build(poly);
            }
            // ToDo: This is a bit wonky. Probably we should expand over all dimensions...
            if (target is Product p) {
                var leftArgument = Find(p.L);
                var rightArgument = Find(p.R);
                if (leftArgument is not null && rightArgument is not null) {
                    var chain = leftArgument.Chain(rightArgument);
                    return chain.Build(poly, target);
                }
            }
            return null;
        }

        private IFallbackBuilder? Find(Dimension target) =>
            this.factories.Select(f => f.Matches(target)).FirstOrDefault(b => b is not null);

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
        public Result? Build(Polynomial poly) => TMeasure.Power(AllocationFree<IVisitor, Scalar<Identity>>.Item, target.E).Build(poly, target);

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
            var leftPower = TLeft.Power(AllocationFree<IVisitor, Scalar<Identity>>.Item, left.E);
            var combo = TRight.Power(leftPower, right.E);
            return combo.Build(poly, target);
        }
    }
}
