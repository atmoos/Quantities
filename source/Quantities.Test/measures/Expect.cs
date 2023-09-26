using Quantities.Core.Numerics;

namespace Quantities.Test.Measures;

internal static class Expect<TResult>
    where TResult : IMeasure
{
    private static readonly Measure expected = Measure.Of<TResult>();
    public static Polynomial IsProductOf<TLeft, TRight>()
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        var left = Measure.Of<TLeft>();
        var right = Measure.Of<TRight>();

        var actual = left.Multiply(right);

        Assert.Same(expected, (Measure)actual);
        return (Polynomial)actual;
    }
    public static Polynomial IsQuotientOf<TNumerator, TDenominator>()
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
