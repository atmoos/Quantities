using Quantities.Measures;

namespace Quantities.Serialization;

internal interface IBuild
{
    Quant Build(in Double value);
}
internal interface IInject
{
    IBuilder Inject<TMeasure>() where TMeasure : IMeasure;
    IBuilder Inject<TMeasure, TAlias>() where TMeasure : IMeasure where TAlias : IInjector, new();
}
internal interface IBuilder : IBuild
{
    IBuilder Append(IInject inject);
}
