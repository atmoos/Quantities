using Quantities.Measures;

namespace Quantities.Serialization;

internal interface IBuilder
{
    Quant Build(in Double value);
}
internal interface IInject
{
    IBuilder Inject<TMeasure>() where TMeasure : IMeasure;
    IBuilder Inject<TMeasure, TAlias>() where TMeasure : IMeasure where TAlias : IInjector, new();
}
