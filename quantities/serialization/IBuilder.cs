using Quantities.Measures;

namespace Quantities.Serialization;

internal interface IBuilder
{
    Quantity Build(in Double value);
}
internal interface IInject
{
    IBuilder Inject<TMeasure>() where TMeasure : IMeasure;
    IBuilder Inject<TMeasure, TAlias>() where TMeasure : IMeasure where TAlias : IInjector, new();
}
