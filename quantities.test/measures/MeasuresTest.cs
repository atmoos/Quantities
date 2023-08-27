using Quantities.Measures;
using Quantities.Numerics;
using Quantities.Units.Imperial.Mass;
using Quantities.Units.NonStandard.Length;
using Quantities.Units.NonStandard.Temperature;
using Quantities.Units.Si.Metric;
using static Quantities.Extensions;

namespace Quantities.Test.Measures;

public class MeasuresTest
{
    private const Int32 fullPrecision = 15;
    private static readonly AssertionInjector assertionInjector = new();
    [Fact]
    public void SiToSi()
    {
        var kilometre = Polynomial.Of<Si<Kilo, Metre>>().Evaluate(1d);

        Assert.Equal(1000d, kilometre);
    }
    [Fact]
    public void SiFromSi()
    {
        var millimetre = Polynomial.Of<Si<Milli, Metre>>().Inverse(1d);

        Assert.Equal(1000d, millimetre);
    }

    [Fact]
    public void LowerSi()
    {
        AssertLoweringOf<Si<Kilo, Metre>, Si<Metre>>(3, 3000);
        AssertLoweringOf<Si<Centi, Metre>, Si<Metre>>(200, 2);
    }
    [Fact]
    public void LowerPureSi()
    {
        AssertLoweringOf<Si<Second>>();
    }
    [Fact]
    public void LowerMetric()
    {
        AssertLoweringOf<Metric<Hecto, Litre>, Metric<Litre>>(3, 300);
        AssertLoweringOf<Metric<Centi, Litre>, Metric<Litre>>(200, 2);
    }
    [Fact]
    public void LowerPureMetric()
    {
        AssertLoweringOf<Metric<Hour>>();
    }
    [Fact]
    public void LowerImperial()
    {
        AssertLoweringOf<Imperial<Mile>>();
    }
    [Fact]
    public void LowerNonStandard()
    {
        AssertLoweringOf<NonStandard<LightYear>>();
    }

    [Fact]
    public void LowerProduct()
    {
        AssertLoweringOf<Product<Si<Kilo, Metre>, Metric<Milli, Litre>>, Product<Si<Metre>, Metric<Litre>>>(4, 4);
        AssertLoweringOf<Product<Si<Deca, Metre>, Metric<Hecto, Litre>>, Product<Si<Metre>, Metric<Litre>>>(4, 4000);
    }

    [Fact]
    public void LowerQuotient()
    {
        AssertLoweringOf<Quotient<Si<Hecto, Metre>, Metric<Hecto, Litre>>, Quotient<Si<Metre>, Metric<Litre>>>(4, 4);
        AssertLoweringOf<Quotient<Si<Deca, Metre>, Metric<Kilo, Litre>>, Quotient<Si<Metre>, Metric<Litre>>>(200, 2);
    }

    [Fact]
    public void LowerPower()
    {
        AssertLoweringOf<Power<Square, Si<Kilo, Metre>>, Power<Square, Si<Metre>>>(3, 3e6);
        AssertLoweringOf<Power<Square, Si<Centi, Metre>>, Power<Square, Si<Metre>>>(1e4, 1);
    }
    [Fact]
    public void NormalizeSi()
    {
        AssertNormalizationOf<Si<Metre>, Si<Mega, Metre>>(4e7, 40);
    }

    [Fact]
    public void NormalizePrefixedSi()
    {
        AssertNormalizationOf<Si<Hecto, Metre>, Si<Metre>>(8e-2, 8);
        AssertNormalizationOf<Si<Kilo, Metre>, Si<Micro, Metre>>(4e-8, 40);
    }

    [Fact]
    public void NormalizeMetric()
    {
        AssertNormalizationOf<Metric<Litre>, Metric<Mega, Litre>>(4e7, 40);
    }

    [Fact]
    public void NormalizePrefixedMetric()
    {
        AssertNormalizationOf<Metric<Hecto, Litre>, Metric<Litre>>(8e-2, 8);
        AssertNormalizationOf<Metric<Kilo, Litre>, Metric<Micro, Litre>>(4e-8, 40);
    }

    [Fact]
    public void NormalizeImperial()
    {
        AssertNormalizationOf<Imperial<Pound>>();
    }

    [Fact]
    public void NormalizeNonStandard()
    {
        AssertNormalizationOf<NonStandard<Réaumur>>();
    }

    [Fact]
    public void NormalizeProduct()
    {
        AssertNormalizationOf<Product<Si<Hecto, Metre>, Si<Milli, Second>>, Product<Si<Kilo, Metre>, Si<Second>>>(8e5, 80);
    }

    [Fact]
    public void NormalizeQuotient()
    {
        AssertNormalizationOf<Quotient<Si<Hecto, Metre>, Si<Micro, Second>>, Quotient<Si<Mega, Metre>, Si<Second>>>(8, 800);
    }

    [Fact]
    public void NormalizePowerToUnitPrefix()
    {
        AssertNormalizationOf<Power<Square, Si<Pico, Second>>, Power<Square, Si<Second>>>(8e24, 8, fullPrecision - 1);
    }
    [Fact]
    public void NormalizePowerToOtherPrefixBase()
    {
        AssertNormalizationOf<Power<Square, Si<Pico, Second>>, Power<Square, Si<Milli, Second>>>(8e19, 80, fullPrecision - 2);
    }

    private static void AssertLoweringOf<TFrom, TExpected>(Double from, Double expected)
        where TFrom : IMeasure
        where TExpected : IMeasure
    {
        var (actual, lowering) = TFrom.Lower(assertionInjector, from);
        lowering.Is<TExpected>();
        Assert.Equal(expected, actual);
    }

    private static void AssertLoweringOf<TIdentity>()
        where TIdentity : IMeasure => AssertLoweringOf<TIdentity, TIdentity>(Math.Tau, Math.Tau);

    private static void AssertNormalizationOf<TFrom, TExpected>(Double from, Double expected, Int32 precision = fullPrecision)
        where TFrom : IMeasure
        where TExpected : IMeasure
    {
        var lowering = TFrom.Normalize(Metric.Scaling, assertionInjector, from);
        lowering.Is<TExpected>();
        Assert.Equal(expected, lowering.Value, precision);
    }
    private static void AssertNormalizationOf<TIdentity>()
        where TIdentity : IMeasure => AssertNormalizationOf<TIdentity, TIdentity>(Math.E, Math.E);

    private abstract class Assertion
    {
        public required Double Value { get; init; }
        public abstract void Is<TExpected>() where TExpected : IMeasure;
        public static Assertion Of<TActual>(Double value) where TActual : IMeasure => new TypeOf<TActual> { Value = value };
        private sealed class TypeOf<TActual> : Assertion
            where TActual : IMeasure
        {
            public override void Is<TExpectedMeasure>()
            {
                Assert.IsType<TypeOf<TExpectedMeasure>>(this);
            }
            public override String ToString() => NameOf<TActual>();
        }
    }

    private sealed class AssertionInjector : IInject<Assertion>
    {
        public Assertion Inject<TMeasure>(in Double value) where TMeasure : IMeasure => Assertion.Of<TMeasure>(value);
    }
}
