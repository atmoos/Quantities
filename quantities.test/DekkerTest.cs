namespace Quantities.Test;

public class DekkerTest
{
    [Fact]
    public void SimpleAdditionWorks()
    {
        var value = new Dekker(3);

        value.Add(5);

        Assert.Equal(8, value.Value);
    }

    [Fact]
    public void SimpleMultiplicationWorks()
    {
        var value = new Dekker(3);

        value.Multiply(5);

        Assert.Equal(15, value.Value);
    }

    [Fact]
    public void ComplexMultiplicationWorks()
    {
        const Double left = Math.E;
        const Double centre = Math.Tau * Math.E;
        const Double right = Math.PI;
        const Double expected = left * centre * right;
        var result = new Dekker(left);

        result.Multiply(centre);
        result.Multiply(right);

        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void NoCancellation()
    {
        var lowPrecision = 6d;
        var highPrecision = new Dekker(lowPrecision);

        CatastrophicCancellation(ref highPrecision);
        CatastrophicCancellation(ref lowPrecision);

        Assert.NotEqual(highPrecision.Value, lowPrecision);
    }

    [Fact]
    public void IllConditionedSum()
    {
        var random = new Random(3);
        var lowPrecision = 391826d;
        var highPrecision = new Dekker(lowPrecision);
        foreach (var summand in IllConditioned(1024)) {
            highPrecision.Add(in summand);
            lowPrecision += summand;
            var mult = 1.5d - 0.7 * random.NextDouble();
            highPrecision.Multiply(mult);
            lowPrecision *= mult;
        }

        Assert.NotEqual(highPrecision.Value, lowPrecision);
    }

    private static IEnumerable<Double> IllConditioned(Int32 count)
    {
        for (Int32 exp = 0; exp < count; ++exp) {
            yield return Math.E * Math.Pow(2, -exp);
        }
    }

    private static void CatastrophicCancellation(ref Dekker value)
    {
        const Double kilo = 1000;
        const Double metres = 1852;
        const Double seconds = 3600;

        // knots to m/s
        value.Multiply(metres);
        value.Divide(seconds);

        // si to Km/h
        value.Divide(kilo);
        value.Multiply(seconds);
    }
    private static void CatastrophicCancellation(ref Double value)
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
    }

}
