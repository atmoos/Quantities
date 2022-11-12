using Quantities.Dimensions;

namespace Quantities.Measures.Transformations;

internal sealed class RaiseTo<TDim> : ICreateLinear<Quant>
    where TDim : IDimension
{
    public Quant Create<TMeasure>(in Double value) where TMeasure : IMeasure, ILinear => Build<Power<TDim, TMeasure>>.With(in value);
}