using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Unit.Si;

namespace Quantities.Measures.Si;
public readonly struct Length<TPrefix, TUnit> : IMeasure, ILength
    where TPrefix : IPrefix
    where TUnit : ISiUnit, ILength
{
    private static readonly Map map = new Map<SiKernel<Si<TPrefix, TUnit>>>();
    public static Quant Create(in Double value) => new(value, in map);
    public static Quant Create(in Quant value)
    {
        var siValue = value.Project<SiKernel<Si<TPrefix, TUnit>>>();
        return new Quant(in siValue, in map);
    }
}