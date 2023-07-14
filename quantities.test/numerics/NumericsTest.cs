using Quantities.Numerics;

namespace Quantities.Test.Numerics;

public class TransformationsTest
{
    private const Double defaultInput = Math.Tau / Math.E;
    private static Transformation Value => new();

    [Fact]
    public void ModeratelyComplexFunction_EvaluatesCorrectly()
    {
        Double value = 3;
        var func = new Transformation();
        Double expected = (6.262 * value / 2 - 3) / 2 + 1;

        Polynomial poly = ((6.262 * func / 2 - 3) / 2 + 1).Build();
        Double actual = poly.Evaluate(value);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(Computations))]
    public void TransformationComputeCorrectResult(Transformation transformation, Double expectedResult)
    {
        var polynomial = transformation.Build();

        var actualResult = polynomial.Evaluate(defaultInput);

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [MemberData(nameof(Transformations))]
    public void TransformationsInverse(Transformation transformation)
    {
        var input = defaultInput;
        var polynomial = transformation.Build();

        var forwardResult = polynomial.Evaluate(input);
        var actual = polynomial.Inverse(forwardResult);

        Assert.Equal(input, actual, 14);
    }

    public static IEnumerable<Object[]> Transformations()
    {
        return Computations().Select(o => new Object[] { o[0] });
    }
    public static IEnumerable<Object[]> Computations()
    {
        static IEnumerable<(Transformation transform, Double result)> FunctionsOfInterest()
        {
            yield return (Value, defaultInput);
            yield return (Value - 3, defaultInput - 3);
            yield return (3.4 * Value, 3.4 * defaultInput);
            yield return (Value / 9.28, defaultInput / 9.28);
            yield return (5.21 * Value / 12.34, 5.21 * defaultInput / 12.34);
            yield return (2 * Value / 3 + 3.42, 2 * defaultInput / 3 + 3.42);
            yield return ((9.23 * (0.12 * Value - 2) + 32) / 8, (9.23 * (0.12 * defaultInput - 2) + 32) / 8);
        }
        return FunctionsOfInterest().Select(f => new Object[] { f.transform, f.result });
    }
}
