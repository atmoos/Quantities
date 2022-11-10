using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Measures.Si;

namespace Quantities.Quantities.Builders;

internal sealed class SiBuilder<TQuantity, TNominator> : IBuilder<TQuantity>
    where TQuantity : Dimensions.IDimension, IEquatable<TQuantity>, IFormattable
    where TNominator : ISi, ILinear
{
    private readonly Double value;
    public SiBuilder(in Double value) => this.value = value;
    Quant IBuilder<TQuantity>.By<TUnit>()
    {
        return BuildSi<SiDivide<TNominator, Accepted<TUnit>>>.With(in this.value);
    }

    Quant IBuilder<TQuantity>.By<TPrefix, TUnit>()
    {
        return BuildSi<SiDivide<TNominator, Si<TPrefix, TUnit>>>.With(in this.value);
    }
}