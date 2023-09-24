using Quantities.Measures;

namespace Quantities.Serialization;

internal sealed class Builder<TMeasure> : IBuilder
    where TMeasure : IMeasure
{
    public Quantity Build(in Double value) => Quantity.Of<TMeasure>(in value);
}
