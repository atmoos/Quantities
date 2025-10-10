namespace Atmoos.Quantities.Test.Numerics;

using Atmoos.Quantities.Core.Numerics;
using static System.Math;


public class AlgorithmsTest
{
    [Fact]
    public void GcdFindsTheGreatestCommonDivisor()
    {
        Int64 expected = (Int64)(Math.Pow(3, 6) * Math.Pow(13, 4));

        Int64 actual = Algorithms.Gcd(expected * 11 * 23, expected * 7 * 17 * 29);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SimplifyMovesTheSignToTheNominator()
    {
        var expected = (n: -1d, d: 1d);

        var actual = Algorithms.Simplify(1, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SimplifyWhenDenominatorIsAnInteger()
    {
        const Int64 gcd = 1156;
        Int64 integerN = gcd * 2 * 7 * 17 * 47;
        Int64 denominator = gcd * 5 * 23 * 53;
        var nominator = integerN + (2d / Math.E);
        var expected = (n: nominator / gcd, d: denominator / gcd);

        var actual = Algorithms.Simplify(nominator, denominator);

        Assert.Equal(expected.n / expected.d, actual.nominator / actual.denominator, 15);
    }

    [Fact]
    public void SimplifyIsAbleToSimplifyIntegerRatiosOfSmallMagnitudes()
    {
        var nominator = 1.6e-3;
        var denominator = 1.4e-2;
        var expected = (4d, 35d);

        var actual = Algorithms.Simplify(nominator, denominator);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SimplifySelectsNonIntegerTermForSimplification()
    {
        var integer = 4;
        var nonInteger = 7.2;
        var expected = (9d, 5d);

        var left = Algorithms.Simplify(integer, nonInteger);
        var right = Algorithms.Simplify(nonInteger, integer);

        Assert.Equal(expected, right);
        Assert.Equal(left, (right.denominator, right.nominator));
    }

    [Fact]
    public void SimplifyIsAbleToSimplifyIntegerRatiosOfMixedMagnitudes()
    {
        Int32 gcd = 13 * 13 * 17;
        Int32 n = 2 * 7;
        Int32 d = 3 * 11;
        var nominator = gcd * n * 1e-6;
        var denominator = gcd * d * 1e-1;

        var actual = Algorithms.Simplify(nominator, denominator);

        Assert.Equal(nominator / denominator, actual.nominator / actual.denominator);
    }

    [Fact]
    public void SimplifyPreservesRatios()
    {
        Int64 integerN = 2 * 2 * 2 * 7 * 17 * 17 * 47;
        Double denominator = 2 * 2 * 5 * 13 * 17 * 23 * 51;
        var nominator = integerN + 2d / Math.E;
        Double expected = 1.467114016654674e-1; // computed with arbitrary precision

        var actual = Algorithms.Simplify(nominator, denominator);

        Assert.Equal(expected, actual.nominator / actual.denominator, 15);
        Assert.NotEqual(nominator, actual.nominator); // Sanity check!
    }

    [Fact]
    public void SimplifyRemovesMultiplicitiesCorrectly()
    {
        var nominator = Pow(2, 3) * Pow(3, 3) * Pow(5, 2) * Pow(7, 5) * Pow(11, 2) * 13;
        var denominator = Pow(2, 5) * Pow(3, 1) * Pow(5, 3) * Pow(7, 2) * Pow(11, 4) * 17;
        var expectedNominator = Pow(3, 2) * Pow(7, 3) * 13;
        var expectedDenominator = Pow(2, 2) * Pow(5, 1) * Pow(11, 2) * 17;
        var expected = (expectedNominator, expectedDenominator);

        var actual = Algorithms.Simplify(nominator, denominator);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(13)]
    [InlineData(23)]
    public void PowSatisfiesTheDefinitionForPositiveExponents(Int32 exponent)
    {
        const Double value = 7 / 4d;
        const Double multiplicativeIdentity = 1d;
        var terms = Enumerable.Repeat(value, exponent);
        var expected = terms.Aggregate(multiplicativeIdentity, (pow, term) => term * pow);
        var sanity = Math.Pow(value, exponent);

        var actual = Algorithms.Pow(value, exponent);

        Assert.Equal(sanity, actual);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(13)]
    [InlineData(23)]
    public void PowSatisfiesTheIdentityCondition(Int32 exponent)
    {
        const Int32 precision = 15;
        const Double value = 9 / 8d;
        const Double multiplicativeIdentity = 1d;

        var posExponent = Algorithms.Pow(value, exponent);
        var negExponent = Algorithms.Pow(value, -exponent);

        var actual = negExponent * posExponent;
        var sanity = Math.Pow(value, -exponent) * Math.Pow(value, exponent);

        Assert.Equal(sanity, actual);
        Assert.Equal(multiplicativeIdentity, actual, precision);
    }
}
