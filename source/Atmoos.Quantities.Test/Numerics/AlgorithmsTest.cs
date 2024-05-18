namespace Atmoos.Quantities.Test.Numerics;

using Atmoos.Quantities.Core.Numerics;


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
    public void SimplifyOnlySimplifiesWhenDenominatorIsAnInteger()
    {
        const Int64 gcd = 1156;
        Int64 integerN = gcd * 2 * 7 * 17 * 47;
        Int64 denominator = gcd * 5 * 23 * 53;
        var nominator = integerN + (2d / Math.E);
        var nonIntegerDenominator = denominator + 0.2;
        var expected = (n: nominator / gcd, d: denominator / gcd);

        var actual = Algorithms.Simplify(nominator, denominator);
        var (unchangedNominator, _) = Algorithms.Simplify(nominator, nonIntegerDenominator);

        Assert.Equal(expected, actual);
        Assert.Equal(nominator, unchangedNominator);
    }

    [Fact]
    public void SimplifyPreservesRatios()
    {
        Int64 integerN = 2 * 2 * 2 * 7 * 17 * 17 * 47;
        Double denominator = 2 * 2 * 5 * 13 * 17 * 23 * 51;
        var nominator = integerN + 2d / Math.E;

        var actual = Algorithms.Simplify(nominator, denominator);

        Assert.Equal(nominator / denominator, actual.nominator / actual.denominator);
        Assert.NotEqual(nominator, actual.nominator); // Sanity check!
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
