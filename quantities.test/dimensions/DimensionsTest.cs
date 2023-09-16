using Quantities.Dimensions;
using static Quantities.Dimensions.Rank;

namespace Quantities.Test.Dimensions;

public class DimensionsTest
{
    [Fact]
    public void RankOfSelfIsLinear()
    {
        var rank = Rank<Time>.Of<Time>();
        Assert.Equal(One, rank);
    }
    [Fact]
    public void RankOfLinearDimensionIsLinear()
    {
        var rank = Rank<Time>.Of<OtherTime>();
        Assert.Equal(One, rank);
    }
    [Fact]
    public void RankOfOtherDimensionIsNone()
    {
        var rank = Rank<Length>.Of<Time>();
        Assert.Equal(None, rank);
    }
    [Fact]
    public void RankOfSquareSelfIsLinear()
    {
        var rank = Rank<Area>.Of<OtherArea>();
        Assert.Equal(One, rank);
    }
    [Fact]
    public void RankOfLinearOnSquareIsSquare()
    {
        var rank = Rank<Area>.Of<Length>();
        Assert.Equal(Two, rank);
    }
    [Fact]
    public void RankOfOtherLinearOnSquareIsNone()
    {
        var rank = Rank<Area>.Of<Time>();
        Assert.Equal(None, rank);
    }
    [Fact]
    public void RankOfOtherSquareOnSquareIsNone()
    {
        var rank = Rank<Area>.Of<SquareTemperature>();
        Assert.Equal(None, rank);
    }
    [Fact]
    public void RankOfCubeSelfIsLinear()
    {
        var rank = Rank<Volume>.Of<OtherVolume>();
        Assert.Equal(One, rank);
    }
    [Fact]
    public void RankOfLinearOnCubeIsCubic()
    {
        var rank = Rank<Volume>.Of<Length>();
        Assert.Equal(Three, rank);
    }
    [Fact]
    public void RankOfOtherLinearOnCubicIsNone()
    {
        var rank = Rank<Volume>.Of<Time>();
        Assert.Equal(None, rank);
    }
    [Fact]
    public void RankOfOtherCubicOnCubicIsNone()
    {
        var rank = Rank<Volume>.Of<CubicMass>();
        Assert.Equal(None, rank);
    }
    [Fact]
    public void RankOfLinearNominatorOnQuotientIsHigherOrder()
    {
        var rank = Rank<Velocity>.Of<Length>();
        Assert.Equal(HigherOrder, rank);
    }
    [Fact]
    public void RankOfLinearDenominatorOnQuotientIsHigherOrder()
    {
        var rank = Rank<Velocity>.Of<Time>();
        Assert.Equal(HigherOrder, rank);
    }
    [Fact]
    public void RankOfOtherOnQuotientIsNone()
    {
        var rank = Rank<Velocity>.Of<Area>();
        Assert.Equal(None, rank);
    }
    [Fact]
    public void RankOfSimilarQuotientOnQuotientIsLinear()
    {
        var rank = Rank<Velocity>.Of<OtherVelocity>();
        Assert.Equal(One, rank);
    }
    [Fact]
    public void RankOfConstituentOnDegenerateQuotientIsZero()
    {
        var rank = Rank<Angle>.Of<Length>();
        Assert.Equal(Zero, rank);
    }
    [Fact]
    public void RankOfLeftArgumentOnProductIsHigherOrder()
    {
        var rank = Rank<Coulomb>.Of<Time>();
        Assert.Equal(HigherOrder, rank);
    }
    [Fact]
    public void RankOfRightArgumentOnProductIsHigherOrder()
    {
        var rank = Rank<Coulomb>.Of<Ampere>();
        Assert.Equal(HigherOrder, rank);
    }
    [Fact]
    public void RankOfOtherOnProductIsNone()
    {
        var rank = Rank<Coulomb>.Of<Length>();
        Assert.Equal(None, rank);
    }
    [Fact]
    public void RankOfSimilarProductOnProductIsLinear()
    {
        var rank = Rank<Coulomb>.Of<AmpereHour>();
        Assert.Equal(One, rank);
    }
    [Fact]
    public void RankOfConstituentOnSquaringProductIsSquare()
    {
        var rank = Rank<DoubleTime>.Of<Time>();
        Assert.Equal(Two, rank);
    }

    private static class Rank<TSelf>
        where TSelf : IDimension
    {
        public static Rank Of<TOther>()
            where TOther : IDimension => TSelf.RankOf<TOther>();
    }
}
