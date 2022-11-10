using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Measures.Si;

namespace Quantities.Quantities.Builders;

internal sealed class SiTo<TQuantity, TNominator> : IBuilder<TQuantity>
    where TQuantity : Dimensions.IDimension, IEquatable<TQuantity>, IFormattable
    where TNominator : ISi, ILinear
{
    private readonly Quant quant;
    public SiTo(in Quant quant) => this.quant = quant;
    Quant IBuilder<TQuantity>.By<TUnit>()
    {
        return BuildSi<SiDivide<TNominator, Accepted<TUnit>>>.With(in this.quant);
    }

    Quant IBuilder<TQuantity>.By<TPrefix, TUnit>()
    {
        return BuildSi<SiDivide<TNominator, Si<TPrefix, TUnit>>>.With(in this.quant);
    }
}