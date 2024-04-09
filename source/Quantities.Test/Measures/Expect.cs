using Quantities.Core.Numerics;

namespace Quantities.Test.Measures;

internal static class Expect<TResult>
    where TResult : IMeasure
{
    private static readonly Measure expected = Measure.Of<TResult>();

    public static Polynomial ToBeInverseOf<TMeasure>(String expectedRepresentation)
        where TMeasure : IMeasure
    {
        var measure = Measure.Of<TMeasure>();

        var result = measure.Invert();
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
}
