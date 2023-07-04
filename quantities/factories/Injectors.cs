using Quantities.Measures;

namespace Quantities.Factories;

internal sealed class LinearInject : INewInject
{
    public Quant Inject<TCreate, TMeasure, TAlias>(in TCreate create)
        where TCreate : struct, ICreate
        where TMeasure : IMeasure
        where TAlias : IInjector, new() => create.Create<TMeasure, TAlias>();

    public Quant Inject<TCreate, TMeasure>(in TCreate create)
        where TCreate : struct, ICreate
        where TMeasure : IMeasure => create.Create<TMeasure>();
}

internal sealed class Injector<TDim> : INewInject
    where TDim : IDimension
{
    public Quant Inject<TCreate, TMeasure, TAlias>(in TCreate create)
        where TCreate : struct, ICreate
        where TMeasure : IMeasure
        where TAlias : IInjector, new() => create.Create<Power<TDim, TMeasure>, TAlias>();

    public Quant Inject<TCreate, TMeasure>(in TCreate create)
        where TCreate : struct, ICreate
        where TMeasure : IMeasure => create.Create<Power<TDim, TMeasure>, Linear<TMeasure>>();
}