using Quantities.Measures;

namespace Quantities.Serialization;

internal sealed class Builder<TMeasure> : IBuilder
    where TMeasure : IMeasure
{
    public IBuilder Append(IInject inject) => inject.Inject<TMeasure>();
    public Quant Build(in Double value) => Build<TMeasure>.With(in value);
}

internal sealed class AliasedBuilder<TMeasure, TAlias> : IBuilder
    where TMeasure : IMeasure
    where TAlias : IInjector, new()
{
    public IBuilder Append(IInject inject) => inject.Inject<TMeasure, TAlias>();
    public Quant Build(in Double value) => Build<TMeasure>.With<TAlias>(in value);
}
