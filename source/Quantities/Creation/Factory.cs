namespace Quantities.Creation;

internal abstract class Factory : IInjector
{
    public IInject<Factory> Product { get; }
    public IInject<Factory> Quotient { get; }
    private Factory(IInject<Factory> product, IInject<Factory> quotient) => (Product, Quotient) = (product, quotient);
    public abstract Measure Create();
    public abstract Measure Square();
    public abstract Measure Cubic();
    public abstract TResult Inject<TResult>(IInject<TResult> inject);
    public static Factory Of<TMeasure>() where TMeasure : IMeasure => AllocationFree<Impl<TMeasure>>.Item;

    private sealed class Impl<TMeasure> : Factory
        where TMeasure : IMeasure
    {
        private static readonly IInject<Factory> product = new ProductInject<TMeasure>();
        private static readonly IInject<Factory> quotient = new QuotientInject<TMeasure>();
        public Impl() : base(product, quotient) { }
        public override Measure Create() => Measure.Of<TMeasure>();
        public override Measure Square() => Measure.Of<Measures.Power<Measures.Square, TMeasure>>();
        public override Measure Cubic() => Measure.Of<Measures.Power<Measures.Cubic, TMeasure>>();
        public override TResult Inject<TResult>(IInject<TResult> inject) => inject.Inject<TMeasure>();
    }

    private sealed class ProductInject<TLeftTerm> : IInject<Factory>
        where TLeftTerm : IMeasure
    {
        public Factory Inject<TMeasure>() where TMeasure : IMeasure => Of<Measures.Product<TLeftTerm, TMeasure>>();
    }

    private sealed class QuotientInject<TLeftTerm> : IInject<Factory>
        where TLeftTerm : IMeasure
    {
        public Factory Inject<TMeasure>() where TMeasure : IMeasure => Of<Measures.Quotient<TLeftTerm, TMeasure>>();
    }
}
