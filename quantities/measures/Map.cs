namespace Quantities.Measures;

internal delegate Double Scale(in Double value);

internal sealed class Map
{
    public required Scale ToSi { get; init; }
    public required Scale FromSi { get; init; }
    public required IInjector Injector { get; init; }
    public required String Representation { get; init; }

    public Map With(IInjector injector) => new() {
        Injector = injector,
        ToSi = this.ToSi,
        FromSi = this.FromSi,
        Representation = this.Representation
    };
}
