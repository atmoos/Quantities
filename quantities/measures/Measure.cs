using Quantities.Numerics;

namespace Quantities.Measures;

internal delegate Double Scale(in Double value);

internal abstract class Measure
{
    private readonly Polynomial conversion;
    private Measure(in Polynomial conversion) => this.conversion = conversion;
    public Polynomial Project(in Measure other) => this.conversion / other.conversion;
    public Double ToSi(in Double self) => this.conversion * self;
    public abstract void Serialize(IWriter writer);
    public abstract T Inject<T>(IFactory<T> factory, in Double value);
    public static Measure Create<TMeasure>()
        where TMeasure : IMeasure => new Impl<TMeasure, Linear<TMeasure>>();
    public static Measure Create<TMeasure, TInjector>()
    where TMeasure : IMeasure where TInjector : IInjector => new Impl<TMeasure, TInjector>();

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
