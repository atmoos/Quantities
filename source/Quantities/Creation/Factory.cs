using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Units;

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
    public abstract Measure AliasTo<TUnit, TLinear>()
        where TUnit : IAlias<TLinear>, IDimension
        where TLinear : IDimension, ILinear;
    public static Factory Of<TMeasure>() where TMeasure : IMeasure => AllocationFree<Impl<TMeasure>>.Item;

    private sealed class Impl<TMeasure> : Factory
        where TMeasure : IMeasure
    {
        private static readonly IInject<Factory> product = new ProductInject<TMeasure>();
        private static readonly IInject<Factory> quotient = new QuotientInject<TMeasure>();
        public Impl() : base(product, quotient) { }
        public override Measure Create() => Measure.Of<TMeasure>();
        public override Measure Square() => Measure.Of<Power<Square, TMeasure>>();
        public override Measure Cubic() => Measure.Of<Power<Cubic, TMeasure>>();
        public override Measure AliasTo<TUnit, TLinear>() => AliasFor<TUnit, TLinear>.Measure;
        public override TResult Inject<TResult>(IInject<TResult> inject) => inject.Inject<TMeasure>();

        private static class AliasFor<TUnit, TLinear>
            where TUnit : IAlias<TLinear>, IDimension
            where TLinear : IDimension, ILinear
        {
            public static Measure Measure { get; } = TUnit.Inject(new AliasInjectionFactory<TLinear, TMeasure>());
        }
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

file sealed class AliasInjectionFactory<TLinear, TOuter> : ISystems<TLinear, Measure>
    where TOuter : IMeasure
    where TLinear : IDimension
{
    public Measure Si<TUnit>()
        where TUnit : ISiUnit, TLinear
            => Measure.Of<Alias<TOuter, Si<TUnit>>>();
    public Measure Metric<TUnit>()
        where TUnit : IMetricUnit, TLinear
            => Measure.Of<Alias<TOuter, Metric<TUnit>>>();
    public Measure Imperial<TUnit>()
        where TUnit : IImperialUnit, TLinear
            => Measure.Of<Alias<TOuter, Imperial<TUnit>>>();
    public Measure NonStandard<TUnit>()
        where TUnit : INonStandardUnit, TLinear
            => Measure.Of<Alias<TOuter, NonStandard<TUnit>>>();
}
