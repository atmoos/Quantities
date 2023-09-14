using Quantities.Numerics;

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
