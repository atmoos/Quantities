using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities.Creation;

internal abstract class Factory
{
    public abstract ref readonly Measure Create();
    public abstract ref readonly Factory Multiply(Factory other);
    protected abstract ref readonly Factory Multiply<TMeasure>() where TMeasure : IMeasure;
    public abstract ref readonly Factory Divide(Factory other);
    protected abstract ref readonly Factory Divide<TMeasure>() where TMeasure : IMeasure;
    public abstract ref readonly Factory Power<TExponent>() where TExponent : INumber;
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
        public override ref readonly Measure Create() => ref Measure.Of<TMeasure>();
        public override ref readonly Factory Multiply(Factory other) => ref other.Multiply<TMeasure>();
        protected override ref readonly Factory Multiply<TMeasure1>() => ref Of<Measures.Product<TMeasure1, TMeasure>>();
        public override ref readonly Factory Divide(Factory other) => ref other.Divide<TMeasure>();
        protected override ref readonly Factory Divide<TMeasure1>() => ref Of<Measures.Product<TMeasure1, Inverse<TMeasure>>>();
        public override ref readonly Factory Power<TExponent>() => ref Of<Measures.Power<TExponent, TMeasure>>();
        public override ref readonly Measure AliasOf<TUnit, TLinear>() => ref InjectionOf<TUnit, TLinear, AliasInjectionFactory<TLinear>>.Measure;
        public override ref readonly Measure InverseOf<TUnit, TLinear>() => ref InjectionOf<TUnit, TLinear, InvertibleInjectionFactory<TLinear>>.Measure;

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
