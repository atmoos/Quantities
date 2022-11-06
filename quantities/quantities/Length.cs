using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Measures.Si;
using Quantities.Prefixes;
using Quantities.Unit.Si;

namespace Quantities.Quantities;

public readonly struct Length : ILength
{
    private readonly Quant quant;
    private Length(in Quant quant) => this.quant = quant;
    public Length ToSi<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiUnit, ILength
    {
        return new(Length<TPrefix, TUnit>.Create(in this.quant));
    }

    public Length Create<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix
        where TUnit : ISiUnit, ILength
    {
        return new(Length<TPrefix, TUnit>.Create(value));
    }
}