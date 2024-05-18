using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Test;

public class TransformationTest
{
    private const Double defaultInput = Math.Tau / Math.E;
    private static Transformation Value => new();

    [Fact]
    public void ModeratelyComplexFunction_EvaluatesCorrectly()
    {
        Double value = 3;
        Double expected = (6.262 * value / 2 - 3) / 2 + 1;

        Polynomial poly = Polynomial.Of((6.262 * new Transformation() / 2 - 3) / 2 + 1);
        Double actual = poly * value;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FusedMultiplyAddIsEquivalentToScaledAndAdd()
    {
        const Double argument = 3.538213;
        const Double scale = 8.1231;
        const Double addend = -2.9232;
        Polynomial fused = Polynomial.Of(Value.FusedMultiplyAdd(scale, addend));
        Polynomial compound = Polynomial.Of(scale * Value + addend);

        Double expected = compound * argument;
        Double actual = fused * argument;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(Computations))]
    public void TransformationComputesCorrectResult(Transformation transformation, Func<Double, Double> function)
    {
        var input = defaultInput;
        var expected = function(input);
        var polynomial = Polynomial.Of(transformation);

        var actual = polynomial * input;

        // The polynomial may be more precise that pure double calc
        // Hence medium precision does not mean it's bad...
        Assert.Equal(expected, actual, MediumPrecision);
    }



    [Theory]
    [MemberData(nameof(Transformations))]
    public void TransformationsBuiltInverse(Transformation transformation)
    {
        var input = defaultInput;
        var polynomial = Polynomial.Of(transformation);

        var forwardResult = polynomial * input;
        var actual = polynomial / forwardResult;

        Assert.Equal(input, actual, LowPrecision);
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
