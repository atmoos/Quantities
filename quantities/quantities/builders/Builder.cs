using Quantities.Measures;

namespace Quantities.Quantities.Builders;

internal sealed class Builder<TQuantity, TNominator> : IBuilder<TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    where TNominator : IMeasure
{
    private readonly Double value;
    public Builder(in Double value) => this.value = value;
    Quant IBuilder<TQuantity>.By<TMeasure>() => Build<Fraction<TNominator, TMeasure>>.With(in this.value);
}

public readonly struct Create<TQuantity> : ICreate<IBuilder<TQuantity>>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
{
    private readonly Double value;
    internal Create(in Double value) => this.value = value;
    IBuilder<TQuantity> ICreate<IBuilder<TQuantity>>.Create<TMeasure>() => new Builder<TQuantity, TMeasure>(in this.value);
}