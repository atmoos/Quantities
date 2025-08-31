using System.Numerics;
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Core.Serialization;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;

namespace Atmoos.Quantities.Core;

internal abstract class Measure :
    IMultiplyOperators<Measure, Measure, Result>,
    IMultiplyOperators<Measure, Polynomial, Polynomial>,
    IDivisionOperators<Measure, Measure, Result>,
    IDivisionOperators<Measure, Polynomial, Polynomial>,
    ICastOperators<Measure, Polynomial>
{
    private static readonly IInject<Measure> invert = new Invert();
    private readonly Polynomial conversion;
    public Dimension D { get; }
    private Measure(in Polynomial conversion, Dimension d) => (this.conversion, this.D) = (conversion, d);
    public abstract Measure Inverse { get; }
    protected abstract Result Multiply(Measure other);
    protected abstract Result Multiply<TMeasure>() where TMeasure : IMeasure;
    protected abstract Result Divide(Measure other);
    protected abstract Result Divide<TMeasure>() where TMeasure : IMeasure;
    public abstract void Serialize(IWriter writer);

    public static Result operator *(Measure left, Measure right) => left.Multiply(right);
    public static Result operator /(Measure left, Measure right) => left.Divide(right);
    public static Polynomial operator *(Measure left, Polynomial right) => left.conversion * right;
    public static Polynomial operator /(Measure left, Polynomial right) => left.conversion / right;
    public static implicit operator Polynomial(Measure self) => self.conversion;

    public static ref readonly Measure Of<TMeasure>() where TMeasure : IMeasure => ref AllocationFree<Measure, Impl<TMeasure>>.Item;

    private sealed class Impl<TMeasure> : Measure
        where TMeasure : IMeasure
    {
        public Impl() : base(TMeasure.Poly, TMeasure.D) { }
        public override Measure Inverse => SafeInverse<TMeasure>.Value;
        protected override Result Multiply(Measure other) => other.Multiply<TMeasure>();
        protected override Result Divide(Measure other) => other.Divide<TMeasure>();
        public override void Serialize(IWriter writer) => TMeasure.Write(writer, TMeasure.D.E);
        public override String ToString() => TMeasure.Representation;
        protected override Result Multiply<TOtherMeasure>() => Multiplication<TOtherMeasure, TMeasure>.Result;
        protected override Result Divide<TOtherMeasure>() => Division<TOtherMeasure, TMeasure>.Result;
    }

    private static class Multiplication<TLeft, TRight>
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        public static Result Result { get; } = Arithmetic<TLeft>.Map<TRight>(TLeft.Poly * TRight.Poly, TLeft.D * TRight.D);
    }

    private static class Division<TLeft, TRight>
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        public static Result Result { get; } = Arithmetic<TLeft>.Map<TRight>(TLeft.Poly / TRight.Poly, TLeft.D / TRight.D);
    }

    private sealed class Invert : IInject<Measure>
    {
        public Measure Inject<TMeasure>()
            where TMeasure : IMeasure => Of<TMeasure>();
    }

    private static class SafeInverse<TMeasure>
        where TMeasure : IMeasure
    {
        public static Measure Value { get; } = TMeasure.InjectInverse(invert);
    }
}
