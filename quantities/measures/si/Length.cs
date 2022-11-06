using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Unit.Si;

namespace Quantities.Measures.Si;
public readonly struct Length<TPrefix, TUnit> : IMeasure, ILength
    where TPrefix : IPrefix
    where TUnit : ISiUnit, ILength
{
    private static readonly Map<SiKernel<Si<TPrefix, TUnit>>> map = new();
    public static Quant Create(in Double value) => new Quant(value, map);
}