namespace Quantities.Measures;

internal sealed class Map
{
    public required Projection Projection { get; init; }
    public required IInjector Injector { get; init; }
    public required String Representation { get; init; }

    public Map With(IInjector injector) => new() {
        Projection = this.Projection,
        Injector = injector,
        Representation = this.Representation
    };
}

internal abstract class Projection
{
    public abstract Double MapTo<TMeasure>(in Double value) where TMeasure : IMeasure;
    public abstract Double Project(Projection projection, in Double value);
}

internal sealed class Projection<TMeasure> : Projection
    where TMeasure : IMeasure
{
    public override Double Project(Projection map, in Double value) => map.MapTo<TMeasure>(in value);
    public override Double MapTo<TOtherMeasure>(in Double value)
    {
        Double siValue = TMeasure.ToSi(in value);
        return TOtherMeasure.FromSi(in siValue);
    }
}
