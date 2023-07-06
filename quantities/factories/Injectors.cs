using Quantities.Measures;

namespace Quantities.Factories;

internal sealed class Injector<TCreate, TDim> : IInject<TCreate>
        where TCreate : struct, ICreate
    where TDim : IDimension
{
    public Quant Inject<TMeasure, TAlias>(in TCreate create)
        where TMeasure : IMeasure
        where TAlias : IInjector, new() => create.Create<Power<TDim, TMeasure>, TAlias>();

    public Quant Inject<TMeasure>(in TCreate create)
        where TMeasure : IMeasure => create.Create<Power<TDim, TMeasure>, Linear<TMeasure>>();
}