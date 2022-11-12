namespace Quantities.Measures;

internal abstract class Map
{
    public abstract String Representation { get; }
    public abstract Double MapTo<TMeasure>(in Double value) where TMeasure : IMeasure;
    public abstract Double Project(Map map, in Double value);
    public abstract T Inject<T>(in Creator<T> creator);
}

internal sealed class Map<TMeasure> : Map
    where TMeasure : IMeasure
{
    public override String Representation => TMeasure.Representation;
    public override Double MapTo<TOtherMeasure>(in Double value)
    {
        Double siValue = TMeasure.ToSi(in value);
        return TOtherMeasure.FromSi(in siValue);
    }
    public override Double Project(Map map, in Double value) => map.MapTo<TMeasure>(in value);
    public override T Inject<T>(in Creator<T> creator) => creator.Create<TMeasure>();
}