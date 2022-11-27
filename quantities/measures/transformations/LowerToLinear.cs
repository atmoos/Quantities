namespace Quantities.Measures.Transformations;

internal sealed class ToLinear : ICreate<Quant>
{
    public Quant Create<TMeasure>(in Double value) where TMeasure : IMeasure
    {
        return Build<TMeasure>.With(in value);
    }
}