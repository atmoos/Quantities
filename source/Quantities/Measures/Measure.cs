﻿using Quantities.Numerics;

namespace Quantities.Measures;

internal abstract class Measure
{
    private readonly Polynomial conversion;
    private Measure(in Polynomial conversion) => this.conversion = conversion;
    public Polynomial Project(Measure other) => this.conversion / other.conversion;
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
        public override Result Multiply(Measure other) => other.Multiply<TMeasure>();
        public override Result Divide(Measure other) => other.Divide<TMeasure>();
        public override void Serialize(IWriter writer) => TMeasure.Write(writer);
        public override String ToString() => TMeasure.Representation;
        protected override Result Multiply<TOtherMeasure>() => TOtherMeasure.Multiply<TMeasure>();
        protected override Result Divide<TOtherMeasure>() => TOtherMeasure.Divide<TMeasure>();
    }
}