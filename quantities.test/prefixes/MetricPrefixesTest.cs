using Xunit.Sdk;

namespace Quantities.Test.Prefixes;

public class MetricPrefixesTest
{
    private const Int32 fullPrecision = 16;
    private const Int32 mediumPrecision = fullPrecision - 1;
    private static readonly Dictionary<String, ITestPrefix> outliers = new() {
        ["quetta"] = new TestPrefix<Quetta>(1e30),
        ["zetta"] = new TestPrefix<Zetta>(1e21),
        ["zepto"] = new TestPrefix<Zepto>(1e-21),
        ["yocto"] = new TestPrefix<Yocto>(1e-24),
        ["quecto"] = new TestPrefix<Quecto>(1e-30)
    };

    [Theory]
    [MemberData(nameof(AllMetricPrefixes))]
    public void ToSiScalesAsExpected(ITestPrefix prefix) => PrefixTests.ToSiScalesAsExpected(prefix, FullPrecision);

    [Theory]
    [InlineData("quetta", fullPrecision)]
    [InlineData("zetta", fullPrecision)]
    [InlineData("zepto", fullPrecision)]
    [InlineData("yocto", mediumPrecision)]
    [InlineData("quecto", mediumPrecision)]
    public void ToSiScalesAsExpected_VariablePrecision(String prefix, Int32 precision) => PrefixTests.ToSiScalesAsExpected(outliers[prefix], precision);

    [Theory]
    [MemberData(nameof(AllMetricPrefixes))]
    public void FromSiScalesAsExpected(ITestPrefix prefix) => PrefixTests.FromSiScalesAsExpected(prefix, FullPrecision);

    [Theory]
    [InlineData("quetta", mediumPrecision)]
    [InlineData("zetta", mediumPrecision)]
    [InlineData("zepto", mediumPrecision)]
    [InlineData("yocto", mediumPrecision)]
    [InlineData("quecto", fullPrecision)]
    public void FromSiScalesAsExpected_VariablePrecision(String prefix, Int32 precision) => PrefixTests.FromSiScalesAsExpected(outliers[prefix], precision);

    [Theory]
    [MemberData(nameof(AllMetricPrefixes))]
    public void ToSiRoundRobinEquality(ITestPrefix prefix) => PrefixTests.ToSiRoundRobinEquality(prefix, FullPrecision);

    [Theory]
    [InlineData("quetta", fullPrecision)]
    [InlineData("zetta", fullPrecision)]
    [InlineData("zepto", mediumPrecision)]
    [InlineData("yocto", fullPrecision)]
    [InlineData("quecto", mediumPrecision)]
    public void ToSiRoundRobinEquality_VariablePrecision(String prefix, Int32 precision) => PrefixTests.ToSiRoundRobinEquality(outliers[prefix], precision);

    [Theory]
    [MemberData(nameof(AllMetricPrefixes))]
    public void FromSiRoundRobinEquality(ITestPrefix prefix) => PrefixTests.FromSiRoundRobinEquality(prefix, FullPrecision);

    [Theory]
    [InlineData("quetta", mediumPrecision)]
    [InlineData("zetta", mediumPrecision)]
    [InlineData("zepto", fullPrecision)]
    [InlineData("yocto", fullPrecision)]
    [InlineData("quecto", fullPrecision)]
    public void FromSiRoundRobinEquality_VariablePrecision(String prefix, Int32 precision) => PrefixTests.FromSiRoundRobinEquality(outliers[prefix], precision);

    public static IEnumerable<Object[]> AllMetricPrefixes() => MetricPrefixes().Select(p => new Object[] { p });
    public static IEnumerable<ITestPrefix> MetricPrefixes()
    {
        // Excluding prefixes that cannot round exactly.
        // They are covered with the "outlier" tests.
        yield return new TestPrefix<Ronna>(1e27);
        yield return new TestPrefix<Yotta>(1e24);
        yield return new TestPrefix<Exa>(1e18);
        yield return new TestPrefix<Peta>(1e15);
        yield return new TestPrefix<Tera>(1e12);
        yield return new TestPrefix<Giga>(1e9);
        yield return new TestPrefix<Mega>(1e6);
        yield return new TestPrefix<Kilo>(1e3);
        yield return new TestPrefix<Hecto>(1e2);
        yield return new TestPrefix<Deca>(1e1);
        yield return new TestPrefix<Deci>(1e-1);
        yield return new TestPrefix<Centi>(1e-2);
        yield return new TestPrefix<Milli>(1e-3);
        yield return new TestPrefix<Micro>(1e-6);
        yield return new TestPrefix<Nano>(1e-9);
        yield return new TestPrefix<Pico>(1e-12);
        yield return new TestPrefix<Femto>(1e-15);
        yield return new TestPrefix<Atto>(1e-18);
        yield return new TestPrefix<Ronto>(1e-27);
    }
}
