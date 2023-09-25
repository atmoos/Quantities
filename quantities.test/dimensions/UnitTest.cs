using Quantities.Dimensions;

namespace Quantities.Test.Dimensions;

public class UnitTest
{
    [Fact]
    public void IdentityIsOfTypeUnit()
    {
        Assert.IsType<Unit>(Unit.Identity);
    }
    [Fact]
    public void IdentitiesEquateEqual()
    {
        Assert.Equal(Unit.Identity, Unit.Identity);
    }
    [Fact]
    public void IdentityIsNotEqualToAnyScalar()
    {
        Assert.NotEqual(Unit.Identity, Dim<Time>.Value);
    }
    [Fact]
    public void IdentityIsNotEqualToAnyProduct()
    {
        Assert.NotEqual(Unit.Identity, Dim<Time>.Times<Length>());
    }
    [Fact]
    public void ExponentIsZero()
    {
        Assert.Equal(0, Unit.Identity.E);
    }
    [Theory]
    [MemberData(nameof(AnyPowers))]
    public void RaisedToAnyPowerRemainsUnit(Int32 power)
    {
        Assert.IsType<Unit>(Unit.Identity.Pow(power));
    }
    [Fact]
    public void TimesAnyDimensionIsTheSameDimension()
    {
        var anyDimension = Dim<Length>.Value;

        var actual = Unit.Identity * anyDimension;

        Assert.Same(anyDimension, actual);
    }
    [Fact]
    public void EnumeratesEmpty()
    {
        Assert.Empty(Unit.Identity);
    }

    public static IEnumerable<Object[]> AnyPowers()
    {
        var any = new[] { -12, -3, -2, -1, 0, 1, 2, 3, 4, 564 };
        return any.Select(p => new Object[] { p });
    }
}
