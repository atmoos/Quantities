using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Core.Serialization;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;

namespace Atmoos.Quantities.Core;

internal abstract class Measure
{
    private readonly Polynomial conversion;
    public Dimension D { get; }
    private Measure(in Polynomial conversion, Dimension d) => (this.conversion, this.D) = (conversion, d);
    public Double Project(Measure other, in Double value) => this.conversion / other.conversion * value;
    public abstract Result Inverse { get; }
    public abstract Result Multiply(Measure other);
    protected abstract Result Multiply<TMeasure>() where TMeasure : IMeasure;
    public abstract Result Divide(Measure other);
    protected abstract Result Divide<TMeasure>() where TMeasure : IMeasure;
    public abstract void Serialize(IWriter writer);
    public static Measure Of<TMeasure>() where TMeasure : IMeasure => AllocationFree<Impl<TMeasure>>.Item;

    // ToDo: Move the exponent part of all measures up to the Measure class.
    //       Ideally, this will simplify the implementation of all Measure classes
    //       and solve the ambiguous meaning of the exponent...
    //       The Power measure should be able to be deleted as well as the Quotient measure.
    private sealed class Impl<TMeasure> : Measure
        where TMeasure : IMeasure
    {
        public Impl() : base(TMeasure.Poly, TMeasure.D) { }
        public override Result Inverse => SafeInverse<TMeasure>.Value;
        public override Result Multiply(Measure other) => other.Multiply<TMeasure>();
        public override Result Divide(Measure other) => other.Divide<TMeasure>();
        public override void Serialize(IWriter writer) => TMeasure.Write(writer, TMeasure.D.E);
        public override String ToString() => TMeasure.Representation;
        protected override Result Multiply<TOtherMeasure>() => Multiplication<TOtherMeasure, TMeasure>.Result;
        protected override Result Divide<TOtherMeasure>() => Division<TOtherMeasure, TMeasure>.Result;
    }
    private sealed class Invert(Polynomial nominal) : IInject<Result>
    {
        public Result Inject<TMeasure>() where TMeasure : IMeasure
            => new(TMeasure.Poly / nominal, Of<TMeasure>());
    }

    private static class Multiplication<TLeft, TRight>
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        public static Result Result { get; } = Prod<TLeft>.Map<TRight>(TLeft.Poly * TRight.Poly, TLeft.D * TRight.D);
    }

    private static class Division<TLeft, TRight>
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        public static Result Result { get; } = Prod<TLeft>.Map<TRight>(TLeft.Poly / TRight.Poly, TLeft.D / TRight.D);
    }

    private static class SafeInverse<TMeasure>
        where TMeasure : IMeasure
    {
        public static Result Value { get; } = TMeasure.InjectInverse(new Invert(TMeasure.Poly));
    }
}
