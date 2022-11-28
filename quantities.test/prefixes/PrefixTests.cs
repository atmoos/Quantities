namespace Quantities.Test.Prefixes;

internal static class PrefixTests
{
    public static void ToSiScalesAsExpected(ITestPrefix prefix, Int32 precision)
    {
        Double scalingFactor = prefix.ToSi(1d);
        Double normalisedFactor = scalingFactor / prefix.Factor;

        PrecisionIsBounded(1d, normalisedFactor, precision);
    }

    public static void FromSiScalesAsExpected(ITestPrefix prefix, Int32 precision)
    {
        Double scalingFactor = prefix.FromSi(1d);
        Double normalisedFactor = scalingFactor * prefix.Factor;

        PrecisionIsBounded(1d, normalisedFactor, precision);
    }

    public static void ToSiRoundRobinEquality(ITestPrefix prefix, Int32 precision)
    {
        const Double expected = 2d;

        Double siValue = prefix.ToSi(expected);
        Double actual = prefix.FromSi(siValue);

        PrecisionIsBounded(expected, actual, precision);
    }

    public static void FromSiRoundRobinEquality(ITestPrefix prefix, Int32 precision)
    {
        const Double expected = 2d;

        Double siValue = prefix.FromSi(expected);
        Double actual = prefix.ToSi(siValue);

        PrecisionIsBounded(expected, actual, precision);
    }
}
