using Quantities.Measures;

namespace Quantities.Serialization;

internal sealed class AliasedBuilder<TMeasure, TAlias> : IBuilder
    where TMeasure : IMeasure
    where TAlias : IInjector, new()
{
    public IBuilder Append(IInject inject) => inject.Inject<TMeasure>().With<TAlias>();
    public Quant Build(in Double value) => Build<TMeasure>.With<TAlias>(in value);
    public IBuilder With<TAlias1>() where TAlias1 : IInjector, new() => throw new KeepUnusedException(this);
}
