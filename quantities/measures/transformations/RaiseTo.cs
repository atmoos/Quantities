namespace Quantities.Measures.Transformations;

internal sealed class RaiseTo<TDim> : IInject<Quant>
    where TDim : IDimension
{
    public Quant Inject<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Power<TDim, TMeasure>>.With(in value);
}