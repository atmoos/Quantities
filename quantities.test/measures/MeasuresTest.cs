using Quantities.Measures;
using Quantities.Numerics;
using Quantities.Units.NonStandard.Length;
using Quantities.Units.Si.Metric;

namespace Quantities.Test.Measures;

public class MeasuresTest
{
    private static readonly AssertionInjector loweringInjector = new();
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
        AssertIdentityLoweringOf<Si<Second>>();
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
        AssertIdentityLoweringOf<Metric<Hour>>();
    }
    [Fact]
    public void LowerImperial()
    {
        AssertIdentityLoweringOf<Imperial<Mile>>();
    }
    [Fact]
    public void LowerNonStandard()
    {
        AssertIdentityLoweringOf<NonStandard<LightYear>>();
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

    private static void AssertLoweringOf<TFrom, TExpected>(Double from, Double expected)
        where TFrom : IMeasure
        where TExpected : IMeasure
    {
        var (actual, lowering) = TFrom.Lower(loweringInjector, from);
        lowering.Is<TExpected>();
        Assert.Equal(expected, actual);
    }

    private static void AssertIdentityLoweringOf<TIdentity>()
        where TIdentity : IMeasure => AssertLoweringOf<TIdentity, TIdentity>(Math.PI, Math.PI);

    private abstract class LoweringAssertion
    {
        public abstract void Is<TExpected>() where TExpected : IMeasure;
        public static LoweringAssertion Of<TActual>() where TActual : IMeasure => AllocationFree<Lowering<TActual>>.Item;
        private sealed class Lowering<TActual> : LoweringAssertion
            where TActual : IMeasure
        {
            public override void Is<TExpectedMeasure>()
            {
                Assert.IsType<Lowering<TExpectedMeasure>>(this);
            }
        }
    }
    private sealed class AssertionInjector : IInject<LoweringAssertion>
    {
        public LoweringAssertion Inject<TMeasure>(in Double value) where TMeasure : IMeasure => LoweringAssertion.Of<TMeasure>();
    }
}
