using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Measures.Other;

namespace Quantities.Quantities.Builders;

internal sealed class OtherTo<TQuantity, TNominator> : IBuilder<TQuantity>
    where TQuantity : Dimensions.IDimension, IEquatable<TQuantity>, IFormattable
    where TNominator : IOther, ILinear
{
    private readonly Quant quant;
    public OtherTo(in Quant quant) => this.quant = quant;
    Quant IBuilder<TQuantity>.By<TUnit>()
    {
        return BuildOther<Divide<TNominator, Other<TUnit>>>.With(in this.quant);
    }
    Quant IBuilder<TQuantity>.By<TPrefix, TUnit>()
    {
        throw new NotImplementedException();
    }
}