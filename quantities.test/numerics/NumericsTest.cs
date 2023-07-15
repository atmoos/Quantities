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
        Double expected = (6.262 * value / 2 - 3) / 2 + 1;

        Polynomial poly = ((6.262 * new Transformation() / 2 - 3) / 2 + 1).Build();
        Double actual = poly.Evaluate(value);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(Computations))]
    public void TransformationComputeCorrectResult(Transformation transformation, Func<Double, Double> function)
    {
        var input = defaultInput;
        var expected = function(input);
        var polynomial = transformation.Build();

        var actual = polynomial.Evaluate(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(Transformations))]
    public void TransformationInverse(Transformation transformation)
    {
        var input = defaultInput;
        var forward = transformation.Build();
        var inverse = transformation.Invert().Build();

        var forwardResult = forward.Evaluate(input);
        var actual = inverse.Evaluate(forwardResult);

        Assert.Equal(input, actual, 14);
    }

    [Theory]
    [MemberData(nameof(Transformations))]
    public void TransformationsBuiltInverse(Transformation transformation)
    {
        var input = defaultInput;
        var polynomial = transformation.Build();

        var forwardResult = polynomial.Evaluate(input);
        var actual = polynomial.Inverse(forwardResult);

        Assert.Equal(input, actual, 14);
    }

    public static IEnumerable<Object[]> Transformations()
    {
        return Computations().Select(o => o[..1]);
    }
    public static IEnumerable<Object[]> Computations()
    {
        static IEnumerable<(Transformation transform, Func<Double, Double> function)> FunctionsOfInterest()
        {
            yield return (Value, value => value);
            yield return (Value - 3, value => value - 3);
            yield return (3.4 * Value, value => 3.4 * value);
            yield return (Value / 9.28, value => value / 9.28);
            yield return (5.21 * Value / 12.34, value => 5.21 * value / 12.34);
            yield return (2 * Value / 3 + 3.42, value => 2 * value / 3 + 3.42);
            yield return ((9.23 * (0.12 * Value - 2) + 32) / 8, value => (9.23 * (0.12 * value - 2) + 32) / 8);
        }
        return FunctionsOfInterest().Select(f => new Object[] { f.transform, f.function });
    }
}
