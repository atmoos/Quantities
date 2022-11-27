using Quantities.Dimensions;
using Quantities.Measures;

namespace Quantities.Quantities.Builders;

internal sealed class Builder<TQuantity, TNominator> : IBuilder<TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    where TNominator : IMeasure, ILinear
{
    private readonly Double value;
    public Builder(in Double value) => this.value = value;
    Quant IBuilder<TQuantity>.By<TMeasure>() => Build<Fraction<TNominator, TMeasure>>.With(in this.value);
}
