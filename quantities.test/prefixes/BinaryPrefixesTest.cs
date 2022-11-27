namespace Quantities.Test.Prefixes;

public class BinaryPrefixesTest
{
    private static readonly PrefixTests tests = new(precision: 16);
    [Theory]
    [MemberData(nameof(AllBinaryPrefixes))]
    public void ToSiScalesAsExpected(ITestPrefix prefix) => tests.ToSiScalesAsExpected(prefix);

    [Theory]
    [MemberData(nameof(AllBinaryPrefixes))]
    public void FromSiScalesAsExpected(ITestPrefix prefix) => tests.FromSiScalesAsExpected(prefix);

    [Theory]
    [MemberData(nameof(AllBinaryPrefixes))]
    public void ToSiRoundRobinEquality(ITestPrefix prefix) => tests.ToSiRoundRobinEquality(prefix);

    [Theory]
    [MemberData(nameof(AllBinaryPrefixes))]
    public void FromSiRoundRobinEquality(ITestPrefix prefix) => tests.FromSiRoundRobinEquality(prefix);
    public static IEnumerable<Object[]> AllBinaryPrefixes()
    {
        return BinaryPrefixes().Select(p => new Object[] { p });
    }
    public static IEnumerable<ITestPrefix> BinaryPrefixes()
    {
        const Double kibi = 1024;
        const Double gibi = 1073741824;
        const Double tebi = 1099511627776;
        yield return new TestPrefix<Kibi>(kibi);
        yield return new TestPrefix<Mebi>(kibi * kibi);
        yield return new TestPrefix<Gibi>(gibi);
        yield return new TestPrefix<Tebi>(tebi);
        yield return new TestPrefix<Pebi>(kibi * tebi);
        yield return new TestPrefix<Exbi>(gibi * gibi);
        yield return new TestPrefix<Zebi>(gibi * tebi);
        yield return new TestPrefix<Yobi>(tebi * tebi);
    }
}
