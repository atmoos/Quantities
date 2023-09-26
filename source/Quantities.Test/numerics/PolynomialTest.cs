using Quantities.Core.Numerics;

using static System.Math;

namespace Quantities.Test.Numerics;

public class PolynomialTest
{
    private static readonly Int32 precision = VeryLowPrecision;
    private static readonly Polynomial some = Poly(1, 2, 3);
    private static readonly Random rand = new();
    private static Transformation Value => new();

    [Theory]
    [MemberData(nameof(Polynomials))]
    internal void SelfDividedBySelfIsTheIdentityPolynomial(Polynomial self)
    {
        var expected = rand.Uniform();
        var identity = self / self;

        var actual = identity * expected;

        Assert.Equal(expected, actual, precision);
    }

    [Theory]
    [MemberData(nameof(Polynomials))]
    internal void CompositionOfPolynomialsIsTheSameAsApplyingPolynomialsSuccessively(Polynomial self)
    {
        var argument = rand.Uniform();
        var composition = some * self;
        var expected = some * (self * argument);

        var actual = composition * argument;

        Assert.Equal(expected, actual, precision);
    }

    [Fact]
    internal void PolynomialOfEqualFractionCompareEqual()
    {
        var n = Math.PI;
        var d = Math.Tau;
        var left = Poly(-n, d);
        var right = Poly(n, -d);
        Assert.Equal(left, right);
    }

    [Fact]
    internal void PolynomialWithNegativeNominatorAndDenominatorCompareEqual()
    {
        var n = Math.E;
        var d = Math.PI;
        var left = Poly(n, d);
        var right = Poly(-n, -d);
        Assert.Equal(left, right);
    }

    [Fact]
    internal void PolynomialWithUnequalNominatorCompareNonEqual()
    {
        var n = Math.E;
        var left = Poly(nominator: n);
        var right = Poly(nominator: Double.BitIncrement(n));
        Assert.NotEqual(left, right);
    }

    [Fact]
    internal void PolynomialWithUnequalDenominatorCompareNonEqual()
    {
        var d = Math.PI;
        var left = Poly(denominator: d);
        var right = Poly(denominator: Double.BitDecrement(d));
        Assert.NotEqual(left, right);
    }

    [Fact]
    internal void PolynomialWithUnequalOffsetCompareNonEqual()
    {
        var o = Math.Tau;
        var left = Poly(offset: o);
        var right = Poly(offset: Double.BitIncrement(o));
        Assert.NotEqual(left, right);
    }

    [Theory]
    [InlineData(4, 5, 0)]
    [InlineData(2, -3, 0)]
    [InlineData(-2, -3, 0)]
    [InlineData(1, 2, 3)]
    [InlineData(3, 2, -5)]
    [InlineData(5, -2, -9)]
    internal void PolynomialsCompareEqualBasedOnValue(Double n, Double d, Double o)
    {
        var left = Poly(n, d, o);
        var right = Poly(n, d, o);
        Assert.Equal(left, right);
    }

    [Fact]
    public void PolynomialsSimplifyCorrectly()
    {
        var nominator = Pow(2, 3) * Pow(3, 3) * Pow(5, 2) * Pow(7, 5) * Pow(11, 2) * 13;
        var denominator = Pow(2, 5) * Pow(3, 1) * Pow(5, 3) * Pow(7, 2) * Pow(11, 4) * 17;
        var expectedNominator = Pow(3, 2) * Pow(7, 3) * 13;
        var expectedDenominator = Pow(2, 2) * Pow(5, 1) * Pow(11, 2) * 17;
        var poly = Poly(nominator, denominator); // this prevents prior simplification
        var expected = Poly(expectedNominator, expectedDenominator);

        var actual = poly.Simplify();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PowerIsMaximallySimplifiedForZeroOffsetPolyNomials()
    {
        const Int32 exp = 5;
        var nominator = Pow(2, 3) * Pow(3, 4) * 5;
        var denominator = Pow(2, 5) * Pow(3, 1) * 7;
        var expectedN = Pow(Pow(3, 3) * 5, exp);
        var expectedD = Pow(Pow(2, 2) * 7, exp);
        var expected = Poly(expectedN, expectedD);
        var poly = Poly(nominator, denominator);

        var actual = poly.Pow(exp);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PowerIsMaximallySimplifiedForPolynomialsWithOffset()
    {
        const Int32 exp = 9;
        const Int32 offset = 7;
        var nominator = Pow(2, 4) * Pow(3, 2) * 7;
        var denominator = Pow(2, 3) * Pow(3, 3) * 5;
        var expectedNominator = Pow(Pow(2, 1) * 7, exp);
        var expectedDenominator = Pow(Pow(3, 1) * 5, exp);
        var poly = Poly(nominator, denominator, offset);

        var actual = poly.Pow(exp);

        var (actualNominator, actualDenominator, actualOffset) = actual;

        Assert.Equal(expectedNominator, actualNominator);
        Assert.Equal(expectedDenominator, actualDenominator);
        (actualOffset > offset).IsTrue(); // account for Pow(offset,exp) > offset
        (actualOffset < Pow(offset, exp)).IsTrue(); // account for the fact that (n/s)= (1008/1080) < 1!
    }

    [Fact]
    public void PowerOffsetIsBoundedForUpscalingPolynomials()
    {
        const Int32 exp = 9;
        const Int32 offset = 7;
        var nominator = 3 * 7d;
        var denominator = 17d;
        var scalingFactor = nominator / denominator;
        var poly = Poly(nominator, denominator, offset);

        var actual = poly.Pow(exp);

        var (_, _, actualOffset) = actual;

        // The offset is additive. Hence power adds the offset "exp" times, i.e. offset * exp
        var additiveComponent = offset * exp;
        var lowerBound = scalingFactor * additiveComponent;
        var upperBound = Pow(scalingFactor, exp) * additiveComponent;
        (actualOffset > lowerBound).IsTrue();
        (actualOffset < upperBound).IsTrue();
    }

    [Fact]
    public void PowerOffsetIsBoundedForDownscalingPolynomials()
    {
        const Int32 exp = 7;
        const Int32 offset = 8;
        var nominator = 19d;
        var denominator = 3 * 7d;
        var scalingFactor = nominator / denominator;
        var poly = Poly(nominator, denominator, offset);

        var actual = poly.Pow(exp);

        var (_, _, actualOffset) = actual;

        var additiveComponent = offset * exp; // i.e. offset applied 'exp' times.
        var upperBound = scalingFactor * additiveComponent;
        var lowerBound = Pow(scalingFactor, exp) * additiveComponent;
        (actualOffset > lowerBound).IsTrue();
        (actualOffset < upperBound).IsTrue();
    }
    [Fact]
    public void PowerTimesInversePowerIsIdentityForPolynomialsWithoutOffset()
    {
        const Int32 exp = 7;
        var nominator = 13d;
        var denominator = 2 * 7d;
        var poly = Poly(nominator, denominator);

        var power = poly.Pow(exp);
        var inversePower = poly.Pow(-exp);

        var actual = power * inversePower;

        var (n, d, o) = actual;

        Assert.Equal(1d, n);
        Assert.Equal(1d, d);
        Assert.Equal(0, o);
    }
    [Fact]
    public void PowerTimesInversePowerIsIdentityForPolynomialsWithOffset()
    {
        const Int32 exp = 7;
        const Int32 offset = 8;
        var nominator = 13d;
        var denominator = 2 * 7d;
        var poly = Poly(nominator, denominator, offset);

        var power = poly.Pow(exp);
        var inversePower = poly.Pow(-exp);

        var actual = power * inversePower;

        var (n, d, o) = actual;

        Assert.Equal(1d, n);
        Assert.Equal(1d, d);
        PrecisionIsBounded(0, o, 14);
    }

    [Theory]
    [InlineData(0, 1, 0, "0")]
    [InlineData(0, 1, 2, "2")]
    [InlineData(1, 1, 0, "x")]
    [InlineData(1, -1, 0, "-x")]
    [InlineData(1, 3, 0, "x/3")]
    [InlineData(4, 1, 0, "4*x")]
    [InlineData(4, -1, 0, "-4*x")]
    [InlineData(1, -7, 0, "-x/7")]
    [InlineData(4, 5, 0, "4*x/5")]
    [InlineData(2, -3, 0, "-2*x/3")]
    [InlineData(-2, -3, 0, "2*x/3")]
    [InlineData(1, 2, 3, "x/2 + 3")]
    [InlineData(3, 2, -5, "3*x/2 - 5")]
    [InlineData(5, -2, -9, "-5*x/2 - 9")]
    [InlineData(5, 0, Double.MinValue, "∞")]
    [InlineData(-5, 0, Double.MaxValue, "-∞")]
    [InlineData(0, 0, Double.MaxValue, "∞")]
    internal void ToStringRespectsCommonShorthands(Double n, Double d, Double o, String expected)
    {
        var poly = Poly(n, d, o);
        var actual = poly.ToString();

        Assert.Equal($"p(x) = {expected}", actual);
    }

    [Fact]
    internal void ToStringCreatesSimplifiedRepresentation()
    {
        var poly = Poly(21, -14, 5);
        var expected = "p(x) = -3*x/2 + 5";
        var actual = poly.ToString();

        Assert.Equal(expected, actual);
    }

    public static IEnumerable<Object[]> Polynomials()
    {
        static IEnumerable<Transformation> FunctionsOfInterest()
        {
            yield return Value;
            yield return Value - 3;
            yield return 3.4 * Value;
            yield return Value / 9.28;
            yield return 5.21 * Value / 12.34;
            yield return 2 * Value / 3 + 3.42;
            yield return (9.23 * (0.12 * Value - 2) + 32) / 8;
        }
        return FunctionsOfInterest().Select(f => new Object[] { Polynomial.Of(f) });
    }
}
