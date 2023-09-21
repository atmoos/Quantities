﻿using Quantities.Numerics;

namespace Quantities.Measures;

internal abstract class Measure
{
    private readonly Polynomial conversion;
    private Measure(in Polynomial conversion) => this.conversion = conversion;
    public Polynomial Project(in Measure other) => this.conversion / other.conversion;
    public Double ToSi(in Double self) => this.conversion * self;
    public abstract void Serialize(IWriter writer);
    public abstract T Inject<T>(IFactory<T> factory, in Double value);
    public static Measure Of<TMeasure>()
        where TMeasure : IMeasure => AllocationFree<Impl<TMeasure, Linear<TMeasure>>>.Item;
    public static Measure Of<TMeasure, TInjector>()
    where TMeasure : IMeasure where TInjector : IInjector => AllocationFree<Impl<TMeasure, TInjector>>.Item;

    private sealed class Impl<TMeasure, TInjector> : Measure
        where TMeasure : IMeasure
        where TInjector : IInjector
    {
        public Impl() : base(TMeasure.Poly) { }
        public override T Inject<T>(IFactory<T> factory, in Double value) => TInjector.Inject(in factory, in value);
        public override void Serialize(IWriter writer) => TMeasure.Write(writer);
        public override String ToString() => TMeasure.Representation;
    }
}
