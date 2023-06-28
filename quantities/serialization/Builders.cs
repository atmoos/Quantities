using Quantities.Measures;

namespace Quantities.Serialization;

internal sealed class Builder<TMeasure> : IBuilder
    where TMeasure : IMeasure
{
    public Quant Build(in Double value) => Build<TMeasure>.With(in value);
}

internal sealed class AliasedBuilder<TMeasure, TAlias> : IBuilder
    where TMeasure : IMeasure
    where TAlias : IInjector, new()
{
    public Quant Build(in Double value) => Build<TMeasure>.With<TAlias>(in value);
}
