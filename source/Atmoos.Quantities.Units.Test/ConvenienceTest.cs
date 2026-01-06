using Xunit.Sdk;

namespace Atmoos.Quantities.Units.Test;

public class ConvenienceTest
{
    private const Double expectedValue = 1.2345678944444123;
    private const Double actualOffBy16 = 1.2345678944444124;
    private const Double actualOffBy15 = 1.2345678944444133;

    [Fact]
    public void PrecisionIsBoundedDoesntThrowOnEqualValues()
    {
        // This doesn't throw
        PrecisionIsBounded(expectedValue, expectedValue, 16);
    }

    [Fact]
    public void PrecisionIsBoundedOfOnSixteenthDecimal()
    {
        // This doesn't throw
        PrecisionIsBounded(expectedValue, actualOffBy16, 15);

        // This does!
        Assert.Throws<EqualException>(() => PrecisionIsBounded(expectedValue, actualOffBy16, 16));
    }

    [Fact]
    public void PrecisionIsBoundedOfOnFifteenthDecimal()
    {
        // This doesn't throw
        PrecisionIsBounded(expectedValue, actualOffBy15, 14);

        // This does!
        Assert.Throws<EqualException>(() => PrecisionIsBounded(expectedValue, actualOffBy15, 15));
    }
}
