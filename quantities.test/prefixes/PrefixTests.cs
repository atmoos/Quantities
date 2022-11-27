namespace Quantities.Test.Prefixes;

internal sealed class PrefixTests
{
    private readonly Int32 precision;
    public PrefixTests(Int32 precision) => this.precision = precision;

    public void ToSiScalesAsExpected(ITestPrefix prefix)
    {
        Double scalingFactor = prefix.ToSi(1d);
        Double normalisedFactor = scalingFactor / prefix.Factor;

        PrecisionIsBounded(1d, normalisedFactor, this.precision);
    }

    public void FromSiScalesAsExpected(ITestPrefix prefix)
    {
        Double scalingFactor = prefix.FromSi(1d);
        Double normalisedFactor = scalingFactor * prefix.Factor;

        PrecisionIsBounded(1d, normalisedFactor, this.precision);
    }

    public void ToSiRoundRobinEquality(ITestPrefix prefix)
    {
        const Double expected = 2d;

        Double siValue = prefix.ToSi(expected);
        Double actual = prefix.FromSi(siValue);

        PrecisionIsBounded(expected, actual, this.precision);
    }

    public void FromSiRoundRobinEquality(ITestPrefix prefix)
    {
        const Double expected = 2d;

        Double siValue = prefix.FromSi(expected);
        Double actual = prefix.ToSi(siValue);

        PrecisionIsBounded(expected, actual, this.precision);
    }
}
