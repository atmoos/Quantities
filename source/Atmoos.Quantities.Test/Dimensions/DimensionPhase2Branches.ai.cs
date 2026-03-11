using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Test.Dimensions;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public sealed class DimensionPhase2Branches
{
    [Fact]
    public void EqualityOperatorOnNullValuesReturnsTrue()
    {
        Dimension? left = null;
        Dimension? right = null;

        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact]
    public void EqualityOperatorOnLeftNullAndRightValueReturnsFalse()
    {
        Dimension? left = null;
        Dimension? right = Unit.Identity;

        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact]
    public void EqualityOperatorOnLeftValueAndRightNullReturnsFalse()
    {
        Dimension? left = Unit.Identity;
        Dimension? right = null;

        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact]
    public void ScalarRootByZeroThrows()
    {
        Dimension scalar = Dim<Time>.Value;

        Assert.Throws<DivideByZeroException>(() => scalar.Root(0));
    }

    [Fact]
    public void ScalarTimesUnitReturnsSameInstance()
    {
        Dimension scalar = Dim<Length>.Value;

        Dimension actual = scalar * Unit.Identity;

        Assert.Same(scalar, actual);
    }

    [Fact]
    public void ScalarTimesDisjointProductUsesFallbackPath()
    {
        Dimension scalar = Dim<Time>.Value;
        Dimension product = Dim<Current>.Times<Length>();

        Dimension actual = scalar * product;

        DimAssert.Equal(Dim<Time>.Value * Dim<Current>.Value * Dim<Length>.Value, actual);
    }

    [Fact]
    public void ProductTimesOverlappingProductSimplifiesSharedDimensions()
    {
        Dimension left = Dim<Time>.Times<Length>();
        Dimension right = Dim<Length>.Times<Current>();

        Dimension actual = left * right;

        DimAssert.Equal(Dim<Time>.Value * Dim<Length>.Pow(2) * Dim<Current>.Value, actual);
    }

    [Fact]
    public void ProductTimesDisjointProductFallsBackToProduct()
    {
        Dimension left = Dim<Time>.Times<Length>();
        Dimension right = Dim<Current>.Times<Temperature>();

        Dimension actual = left * right;

        Assert.IsType<Product>(actual);
        DimAssert.Equal(Dim<Time>.Value * Dim<Length>.Value * Dim<Current>.Value * Dim<Temperature>.Value, actual);
    }
}
