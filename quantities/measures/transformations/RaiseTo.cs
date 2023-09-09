namespace Quantities.Measures.Transformations;

internal sealed class RaiseTo<TDim> : IFactory<Quantity>
    where TDim : IDimension
{
    public Quantity Create<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Power<TDim, TMeasure>>.With<Linear<TMeasure>>(in value);
}
