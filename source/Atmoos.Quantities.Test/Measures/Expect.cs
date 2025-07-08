using Atmoos.Quantities.Core.Numerics;
using static Atmoos.Quantities.Test.Convenience;

namespace Atmoos.Quantities.Test.Measures;

file static class Identity
{
    public static Measure Measure = Measure.Of<Quantities.Measures.Identity>();
}

internal static class Expect<TResult>
    where TResult : IMeasure
{
    private static readonly Measure expected = Measure.Of<TResult>();

    public static Polynomial ToBeInverseOf<TMeasure>(String expectedRepresentation)
        where TMeasure : IMeasure
    {
        var measure = Measure.Of<TMeasure>();

        var result = measure.Inverse;
        var actual = (Measure)result;

        Assert.Same(expected, actual);
        Assert.Equal(expectedRepresentation, actual.ToString());
        return (Polynomial)result;
    }

    public static Polynomial ToBeProductOf<TLeft, TRight>()
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        var left = Measure.Of<TLeft>();
        var right = Measure.Of<TRight>();

        var actual = left.Multiply(right);

        Assert.Same(expected, (Measure)actual);
        return (Polynomial)actual;
    }
    public static Polynomial ToBeQuotientOf<TNumerator, TDenominator>()
        where TNumerator : IMeasure
        where TDenominator : IMeasure
    {
        var numerator = Measure.Of<TNumerator>();
        var denominator = Measure.Of<TDenominator>();

        var actual = numerator.Divide(denominator);

        Assert.Same(expected, (Measure)actual);
        return (Polynomial)actual;
    }

    public static Polynomial ToBeEqualTo<TMeasure>()
    where TMeasure : IMeasure
    {
        var measure = Measure.Of<TMeasure>();

        var ratio = measure.Divide(expected);
        var poly = (Polynomial)ratio;

        var (n, d, offset) = poly.Simplify();

        Assert.Equal(1d, n, MediumPrecision);
        Assert.Equal(1d, d, MediumPrecision);
        Assert.Equal(0, offset, MediumPrecision);
        return poly;
    }
}
