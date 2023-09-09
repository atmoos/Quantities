using Quantities.Numerics;

namespace Quantities.Measures;

internal delegate Double Scale(in Double value);

internal sealed class Measure
{
    private readonly Polynomial conversion;
    internal Measure(in Polynomial conversion) => this.conversion = conversion;
    public required IInjector Injector { get; init; }
    public required String Representation { get; init; }
    public required Action<IWriter> Serialize { get; init; }
    public Measure With(IInjector injector) => new(this.conversion) {
        Injector = injector,
        Serialize = this.Serialize,
        Representation = this.Representation
    };
    public Polynomial Project(in Measure other) => this.conversion / other.conversion;
    public Double ToSi(in Double self) => this.conversion * self;
}
