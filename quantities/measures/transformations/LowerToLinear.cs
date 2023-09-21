namespace Quantities.Measures.Transformations;

internal sealed class ToLinear : IFactory<Quantity>
{
    public Quantity Create<TMeasure>(in Double value) where TMeasure : IMeasure => Quantity.Of<TMeasure>(in value);
}
