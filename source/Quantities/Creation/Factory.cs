using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Units;

namespace Quantities.Creation;

internal abstract class Factory
{
    public IInject<Factory> Product { get; }
    public IInject<Factory> Quotient { get; }
    private Factory(IInject<Factory> product, IInject<Factory> quotient) => (Product, Quotient) = (product, quotient);
    public abstract Measure Create();
    public abstract Measure Square();
    public abstract Measure Cubic();
    public abstract TResult Inject<TResult>(IInject<TResult> inject);
    public abstract Measure PowerOf<TUnit, TLinear>()
        where TUnit : IPowerOf<TLinear>, IDimension
        where TLinear : IDimension, ILinear;
    public abstract Measure InverseOf<TUnit, TLinear>()
        where TUnit : IInvertible<TLinear>, IDimension
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
        public override Measure PowerOf<TUnit, TLinear>() => InjectionOf<TUnit, TLinear, AliasInjectionFactory<TLinear>>.Measure;
        public override Measure InverseOf<TUnit, TLinear>() => InjectionOf<TUnit, TLinear, InvertibleInjectionFactory<TLinear>>.Measure;
        public override TResult Inject<TResult>(IInject<TResult> inject) => inject.Inject<TMeasure>();

        private sealed class AliasInjectionFactory<TLinear> : ISystems<TLinear, Measure>
            where TLinear : IDimension
        {
            public Measure Si<TUnit>()
                where TUnit : ISiUnit, TLinear
                    => Measure.Of<Alias<TMeasure, Si<TUnit>>>();
            public Measure Metric<TUnit>()
                where TUnit : IMetricUnit, TLinear
                    => Measure.Of<Alias<TMeasure, Metric<TUnit>>>();
            public Measure Imperial<TUnit>()
                where TUnit : IImperialUnit, TLinear
                    => Measure.Of<Alias<TMeasure, Imperial<TUnit>>>();
            public Measure NonStandard<TUnit>()
                where TUnit : INonStandardUnit, TLinear
                    => Measure.Of<Alias<TMeasure, NonStandard<TUnit>>>();
        }

        private sealed class InvertibleInjectionFactory<TLinear> : ISystems<TLinear, Measure>
            where TLinear : IDimension
        {
            public Measure Si<TUnit>()
                where TUnit : ISiUnit, TLinear
                    => Measure.Of<Invertible<TMeasure, Si<TUnit>>>();
            public Measure Metric<TUnit>()
                where TUnit : IMetricUnit, TLinear
                    => Measure.Of<Invertible<TMeasure, Metric<TUnit>>>();
            public Measure Imperial<TUnit>()
                where TUnit : IImperialUnit, TLinear
                    => Measure.Of<Invertible<TMeasure, Imperial<TUnit>>>();
            public Measure NonStandard<TUnit>()
                where TUnit : INonStandardUnit, TLinear
                    => Measure.Of<Invertible<TMeasure, NonStandard<TUnit>>>();
        }
    }
}

file static class InjectionOf<TUnit, TLinear, TFactory>
    where TUnit : ISystemInject<TLinear>, IDimension
    where TLinear : IDimension
    where TFactory : ISystems<TLinear, Measure>, new()
{
    public static Measure Measure { get; } = TUnit.Inject(new TFactory());
}

file sealed class ProductInject<TLeftTerm> : IInject<Factory>
    where TLeftTerm : IMeasure
{
    public Factory Inject<TMeasure>() where TMeasure : IMeasure => Factory.Of<Measures.Product<TLeftTerm, TMeasure>>();
}

file sealed class QuotientInject<TLeftTerm> : IInject<Factory>
    where TLeftTerm : IMeasure
{
    public Factory Inject<TMeasure>() where TMeasure : IMeasure => Factory.Of<Measures.Quotient<TLeftTerm, TMeasure>>();
}
