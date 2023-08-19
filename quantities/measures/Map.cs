using Quantities.Numerics;

namespace Quantities.Measures;

internal delegate Double Scale(in Double value);

internal sealed class Map
{
    private readonly Polynomial conversion;
    internal Map(Polynomial conversion) => this.conversion = conversion;
    public required IInjector Injector { get; init; }
    public required String Representation { get; init; }
    public required Action<IWriter> Serialize { get; init; }
    public Map With(IInjector injector) => new(this.conversion) {
        Injector = injector,
        Serialize = this.Serialize,
        Representation = this.Representation
    };
    public Double Project(in Map other, in Double self)
    {
        var siValue = this.conversion.Evaluate(in self);
        return other.conversion.Inverse(in siValue);
    }
    public Double ToSi(in Double self) => this.conversion.Evaluate(in self);
}
