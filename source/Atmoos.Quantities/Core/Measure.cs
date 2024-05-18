using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Core.Serialization;

namespace Atmoos.Quantities.Core;

internal abstract class Measure
{
    private readonly Polynomial conversion;
    private Measure(in Polynomial conversion) => this.conversion = conversion;
    public Double Project(Measure other, in Double value) => this.conversion / other.conversion * value;
    public abstract Result Invert();
    public abstract Result Multiply(Measure other);
    protected abstract Result Multiply<TMeasure>() where TMeasure : IMeasure;
    public abstract Result Divide(Measure other);
    protected abstract Result Divide<TMeasure>() where TMeasure : IMeasure;
    public abstract void Serialize(IWriter writer);
    public static Measure Of<TMeasure>() where TMeasure : IMeasure => AllocationFree<Impl<TMeasure>>.Item;

    private sealed class Impl<TMeasure> : Measure
        where TMeasure : IMeasure
    {
        public Impl() : base(TMeasure.Poly) { }
        public override Result Invert() => TMeasure.Inverse;
        public override Result Multiply(Measure other) => other.Multiply<TMeasure>();
        public override Result Divide(Measure other) => other.Divide<TMeasure>();
        public override void Serialize(IWriter writer) => TMeasure.Write(writer);
        public override String ToString() => TMeasure.Representation;
        protected override Result Multiply<TOtherMeasure>() => TOtherMeasure.Multiply<TMeasure>();
        protected override Result Divide<TOtherMeasure>() => TOtherMeasure.Divide<TMeasure>();
    }
}
