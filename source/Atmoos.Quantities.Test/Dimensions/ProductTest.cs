using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Test.Dimensions;

public class ProductTest
{
    private static readonly Dimension someProduct = Dim<Length>.Per<Time>();
    [Fact]
    public void MultiplicativeDimensionsIsOfTypeProduct()
    {
        Assert.IsType<Product>(Dim<Time>.Times<Length>());
    }
    [Fact]
    public void DoesNotEquateToIdentity()
    {
        DimAssert.NotEqual(someProduct, Unit.Identity);
    }
    [Fact]
    public void EquatesToProductOfEqualQuantities()
    {
        DimAssert.Equal(Dim<Coulomb>.Per<Time>(), Dim<Coulomb>.Per<Time>());
    }
    [Fact]
    public void EqualityIsCommutative()
    {
        DimAssert.Equal(Dim<Length>.Times<Time>(), Dim<Time>.Times<Length>());
    }
    [Fact]
    public void EquatesToProductOfEqualQuantity_EvenWhenTheyAreNotTheSameInstance()
    {
        var left = Dim<Coulomb>.Per<Time>();
        var right = left.Copy();
        DimAssert.Equal(left, right);
        Assert.NotSame(left, right);
    }
    [Fact]
    public void EquatesToProductOfSimilarQuantities()
    {
        DimAssert.Equal(Dim<Coulomb>.Per<Time>(), Dim<AmpereHour>.Per<OtherTime>());
    }
    [Fact]
    public void DoesNotEquateToProductOfDifferingQuantity()
    {
        DimAssert.NotEqual(Dim<Length>.Per<Time>(), Dim<Length>.Per<Ampere>());
    }
    [Fact]
    public void DoesNotEquateToTheInverseProduct()
    {
        DimAssert.NotEqual(Dim<Length>.Per<Time>(), Dim<Time>.Per<Length>());
    }
    [Fact]
    public void DoesNotEquateToAnyScalar()
    {
        DimAssert.NotEqual(someProduct, Dim<Coulomb>.Value);
    }
    [Fact]
    public void DoesNotEquateToSameProductRaisedToPowerOtherThanOne()
    {
        var squareProduct = someProduct.Pow(2);
        DimAssert.NotEqual(someProduct, squareProduct);
    }
    [Fact]
    public void DoesNotEquateToProductOfDifferentMultiplicity()
    {
        var any = someProduct.Pow(-2);
        var anyOther = someProduct.Pow(2);
        DimAssert.NotEqual(any, anyOther);
    }
    [Fact]
    public void DoesNotEquateToProductOfSameMultiplicityButDifferingQuantities()
    {
        const Int32 multiplicity = 5;
        var any = Dim<Time>.Times<Length>().Pow(multiplicity);
        var anyOther = Dim<Coulomb>.Times<Length>().Pow(multiplicity);
        DimAssert.NotEqual(any, anyOther);
    }

    [Fact]
    public void ZerothPowerOfProductIsUnit()
    {
        Assert.IsType<Unit>(someProduct.Pow(0));
    }
    [Fact]
    public void RaisedToThePowerOfOneIsTheSameInstance()
    {

        var actual = someProduct.Pow(1);

        Assert.Same(someProduct, actual);
    }
    [Fact]
    public void ProductOfWithScalarIsOfTypeProduct()
    {
        Assert.IsType<Product>(Dim<Time>.Times<Length>() * Dim<Ampere>.Value);
    }
    [Fact]
    public void ProductOfDifferingProductsIsOfTypeProduct()
    {
        Assert.IsType<Product>(Dim<Time>.Times<Length>() * Dim<Angle>.Per<Current>());
    }
    [Fact]
    public void QuotientOfDifferingProductsIsOfTypeProduct()
    {
        Assert.IsType<Product>(Dim<Time>.Times<Length>() * Dim<Angle>.Per<Current>());
    }
    [Fact]
    public void ProductOfSameProductIsOfTypeProduct()
    {
        Assert.IsType<Product>(Dim<Time>.Times<Length>() * Dim<Time>.Times<Length>());
    }
    [Fact]
    public void QuotientOfSameProductIsOfTypeUnit()
    {
        Assert.IsType<Unit>(Dim<Time>.Times<Length>() / Dim<OtherTime>.Times<Length>());
    }
    [Fact]
    public void ProductOfInverseProductIsOfTypeUnit()
    {
        Assert.IsType<Unit>(Dim<Time>.Per<OtherLength>() * Dim<Length>.Per<OtherTime>());
    }
    [Fact]
    public void ProductOfSameQuantityIsTheSameQuantitySquared()
    {
        var expected = someProduct.Pow(2);
        var actual = someProduct * someProduct;
        DimAssert.Equal(expected, actual);
    }
    [Fact]
    public void TimeSimilarQuantityResultsInASquaredQuantity()
    {
        var expected = Dim<Time>.Times<Length>().Pow(2);
        var actual = Dim<Time>.Times<OtherLength>() * Dim<OtherTime>.Times<Length>();
        DimAssert.Equal(expected, actual);
    }
    [Fact]
    public void SimplifyExponentsOnAllPositiveValuesRetainsAllPositive()
    {
        var expected = (Dim<Length>.Pow(3) * Dim<Time>.Pow(2)).Pow(4);
        var actual = Product.SimplifyExponents(Dim<Length>.Pow(12), Dim<Time>.Pow(8));
        DimAssert.Equal(expected, actual);
    }
    [Fact]
    public void SimplifyExponentsOnNegAndPosValuesCreatesPositiveOuterExponent()
    {
        var expected = (Dim<Length>.Pow(-3) * Dim<Time>.Value).Pow(4);
        var actual = Product.SimplifyExponents(Dim<Length>.Pow(-12), Dim<Time>.Pow(4));
        DimAssert.Equal(expected, actual);
    }
    [Fact]
    public void SimplifyExponentsOnAllNegativeValuesMovesNegativeExponentsToOuterExponent()
    {
        var expected = (Dim<Length>.Pow(3) * Dim<Time>.Pow(2)).Pow(-2);
        var actual = Product.SimplifyExponents(Dim<Length>.Pow(-6), Dim<Time>.Pow(-4));
        DimAssert.Equal(expected, actual);
    }
    [Fact]
    public void MultiplicitiesOfSimilarProductsAreAddedWhenMultiplied()
    {
        var left = Dim<Time>.Times<OtherLength>().Pow(14);
        var right = Dim<OtherTime>.Times<Length>().Pow(-5);

        var actual = left * right;

        Assert.Equal(left.E + right.E, actual.E);
    }

    [Fact]
    public void MultiplicitiesOfDifferingProductsAreConsumedByProductWhenMultiplied()
    {
        var left = Dim<Current>.Times<Temperature>().Pow(14);
        var right = Dim<Time>.Times<Length>().Pow(-5);

        var actual = left * right;

        Assert.Equal(1, actual.E);
    }

    [Fact]
    public void MultiplicitiesOfSimilarProductsAreSubtractedWhenDivided()
    {
        var left = Dim<Time>.Times<OtherLength>().Pow(14);
        var right = Dim<OtherTime>.Times<Length>().Pow(-5);

        var actual = left / right;

        Assert.Equal(left.E - right.E, actual.E);
    }
    [Fact]
    public void MultiplicitiesOfDifferingProductsAreConsumedByProductWhenDivided()
    {
        var left = Dim<Current>.Times<Temperature>().Pow(14);
        var right = Dim<Time>.Times<Length>().Pow(-5);

        var actual = left / right;

        Assert.Equal(1, actual.E);
    }
    [Fact]
    public void ScalarsAreProperlyResolvedOnRightHandBaseCase()
    {
        var rh = Dim<Length>.Value;
        var lhBaseCase = Dim<Current>.Times<Length>();
        var expected = Dim<Current>.Value * Dim<Length>.Pow(2);

        var actual = lhBaseCase * rh;

        DimAssert.Equal(expected, actual);
    }
    [Fact]
    public void ScalarsAreProperlyResolvedOnLeftsHandBaseCase()
    {
        var lh = Dim<Current>.Value;
        var rhBaseCase = Dim<Current>.Times<Length>();
        var expected = Dim<Current>.Value.Pow(2) * Dim<Length>.Value;

        var actual = rhBaseCase * lh;

        DimAssert.Equal(expected, actual);
    }
    [Fact]
    public void ScalarsAreProperlyResolvedRecursively()
    {
        var scalar = Dim<Length>.Value;
        var multiProduct = Dim<Current>.Times<Length>() * Dim<Temperature>.Per<Time>();
        var expected = Dim<Current>.Value * Dim<Length>.Pow(2) * Dim<Temperature>.Per<Time>();

        var actual = multiProduct * scalar;

        DimAssert.Equal(expected, actual);
    }
    [Fact]
    public void EnumeratesAllLeafScalars()
    {
        IEnumerable<Scalar> expected = new[] { Dim<Current>.Value, Dim<Length>.Value, Dim<Temperature>.Value, Dim<Time>.Value }.SelectMany(d => d);
        IEnumerable<Scalar> actual = Dim<Current>.Times<Length>() * Dim<Temperature>.Times<Time>();

        DimAssert.Equal(expected, actual);
    }
    [Fact]
    public void EnumerationResolvesProductMultiplicity()
    {
        IEnumerable<Scalar> expected = new[] { Dim<Current>.Value.Pow(2), Dim<Length>.Value.Pow(6), Dim<Temperature>.Value }.SelectMany(d => d).ToArray();
        Dimension product = (Dim<Current>.Value * Dim<Length>.Value.Pow(3)).Pow(2) * Dim<Temperature>.Value;

        DimAssert.Equal(expected, product);
    }
    [Fact]
    public void CommonRootIsFalseOnDifferingDimensions()
    {
        Dimension self = Dim<Time>.Times<Temperature>();
        Dimension other = Dim<Length>.Times<Current>();

        Assert.False(self.CommonRoot(other));
    }
    [Fact]
    public void CommonRootIsFalseWhenComparingToUnit()
    {
        Dimension self = Dim<Time>.Times<Length>();
        Dimension other = Unit.Identity;

        Assert.False(self.CommonRoot(other));
    }
    [Fact]
    public void CommonRootIsFalseWhenComparingToScalars()
    {
        Dimension self = Dim<Time>.Times<Length>();
        Dimension other = Dim<Time>.Value;

        Assert.False(self.CommonRoot(other));
    }
    [Fact]
    public void CommonRootIsTrueOnSameDimensions()
    {
        Dimension self = Dim<Time>.Times<Length>().Pow(3);
        Dimension other = Dim<Time>.Times<Length>().Pow(3);

        Assert.True(self.CommonRoot(other));
    }
    [Fact]
    public void CommonRootIsTrueOnSameInnerDimensionsWithDifferingExponent()
    {
        Dimension self = Dim<Time>.Times<Length>().Pow(2);
        Dimension other = Dim<Time>.Times<Length>().Pow(3);

        Assert.True(self.CommonRoot(other));
    }
    [Fact]
    public void CommonRootIsFalseOnInnerDimensionsDifferingOnlyByExponent()
    {
        Dimension self = (Dim<Time>.Value * Dim<Length>.Value).Pow(2);
        Dimension other = (Dim<Time>.Value * Dim<Length>.Pow(2)).Pow(2);

        Assert.False(self.CommonRoot(other));
    }
    [Fact]
    public void CommonRootIsTrueWhenProductEvaluationIsInDifferingOrder()
    {
        Dimension self = Dim<Time>.Value * Dim<Length>.Value * Dim<Current>.Pow(3);
        Dimension other = Dim<Time>.Value * (Dim<Length>.Value * Dim<Current>.Pow(3));

        Assert.True(self.CommonRoot(other));
    }
    [Fact]
    public void CommonRootIsTrueWhenProductIsInDifferingOrder()
    {
        Dimension self = Dim<Time>.Value * Dim<Length>.Value * Dim<Current>.Pow(3);
        Dimension other = Dim<Length>.Value * Dim<Current>.Pow(3) * Dim<Time>.Value;

        Assert.True(self.CommonRoot(other));
    }
}
