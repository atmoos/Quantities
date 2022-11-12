using Quantities.Dimensions;
using Quantities.Measures;

namespace Quantities.Quantities.Builders;

internal sealed class Transform<TQuantity, TNominator> : IBuilder<TQuantity>
    where TQuantity : Dimensions.IDimension, IEquatable<TQuantity>, IFormattable
    where TNominator : IMeasure, ILinear
{
    private readonly Quant quant;
    public Transform(in Quant quant) => this.quant = quant;
    Quant IBuilder<TQuantity>.By<TUnit>()
    {
        return Build<Divide<TNominator, Other<TUnit>>>.With(in this.quant);
    }

    Quant IBuilder<TQuantity>.By<TPrefix, TUnit>()
    {
        return Build<Divide<TNominator, Si<TPrefix, TUnit>>>.With(in this.quant);
    }
}