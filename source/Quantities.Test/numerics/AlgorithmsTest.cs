// ToDo: Capitalise Test subfolders!
namespace Quantities.Test.Numerics;

using Quantities.Core.Numerics;


public class AlgorithmsTest
{
    [Fact]
    public void GcdFindsTheGreatestCommonDivisor()
    {
        Int64 expected = (Int64)(Math.Pow(3, 6) * Math.Pow(13, 4));

        Int64 actual = Algorithms.Gcd(expected * 11 * 23, expected * 7 * 17 * 29);

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
