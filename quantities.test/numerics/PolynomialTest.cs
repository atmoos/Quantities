using Quantities.Numerics;

namespace Quantities.Test.Numerics;

public class PolynomialTest
{
    private static readonly Int32 precision = VeryLowPrecision;
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
    internal void SelfTimesSelfIsSelfAppliedTwice(Polynomial self)
    {
        var argument = rand.Uniform();
        var duplicate = self * self;
        var expected = self * (self * argument);

        var actual = duplicate * argument;

        Assert.Equal(expected, actual, precision);
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
