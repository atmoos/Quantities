using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;

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

        var actual = measure.Inverse;

        Assert.Same(expected, actual);
        Assert.Equal(expectedRepresentation, actual.ToString());
        return actual;
    }

    public static Polynomial ToBeProductOf<TLeft, TRight>()
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        var left = Measure.Of<TLeft>();
        var right = Measure.Of<TRight>();

        var actual = left * right;

        Assert.Same(expected, (Measure)actual);
        return (Polynomial)actual;
    }

    public static Polynomial ToBeQuotientOf<TNumerator, TDenominator>()
        where TNumerator : IMeasure
        where TDenominator : IMeasure
    {
        var numerator = Measure.Of<TNumerator>();
        var denominator = Measure.Of<TDenominator>();

        var actual = numerator / denominator;

        Assert.Same(expected, (Measure)actual);
        return (Polynomial)actual;
    }

    public static Polynomial ToBeEqualTo<TMeasure>()
        where TMeasure : IMeasure
    {
        var measure = Measure.Of<TMeasure>();

        var ratio = measure / expected;
        var poly = (Polynomial)ratio;

        var (n, d, offset) = poly.Simplify();

        Assert.Equal(1d, n, MediumPrecision);
        Assert.Equal(1d, d, MediumPrecision);
        Assert.Equal(0, offset, MediumPrecision);
        return poly;
    }
}


[Ai(Model = "GPT", Version = "5.4")]
internal static class ExpectRepresentation
{
    public static String Of<TMeasure>(Int32 exponent)
        where TMeasure : IMeasure
    {
        RepresentationVisitor visitor = new();

        TMeasure.Power(visitor, exponent);

        return visitor.Result;
    }

    private sealed class RepresentationVisitor : IVisitor
    {
        public String Result { get; private set; } = String.Empty;

        public Result? Build(Polynomial poly, Dimension target)
        {
            throw new NotSupportedException("Build is not needed for these tests.");
        }

        public IVisitor Inject<TMeasure>()
            where TMeasure : IMeasure
        {
            this.Result = TMeasure.Representation;
            return this;
        }
    }
}
