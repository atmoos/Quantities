using Quantities.Dimensions;
using Quantities.Measures;

namespace Quantities.Quantities.Builders;

internal sealed class Builder<TQuantity, TNominator> : IBuilder<TQuantity>
    where TQuantity : Dimensions.IDimension, IEquatable<TQuantity>, IFormattable
    where TNominator : IMeasure, ILinear
{
    private readonly Double value;
    public Builder(in Double value) => this.value = value;
    Quant IBuilder<TQuantity>.By<TUnit>()
    {
        return Build<Divide<TNominator, Other<TUnit>>>.With(in this.value);
    }

    Quant IBuilder<TQuantity>.By<TPrefix, TUnit>()
    {
        return Build<Divide<TNominator, Si<TPrefix, TUnit>>>.With(in this.value);
    }
}