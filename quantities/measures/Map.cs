using Quantities.Numerics;
using Quantities.Prefixes;

namespace Quantities.Measures;

internal abstract class Map
{
    private readonly Polynomial conversion;
    private Map(Polynomial conversion) => this.conversion = conversion;
    public required IInjector Injector { get; init; }
    public required IOperations Ops { get; init; }
    public required String Representation { get; init; }
    public required Action<IWriter> Serialize { get; init; }
    public Map With(IInjector injector) => With(injector, this.conversion);
    protected abstract Map With(IInjector injector, Polynomial polynomial);
    public abstract Double Project(Map other, in Double self);
    public abstract Quant Multiply(IPrefixScale scaling, Map other, in Double self);
    public abstract Quant Divide(IPrefixScale scaling, Map other, in Operands operands);
    protected abstract Polynomial Project<TOtherMeasure>() where TOtherMeasure : IMeasure;
    public Double ToSi(in Double self) => this.conversion.Evaluate(in self);
    public static Map Create<TMeasure>()
        where TMeasure : IMeasure => new MapImpl<TMeasure>() {
            Ops = TMeasure.Operations,
            Injector = new Linear<TMeasure>(),
            Serialize = TMeasure.Write,
            Representation = TMeasure.Representation
        };

    private sealed class MapImpl<TMeasure> : Map
        where TMeasure : IMeasure
    {
        public MapImpl() : base(Polynomial.Of<TMeasure>()) { }
        private MapImpl(Polynomial polynomial) : base(polynomial) { }
        public override Quant Multiply(IPrefixScale scaling, Map other, in Double self) => other.Ops.Multiply<TMeasure>(scaling, in self);
        public override Quant Divide(IPrefixScale scaling, Map other, in Operands operands) => other.Ops.Divide<TMeasure>(scaling, in operands);
        public override Double Project(Map other, in Double self)
        {
            var poly = other.Project<TMeasure>();
            return poly.Evaluate(in self);
        }
        protected override Polynomial Project<TOtherMeasure>() => Conversion<TOtherMeasure, TMeasure>.Polynomial;
        protected override Map With(IInjector injector, Polynomial polynomial) => new MapImpl<TMeasure>(polynomial) {
            Injector = injector,
            Ops = TMeasure.Operations,
            Serialize = TMeasure.Write,
            Representation = TMeasure.Representation
        };
    }
}
