using Quantities.Measures;

namespace Quantities.Factories;

internal sealed class PowerInjector<TCreate, TDim> : IInject<TCreate>
    where TCreate : struct, ICreate
    where TDim : IExponent
{
    public Quantity Inject<TMeasure>(in TCreate create)
        where TMeasure : IMeasure => create.Create<Power<TDim, TMeasure>>();
}

internal sealed class QuotientInjector<TCreate, TNominator> : IInject<TCreate>
    where TCreate : struct, ICreate
    where TNominator : IMeasure
{
    public Quantity Inject<TMeasure>(in TCreate create)
        where TMeasure : IMeasure => create.Create<Quotient<TNominator, TMeasure>>();
}

internal sealed class ProductInjector<TCreate, TLeftTerm> : IInject<TCreate>
    where TCreate : struct, ICreate
    where TLeftTerm : IMeasure
{
    public Quantity Inject<TMeasure>(in TCreate create)
        where TMeasure : IMeasure => create.Create<Product<TLeftTerm, TMeasure>>();
}
