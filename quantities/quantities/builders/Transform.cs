using Quantities.Dimensions;
using Quantities.Measures;

namespace Quantities.Quantities.Builders;

internal sealed class Transform<TQuantity, TNominator> : IBuilder<TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    where TNominator : IMeasure, ILinear
{
    private readonly Quant quant;
    public Transform(in Quant quant) => this.quant = quant;
    Quant IBuilder<TQuantity>.By<TMeasure>() => Build<Fraction<TNominator, TMeasure>>.With(in this.quant);
}