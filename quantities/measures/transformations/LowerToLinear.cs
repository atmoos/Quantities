using Quantities.Dimensions;

namespace Quantities.Measures.Transformations;

internal sealed class LowerToLinear : ICreateLinear<Quant>
{
    public Quant Create<TMeasure>(in Double value) where TMeasure : IMeasure, ILinear
    {
        return Build<TMeasure>.With(in value);
    }
}