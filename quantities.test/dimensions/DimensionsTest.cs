using Quantities.Dimensions;
using static Quantities.Dimensions.Rank;

namespace Quantities.Test.Dimensions;

public class DimensionsTest
{
    [Fact]
    public void RankOfSelfIsLinear()
    {
        var rank = Rank<Time>.Of<Time>();
        Assert.Equal(Linear, rank);
    }
    [Fact]
    public void RankOfLinearDimensionIsLinear()
    {
        var rank = Rank<Time>.Of<OtherTime>();
        Assert.Equal(Linear, rank);
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
        Assert.Equal(Linear, rank);
    }
    [Fact]
    public void RankOfLinearOnSquareIsSquare()
    {
        var rank = Rank<Area>.Of<Length>();
        Assert.Equal(Square, rank);
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
        Assert.Equal(Linear, rank);
    }
    [Fact]
    public void RankOfLinearOnCubeIsCubic()
    {
        var rank = Rank<Volume>.Of<Length>();
        Assert.Equal(Cubic, rank);
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
        Assert.Equal(Linear, rank);
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
        Assert.Equal(Linear, rank);
    }
    [Fact]
    public void RankOfConstituentOnSquaringProductIsSquare()
    {
        var rank = Rank<DoubleTime>.Of<Time>();
        Assert.Equal(Square, rank);
    }

    private static class Rank<TSelf>
        where TSelf : IDimension
    {
        public static Rank Of<TOther>()
            where TOther : IDimension => TSelf.RankOf<TOther>();
    }
}

// These are alle "dummy" classes.
// They are used to test "RankOf" on these interfaces:
// ILinear<TS>, ISquare<TS,T>, ICubic<TS,T>
// IProduct<TS,TV>, IQuotient<TS,N,D>
// where TS stands for TSelf.
file sealed class Time : ITime { }
file sealed class OtherTime : ITime { }
file sealed class Length : ILength { }
file sealed class Area : IArea { }
file sealed class OtherArea : IArea { }
file sealed class Volume : IVolume { }
file sealed class OtherVolume : IVolume { }
file sealed class SquareTemperature : ISquareTemperature { }
file sealed class CubicMass : ICubicMass { }
file sealed class Velocity : IVelocity { }
file sealed class OtherVelocity : IVelocity { }
file sealed class Angle : IAngle { }
file sealed class Ampere : IElectricCurrent { }
file sealed class Coulomb : IElectricCharge { }
file sealed class AmpereHour : IElectricCharge { }
file sealed class DoubleTime : IDoubleTime { }
file interface ICubicMass : ICubic<ICubicMass, IMass> { }
file interface IDoubleTime : IProduct<IDoubleTime, ITime, ITime> { }
file interface ISquareTemperature : ISquare<ISquareTemperature, ITemperature> { }
