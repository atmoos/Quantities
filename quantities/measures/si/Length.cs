using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Unit.Si;

namespace Quantities.Measures.Si;
public class Length<TPrefix, TUnit> : IMeasure, ILength
    where TPrefix : IPrefix
    where TUnit : ISiUnit, ILength
{
    public static Quant Create(in Double value)
    {
        return Create<SiKernel<Si<TPrefix, TUnit>>>(in value);
    }
}