using Quantities.Measures;

namespace Quantities.Serialization;

internal sealed class PowerBuilder<TDim, TMeasure> : IBuilder
    where TDim : IDimension
    where TMeasure : IMeasure
{
    public IBuilder Append(IInject inject) => inject.Inject<TMeasure>();
    public Quant Build(in Double value) => Build<Power<TDim, TMeasure>>.With<Linear<TMeasure>>(in value);
    public IBuilder With<TAlias>() where TAlias : IInjector, new() => new AliasedBuilder<Power<TDim, TMeasure>, TAlias>();
}
