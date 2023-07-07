using Quantities.Measures;

namespace Quantities.Factories;

internal interface IInject<TCreate>
where TCreate : struct, ICreate
{
    Quant Inject<TMeasure>(in TCreate create) where TMeasure : IMeasure;
}
