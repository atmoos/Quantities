using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Units.NonStandard.Area;
using Quantities.Units.Si.Metric;

namespace Quantities.Test.Measures;

public class MeasureRankTest
{
    [Fact]
    public void ScalarsRanksAreSymmetric()
    {
        var actual = MRank<Si<Metre>>.Equal<Imperial<Foot>, Foot>();
        Assert.Equal(Rank.One, actual);
    }
    [Fact]
    public void PowerRanksAreSymmetricOnLinearDimensions()
    {
        var actual = MRank<Power<Cubic, Si<Metre>>>.Equal<Imperial<Foot>, Foot>();
        Assert.Equal(Rank.Three, actual);
    }
    [Fact]
    public void PowerRanksAreSymmetricOnOwnDimensions()
    {
        var actual = MRank<Power<Cubic, Si<Metre>>>.Equal<Metric<Litre>, Litre>();
        Assert.Equal(Rank.One, actual);
    }
    [Fact]
    public void AliasRanksAreSymmetricOnLinearDimensions()
    {
        var actual = MRank<Alias<Metric<Are>, Si<Metre>>>.Equal<Imperial<Foot>, Foot>();
        Assert.Equal(Rank.Two, actual);
    }
    [Fact]
    public void AliasRanksAreSymmetricOnOwnDimensions()
    {
        var actual = MRank<Alias<Metric<Are>, Si<Metre>>>.Equal<NonStandard<Morgen>, Morgen>();
        Assert.Equal(Rank.One, actual);
    }
}

file static class MRank<TMeasure>
    where TMeasure : IMeasure
{
    private static String name = TMeasure.Representation;
    public static Rank Equal<TM, TD>()
        where TM : IMeasure
        where TD : IDimension
    {
        var expected = TMeasure.Rank<TM>();
        var actual = TMeasure.RankOf<TD>();
        Assert.Equal(expected, actual);
        return expected;
    }
}
