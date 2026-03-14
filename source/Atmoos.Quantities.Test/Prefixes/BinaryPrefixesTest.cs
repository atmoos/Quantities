namespace Atmoos.Quantities.Test.Prefixes;

public class BinaryPrefixesTest
{
    [Theory]
    [MemberData(nameof(AllBinaryPrefixes))]
    public void ToSiScalesAsExpected(ITestPrefix prefix) => PrefixTests.ToSiScalesAsExpected(prefix, FullPrecision);

    [Theory]
    [MemberData(nameof(AllBinaryPrefixes))]
    public void FromSiScalesAsExpected(ITestPrefix prefix) => PrefixTests.FromSiScalesAsExpected(prefix, FullPrecision);

    [Theory]
    [MemberData(nameof(AllBinaryPrefixes))]
    public void ToSiRoundRobinEquality(ITestPrefix prefix) => PrefixTests.ToSiRoundRobinEquality(prefix, FullPrecision);

    [Theory]
    [MemberData(nameof(AllBinaryPrefixes))]
    public void FromSiRoundRobinEquality(ITestPrefix prefix) => PrefixTests.FromSiRoundRobinEquality(prefix, FullPrecision);

    public static TheoryData<ITestPrefix> AllBinaryPrefixes() => [
            new TestPrefix<Kibi>(1024d),
            new TestPrefix<Mebi>(1024d * 1024d),
            new TestPrefix<Gibi>(1073741824d),
            new TestPrefix<Tebi>(1099511627776d),
            new TestPrefix<Pebi>(1024d * 1099511627776d),
            new TestPrefix<Exbi>(1073741824d * 1073741824d),
            new TestPrefix<Zebi>(1073741824d * 1099511627776d),
            new TestPrefix<Yobi>(1099511627776d * 1099511627776d)
        ];
}
