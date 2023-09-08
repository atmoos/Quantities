using Quantities.Measures;

namespace Quantities.Factories;

internal interface IInject<TCreate>
where TCreate : struct, ICreate
{
    Quantity Inject<TMeasure>(in TCreate create) where TMeasure : IMeasure;
}
