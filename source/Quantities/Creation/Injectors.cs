namespace Quantities.Creation;

internal static class Injectors
{
    public static IInject<Measure> Square { get; } = new SquareInject();
    public static IInject<IInject<Measure>> Product { get; } = new ProductInject();
    public static IInject<IInject<Measure>> Quotient { get; } = new QuotientInject();

    private sealed class SquareInject : IInject<Measure>
    {
        public Measure Inject<TMeasure>() where TMeasure : IMeasure => Measure.Of<Measures.Power<Measures.Square, TMeasure>>();
    }

    private sealed class ProductInject : IInject<IInject<Measure>>
    {
        public IInject<Measure> Inject<TMeasure>() where TMeasure : IMeasure => AllocationFree<Product<TMeasure>>.Item;
        private sealed class Product<TLeftTerm> : IInject<Measure>
            where TLeftTerm : IMeasure
        {
            public Measure Inject<TMeasure>() where TMeasure : IMeasure => Measure.Of<Measures.Product<TLeftTerm, TMeasure>>();
        }
    }

    private sealed class QuotientInject : IInject<IInject<Measure>>
    {
        public IInject<Measure> Inject<TMeasure>() where TMeasure : IMeasure => AllocationFree<Quotient<TMeasure>>.Item;
        private sealed class Quotient<TLeftTerm> : IInject<Measure>
            where TLeftTerm : IMeasure
        {
            public Measure Inject<TMeasure>() where TMeasure : IMeasure => Measure.Of<Measures.Quotient<TLeftTerm, TMeasure>>();
        }
    }
}
