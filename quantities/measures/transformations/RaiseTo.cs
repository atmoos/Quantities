using Quantities.Dimensions;

namespace Quantities.Measures.Transformations;

internal sealed class RaiseTo<TDim> : ICreate<Quant>
    where TDim : IDimension
{
    public Quant Create<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Power<TDim, TMeasure>>.With(in value);
}