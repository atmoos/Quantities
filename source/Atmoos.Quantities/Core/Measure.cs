using System.Numerics;
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Core.Serialization;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;

namespace Atmoos.Quantities.Core;

internal abstract class Measure
    : IMultiplyOperators<Measure, Measure, Result>,
        IMultiplyOperators<Measure, Polynomial, Polynomial>,
        IDivisionOperators<Measure, Measure, Result>,
        IDivisionOperators<Measure, Polynomial, Polynomial>,
        ICastOperators<Measure, Polynomial>
{
    private static readonly IInject<Measure> invert = new Invert();
    private readonly Polynomial conversion;
    public Dimension D { get; }

    private Measure(in Polynomial conversion, Dimension d) => (this.conversion, this.D) = (conversion, d);

    public abstract ref readonly Measure Inverse { get; }
    protected abstract ref readonly Result Multiply(Measure other);
    protected abstract ref readonly Result Multiply<TMeasure>()
        where TMeasure : IMeasure;
    protected abstract ref readonly Result Divide(Measure other);
    protected abstract ref readonly Result Divide<TMeasure>()
        where TMeasure : IMeasure;
    public abstract void Serialize(IWriter writer);

    public static Result operator *(Measure left, Measure right) => left.Multiply(right);

    public static Result operator /(Measure left, Measure right) => left.Divide(right);

    public static Polynomial operator *(Measure left, Polynomial right) => left.conversion * right;

    public static Polynomial operator /(Measure left, Polynomial right) => left.conversion / right;

    public static implicit operator Polynomial(Measure self) => self.conversion;

    public static ref readonly Measure Of<TMeasure>()
        where TMeasure : IMeasure => ref AllocationFree<Measure, Impl<TMeasure>>.Item;

    private sealed class Impl<TMeasure> : Measure
        where TMeasure : IMeasure
    {
        public Impl()
            : base(TMeasure.Poly, TMeasure.D) { }

        public override ref readonly Measure Inverse => ref SafeInverse<TMeasure>.Value;

        protected override ref readonly Result Multiply(Measure other) => ref other.Multiply<TMeasure>();

        protected override ref readonly Result Divide(Measure other) => ref other.Divide<TMeasure>();

        public override void Serialize(IWriter writer) => TMeasure.Write(writer, TMeasure.D.E);

        public override String ToString() => TMeasure.Representation;

        protected override ref readonly Result Multiply<TOtherMeasure>() => ref Multiplication<TOtherMeasure, TMeasure>.Result;

        protected override ref readonly Result Divide<TOtherMeasure>() => ref Division<TOtherMeasure, TMeasure>.Result;
    }

    private static class Multiplication<TLeft, TRight>
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        private static readonly Result result = Fallback.Multiply<TLeft, TRight>(Arithmetic<TLeft>.Map<TRight>(TLeft.Poly * TRight.Poly, TLeft.D * TRight.D));
        public static ref readonly Result Result => ref result;
    }

    private static class Division<TNominator, TDenominator>
        where TNominator : IMeasure
        where TDenominator : IMeasure
    {
        private static readonly Result result = Fallback.Divide<TNominator, TDenominator>(
            Arithmetic<TNominator>.Map<TDenominator>(TNominator.Poly / TDenominator.Poly, TNominator.D / TDenominator.D)
        );
        public static ref readonly Result Result => ref result;
    }

    private sealed class Invert : IInject<Measure>
    {
        public Measure Inject<TMeasure>()
            where TMeasure : IMeasure => Of<TMeasure>();
    }

    private static class SafeInverse<TMeasure>
        where TMeasure : IMeasure
    {
        private static readonly Measure value = TMeasure.InjectInverse(invert);
        public static ref readonly Measure Value => ref value;
    }

    private static class Fallback
    {
        public static Result Multiply<TLeft, TRight>(Result? maybe)
            where TLeft : IMeasure
            where TRight : IMeasure => maybe is null ? new(Polynomial.One, in Of<Product<TLeft, TRight>>()) : maybe;

        public static Result Divide<TNominator, TDenominator>(Result? maybe)
            where TNominator : IMeasure
            where TDenominator : IMeasure => maybe is null ? TDenominator.InjectInverse(new Division<TNominator>()) : maybe;

        private sealed class Division<TNominator> : IInject<Result>
            where TNominator : IMeasure
        {
            public Result Inject<TMeasure>()
                where TMeasure : IMeasure => new(Polynomial.One, in Of<Product<TNominator, TMeasure>>());
        }
    }
}
