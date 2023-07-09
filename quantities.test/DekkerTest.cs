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
    public void NoCancellation()
    {
        var lowPrecision = 6d;
        var highPrecision = new Dekker(lowPrecision);

        CatastrophicCancellation(ref highPrecision);
        CatastrophicCancellation(ref lowPrecision);

        Assert.NotEqual(highPrecision.Value, lowPrecision);
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
