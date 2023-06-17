using Quantities.Measures;

namespace Quantities.Serialization;

internal interface IBuild
{
    Quant Build(in Double value);
}
internal interface IInject
{
    IBuilder Inject<TMeasure>() where TMeasure : IMeasure;
}
internal interface IBuilder : IBuild
{
    IBuilder Append(IInject inject);
}
