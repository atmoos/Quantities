using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Measures.Other;

namespace Quantities.Quantities.Builders;

internal sealed class OtherBuilder<TQuantity, TNominator> : IBuilder<TQuantity>
    where TQuantity : Dimensions.IDimension, IEquatable<TQuantity>, IFormattable
    where TNominator : IOther, ILinear
{
    private readonly Double value;
    public OtherBuilder(in Double value) => this.value = value;
    Quant IBuilder<TQuantity>.By<TUnit>()
    {
        return BuildOther<Divide<TNominator, Other<TUnit>>>.With(in this.value);
    }
    Quant IBuilder<TQuantity>.By<TPrefix, TUnit>()
    {
        throw new NotImplementedException();
    }
}