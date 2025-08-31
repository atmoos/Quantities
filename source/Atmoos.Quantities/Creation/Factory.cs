using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities.Creation;

internal abstract class Factory
{
    public IInject<Factory> Product { get; }
    public IInject<Factory> Quotient { get; }
    private Factory(IInject<Factory> product, IInject<Factory> quotient) => (Product, Quotient) = (product, quotient);
    public abstract ref readonly Measure Create();
    public abstract ref readonly Measure Create<TExponent>() where TExponent : INumber;
    public abstract TResult Inject<TResult>(IInject<TResult> inject);
    public abstract ref readonly Measure AliasOf<TUnit, TLinear>()
        where TUnit : IDimension, ISystemInject<TLinear>
        where TLinear : IDimension, ILinear;
    public abstract ref readonly Measure InverseOf<TUnit, TLinear>()
        where TUnit : IInvertible<TLinear>, IDimension
        where TLinear : IDimension, ILinear;
    public static ref readonly Factory Of<TMeasure>() where TMeasure : IMeasure => ref AllocationFree<Factory, Impl<TMeasure>>.Item;

    private sealed class Impl<TMeasure> : Factory
        where TMeasure : IMeasure
    {
        private static readonly IInject<Factory> product = new ProductInject<TMeasure>();
        private static readonly IInject<Factory> quotient = new QuotientInject<TMeasure>();
        public Impl() : base(product, quotient) { }
        public override ref readonly Measure Create() => ref Measure.Of<TMeasure>();
        public override ref readonly Measure Create<TExponent>() => ref Measure.Of<Measures.Power<TExponent, TMeasure>>();
        public override ref readonly Measure AliasOf<TUnit, TLinear>() => ref InjectionOf<TUnit, TLinear, AliasInjectionFactory<TLinear>>.Measure;
        public override ref readonly Measure InverseOf<TUnit, TLinear>() => ref InjectionOf<TUnit, TLinear, InvertibleInjectionFactory<TLinear>>.Measure;
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
    private static readonly Measure measure = TUnit.Inject(new TFactory());
    public static ref readonly Measure Measure => ref measure;
}

file sealed class ProductInject<TLeftTerm> : IInject<Factory>
    where TLeftTerm : IMeasure
{
    public Factory Inject<TMeasure>() where TMeasure : IMeasure => Factory.Of<Measures.Product<TLeftTerm, TMeasure>>();
}

file sealed class QuotientInject<TLeftTerm> : IInject<Factory>
    where TLeftTerm : IMeasure
{
    public Factory Inject<TMeasure>() where TMeasure : IMeasure => Factory.Of<Measures.Product<TLeftTerm, Inverse<TMeasure>>>();
}
