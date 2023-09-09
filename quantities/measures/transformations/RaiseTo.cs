namespace Quantities.Measures.Transformations;

internal sealed class RaiseTo<TDim> : IFactory<Quantity>
    where TDim : IExponent
{
    public Quantity Create<TMeasure>(in Double value) where TMeasure : IMeasure => Quantity.Of<Power<TDim, TMeasure>, Linear<TMeasure>>(in value);
}
