using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Measures;

public class InversionTest
{
    private const String zeroWidthNonJoiner = "\u200C";

    [Fact]
    public void SiInvertedIsInverseOfSi()
    {
        Expect<Power<Si<Kilo, Metre>, Negative<One>>>.ToBeInverseOf<Si<Kilo, Metre>>("km⁻¹");
    }

    [Fact]
    public void InverseOfInvertedSiIsSi()
    {
        Expect<Si<Metre>>.ToBeInverseOf<Power<Si<Metre>, Negative<One>>>("m");
    }

    [Fact]
    public void MetricInvertedIsInverseOfMetric()
    {
        Expect<Power<Metric<Milli, Hour>, Negative<One>>>.ToBeInverseOf<Metric<Milli, Hour>>($"m{Hour.Representation}⁻¹");
    }

    [Fact]
    public void ImperialInvertedIsInverseOfImperial()
    {
        Expect<Power<Imperial<Foot>, Negative<One>>>.ToBeInverseOf<Imperial<Foot>>("ft⁻¹");
    }

    [Fact]
    public void NonStandardInvertedIsInverseOfNonStandard()
    {
        Expect<Power<NonStandard<SomeNonStandardUnit>, Negative<One>>>.ToBeInverseOf<NonStandard<SomeNonStandardUnit>>("snu⁻¹");
    }

    [Fact]
    public void ProductInvertedIsProductOfInverses()
    {
        Expect<Product<Power<Si<Metre>, Negative<One>>, Power<Metric<Hour>, Negative<One>>>>.ToBeInverseOf<Product<Si<Metre>, Metric<Hour>>>($"m⁻¹{zeroWidthNonJoiner}h⁻¹");
    }

    [Fact]
    public void QuotientInvertedIsInvertedQuotient()
    {
        Expect<Product<Metric<Hour>, Power<Si<Metre>, Negative<One>>>>.ToBeInverseOf<Product<Si<Metre>, Power<Metric<Hour>, Negative<One>>>>("h/m");
    }

    [Theory]
    [InlineData(2, "m⁻²")]
    [InlineData(-2, "s⁻²")]
    [Ai(Model = "GPT", Version = "5.4")]
    public void InvertiblePowerCoversBothExponentSignBranches(Int32 exponent, String expected)
    {
        String actual = ExpectRepresentation.Of<Invertible<Si<Metre>, Si<Second>>>(exponent);

        Assert.Equal(expected, actual);
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void InverseOfNegativePowerTwoReturnsPositivePowerMeasure()
    {
        Expect<Power<Si<Metre>, Two>>.ToBeInverseOf<Power<Si<Metre>, Negative<Two>>>("m²");
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void InvertCoversZeroZeroBranch()
    {
        AssertInvert<Identity, Identity, Identity>();
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void InvertCoversNegativePositiveBranch()
    {
        AssertInvert<Si<Metre>, Power<Si<Second>, Negative<One>>, Product<Si<Second>, Power<Si<Metre>, Negative<One>>>>();
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void InvertCoversZeroOtherBranch()
    {
        AssertInvert<Identity, Si<Second>, Power<Si<Second>, Negative<One>>>();
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void InvertCoversOtherZeroBranch()
    {
        AssertInvert<Si<Metre>, Identity, Power<Si<Metre>, Negative<One>>>();
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void InvertCoversDefaultProductBranch()
    {
        AssertInvert<Power<Si<Metre>, Negative<One>>, Si<Second>, Product<Si<Metre>, Power<Si<Second>, Negative<One>>>>();
    }

    private static void AssertInvert<TLeft, TRight, TExpected>()
        where TLeft : IMeasure
        where TRight : IMeasure
        where TExpected : IMeasure
    {
        Measure actual = Arithmetic<TLeft>.Invert<Measure, TRight>(new MeasureInjector());

        Assert.Same(Measure.Of<TExpected>(), actual);
    }

    private sealed class MeasureInjector : IInject<Measure>
    {
        public Measure Inject<TMeasure>()
            where TMeasure : IMeasure => Measure.Of<TMeasure>();
    }
}

file readonly struct SomeNonStandardUnit : INonStandardUnit, IVelocity
{
    public static Transformation ToSi(Transformation self) => self * 1.234;

    public static String Representation => "snu";
}
