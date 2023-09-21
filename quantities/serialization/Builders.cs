using Quantities.Measures;

namespace Quantities.Serialization;

internal sealed class Builder<TMeasure> : IBuilder
    where TMeasure : IMeasure
{
    public Quantity Build(in Double value) => Quantity.Of<TMeasure>(in value);
}

internal sealed class AliasedBuilder<TMeasure, TAlias> : IBuilder
    where TMeasure : IMeasure
    where TAlias : IInjector
{
    public Quantity Build(in Double value) => Quantity.Of<TMeasure, TAlias>(in value);
}
