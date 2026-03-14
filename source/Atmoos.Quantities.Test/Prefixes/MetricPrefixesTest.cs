namespace Atmoos.Quantities.Test.Prefixes;

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

    public static TheoryData<ITestPrefix> AllMetricPrefixes() => [
            // Excluding prefixes that cannot round exactly.
            // They are covered with the "outlier" tests.
            new TestPrefix<Ronna>(1e27),
            new TestPrefix<Yotta>(1e24),
            new TestPrefix<Exa>(1e18),
            new TestPrefix<Peta>(1e15),
            new TestPrefix<Tera>(1e12),
            new TestPrefix<Giga>(1e9),
            new TestPrefix<Mega>(1e6),
            new TestPrefix<Kilo>(1e3),
            new TestPrefix<Hecto>(1e2),
            new TestPrefix<Deca>(1e1),
            new TestPrefix<Deci>(1e-1),
            new TestPrefix<Centi>(1e-2),
            new TestPrefix<Milli>(1e-3),
            new TestPrefix<Micro>(1e-6),
            new TestPrefix<Nano>(1e-9),
            new TestPrefix<Pico>(1e-12),
            new TestPrefix<Femto>(1e-15),
            new TestPrefix<Atto>(1e-18),
            new TestPrefix<Ronto>(1e-27)
        ];
}
