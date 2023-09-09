namespace Quantities.Measures.Transformations;

internal sealed class ToLinear : IFactory<Quantity>
{
    public Quantity Create<TMeasure>(in Double value) where TMeasure : IMeasure
    {
        return Build<TMeasure>.With(in value);
    }
}
