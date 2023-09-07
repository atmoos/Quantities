namespace Quantities.Measures.Transformations;

internal sealed class RaiseTo<TDim> : IInject<Quantity>
    where TDim : IDimension
{
    public Quantity Inject<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Power<TDim, TMeasure>>.With(in value);
}
