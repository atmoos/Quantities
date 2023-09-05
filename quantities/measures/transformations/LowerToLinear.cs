namespace Quantities.Measures.Transformations;

internal sealed class ToLinear : IInject<Quantity>
{
    public Quantity Inject<TMeasure>(in Double value) where TMeasure : IMeasure
    {
        return Build<TMeasure>.With(in value);
    }
}