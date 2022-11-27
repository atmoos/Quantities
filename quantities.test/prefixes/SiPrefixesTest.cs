namespace Quantities.Test.Prefixes;

public class SiPrefixesTest
{
    private static readonly PrefixTests tests = new(precision: 15);
    [Theory]
    [MemberData(nameof(AllMetricPrefixes))]
    public void ToSiScalesAsExpected(ITestPrefix prefix) => tests.ToSiScalesAsExpected(prefix);

    [Theory]
    [MemberData(nameof(AllMetricPrefixes))]
    public void FromSiScalesAsExpected(ITestPrefix prefix) => tests.FromSiScalesAsExpected(prefix);

    [Theory]
    [MemberData(nameof(AllMetricPrefixes))]
    public void ToSiRoundRobinEquality(ITestPrefix prefix) => tests.ToSiRoundRobinEquality(prefix);

    [Theory]
    [MemberData(nameof(AllMetricPrefixes))]
    public void FromSiRoundRobinEquality(ITestPrefix prefix) => tests.FromSiRoundRobinEquality(prefix);
    public static IEnumerable<Object[]> AllMetricPrefixes()
    {
        return MetricPrefixes().Select(p => new Object[] { p });
    }
    public static IEnumerable<ITestPrefix> MetricPrefixes()
    {
        yield return new TestPrefix<Quetta>(1e30);
        yield return new TestPrefix<Ronna>(1e27);
        yield return new TestPrefix<Yotta>(1e24);
        yield return new TestPrefix<Zetta>(1e21);
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
        yield return new TestPrefix<Zepto>(1e-21);
        yield return new TestPrefix<Yocto>(1e-24);
        yield return new TestPrefix<Ronto>(1e-27);
        yield return new TestPrefix<Quecto>(1e-30);
    }
}
