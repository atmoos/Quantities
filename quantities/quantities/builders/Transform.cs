using Quantities.Measures;

namespace Quantities.Quantities.Builders;

internal sealed class Transform<TQuantity, TNominator> : IBuilder<TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    where TNominator : IMeasure
{
    private readonly Quant value;
    public Transform(in Quant value) => this.value = value;
    Quant IBuilder<TQuantity>.By<TMeasure>() => Build<Fraction<TNominator, TMeasure>>.With(in this.value);
}

public readonly struct To<TQuantity> : ICreate<IBuilder<TQuantity>>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
{
    private readonly Quant value;
    internal To(in Quant value) => this.value = value;
    IBuilder<TQuantity> ICreate<IBuilder<TQuantity>>.Create<TMeasure>() => new Transform<TQuantity, TMeasure>(in this.value);
}