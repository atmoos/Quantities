using System.Numerics;
using Quantities.Numerics;

namespace Quantities.Test.Numerics;

public class DekkerTest
{
    [Fact]
    public void SimpleAdditionWorks()
    {
        var value = new Dekker(3);

        var result = value + 5;

        Assert.Equal(8d, result);
    }

    [Fact]
    public void SimpleMultiplicationWorks()
    {
        var value = new Dekker(3);

        var result = value * 5;

        Assert.Equal(15d, result);
    }

    [Fact]
    public void ComplexMultiplicationWorks()
    {
        const Double left = Math.E;
        const Double centre = Math.Tau * Math.E;
        const Double right = Math.PI;
        const Double expected = left * centre * right;
        var value = new Dekker(centre);

        var result = left * value * right;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void NoCancellation()
    {
        var lowPrecision = 6d;
        var highPrecision = new Dekker(lowPrecision);

        var preciseResult = CatastrophicCancellation(highPrecision);
        var impreciseResult = CatastrophicCancellation(lowPrecision);

        Assert.NotEqual(preciseResult, impreciseResult);
    }

    [Fact]
    public void IllConditionedSum()
    {
        var random = new Random(3);
        var lowPrecision = 391826d;
        var highPrecision = new Dekker(lowPrecision);
        foreach (var summand in IllConditioned(1024)) {
            highPrecision += summand;
            lowPrecision += summand;
            var mult = 1.5d - 0.7 * random.NextDouble();
            highPrecision *= mult;
            lowPrecision *= mult;
        }

        Assert.NotEqual(highPrecision, lowPrecision);
    }

    private static IEnumerable<Double> IllConditioned(Int32 count)
    {
        for (Int32 exp = 0; exp < count; ++exp) {
            yield return Math.E * Math.Pow(2, -exp);
        }
    }

    private static T CatastrophicCancellation<T>(T value)
        where T : IMultiplyOperators<T, Double, T>, IDivisionOperators<T, Double, T>
    {
        const Double kilo = 1000;
        const Double metres = 1852;
        const Double seconds = 3600;

        // knots to m/s
        value *= metres;
        value /= seconds;

        // si to Km/h
        value /= kilo;
        value *= seconds;
        return value;
    }
}
