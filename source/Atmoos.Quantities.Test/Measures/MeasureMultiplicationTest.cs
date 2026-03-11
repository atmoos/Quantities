using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Measures;

public class MeasureMultiplicationTest
{
    private const String zeroWidthNonJoiner = "\u200C";

    [Fact]
    public void IdentityTimesAnyIsAny()
    {
        var conversion = Expect<Si<Micro, Second>>.ToBeProductOf<Identity, Si<Micro, Second>>();
        Assert.Equal(Polynomial.One, conversion);
    }

    [Fact]
    public void AnyTimesIdentityIsAny()
    {
        var conversion = Expect<Si<Pico, Second>>.ToBeProductOf<Si<Pico, Second>, Identity>();
        Assert.Equal(Polynomial.One, conversion);
    }

    [Fact]
    public void ScalarTimesOtherScalarIsProduct()
    {
        var conversion = Expect<Product<Si<Metre>, Si<Second>>>.ToBeProductOf<Si<Metre>, Si<Second>>();
        Assert.Equal(Polynomial.One, conversion);
    }

    [Fact]
    public void ScalarTimesSimilarScalarIsSquareScalar()
    {
        var conversion = Expect<Power<Si<Metre>, Two>>.ToBeProductOf<Si<Metre>, Imperial<Foot>>();
        Assert.Equal(Polynomial.Of<Foot>(), conversion);
    }

    [Fact]
    public void ScalarTimesSquareSimilarScalarIsCubicScalar()
    {
        var conversion = Expect<Power<Metric<Hour>, Three>>.ToBeProductOf<Metric<Hour>, Power<Metric<Minute>, Two>>();
        Assert.Equal(Polynomial.Of<Minute>().Pow(-2).Simplify(), conversion);
    }

    [Fact]
    public void ProductOfMeasureTimesItsInverseIsIdentity()
    {
        var conversion = Expect<Identity>.ToBeProductOf<Metric<Hour>, Si<InverseTime>>();
        Assert.Equal(Polynomial.Of<Hour>().Simplify(), conversion);
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void MapReturnsIdentityForUnitTarget()
    {
        Result actual = Assert.IsType<Result>(Arithmetic<Si<Metre>>.Map<Si<Second>>(Polynomial.One, Unit.Identity));
        Measure measure = actual;

        Assert.Same(Measure.Of<Identity>(), measure);
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void MapReturnsMatchingLinearTargetWithoutFallback()
    {
        Result actual = Assert.IsType<Result>(Arithmetic<Si<Metre>>.Map<Identity>(Polynomial.One, Scalar.Of<ILength>()));
        Measure measure = actual;

        Assert.Same(Measure.Of<Si<Metre>>(), measure);
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void MapFallbackCanBuildByDirectFactoryMatch()
    {
        Result actual = Assert.IsType<Result>(Arithmetic<Invertible<Product<Si<Metre>, Si<Second>>, Identity>>.Map<Identity>(Polynomial.One, Invertible<Product<Si<Metre>, Si<Second>>, Identity>.D));
        Measure measure = actual;

        Assert.NotNull(measure);
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void MapFallbackCanBuildByChainingLeftAndRightFactories()
    {
        Dimension target = Invertible<Product<Si<Metre>, Si<Second>>, Identity>.D * Invertible<Product<Si<Ampere>, Si<Kelvin>>, Identity>.D;
        Result actual = Assert.IsType<Result>(Arithmetic<Invertible<Product<Si<Metre>, Si<Second>>, Identity>>.Map<Invertible<Product<Si<Ampere>, Si<Kelvin>>, Identity>>(Polynomial.One, target));
        Measure measure = actual;

        Assert.NotNull(measure);
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void MapReturnsNullWhenNoFallbackPathMatches()
    {
        Result? actual = Arithmetic<Si<Metre>>.Map<Si<Second>>(Polynomial.One, Scalar.Of<IMass>());

        Assert.Null(actual);
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void ProductRepresentationUsesJoinerBranchWhenNoDivisionIsNeeded()
    {
        Assert.Equal($"m{zeroWidthNonJoiner}s", Product<Si<Metre>, Si<Second>>.Representation);
    }

    [Theory]
    [InlineData(1, "h")]
    [InlineData(2, "h²")]
    [Ai(Model = "GPT", Version = "5.4")]
    public void AliasPowerCoversAliasEqualityAndRaisedLinearFallback(Int32 exponent, String expected)
    {
        String actual = ExpectRepresentation.Of<Alias<Metric<Hour>, Metric<Hour>>>(exponent);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(3, "m³")]
    [InlineData(0, "𝟙")]
    [InlineData(-2, "m⁻²")]
    [Ai(Model = "GPT", Version = "5.4")]
    public void RaiseCoversPositiveZeroAndNegativeBranches(Int32 exponent, String expected)
    {
        String actual = ExpectRepresentation.Of<Si<Metre>>(exponent);

        Assert.Equal(expected, actual);
    }

    [Fact]
    [Ai(Model = "GPT", Version = "5.4")]
    public void RaiseThrowsForUnsupportedExponent()
    {
        Assert.Throws<InvalidOperationException>(() => ExpectRepresentation.Of<Si<Metre>>(6));
    }
}

file readonly struct InverseTime : ISiUnit, IFrequency, IInvertible<ITime>
{
    public static Transformation ToSi(Transformation self) => self;

    static T ISystemInject<ITime>.Inject<T>(ISystems<ITime, T> basis) => basis.Si<Second>();

    public static String Representation => "s⁻¹";
}
