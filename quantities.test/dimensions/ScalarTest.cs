using Quantities.Dimensions;

namespace Quantities.Test.Dimensions;

public class ScalarTest
{
    [Fact]
    public void SingularDimensionsIsOfTypeScalar()
    {
        Assert.IsAssignableFrom<Scalar>(Dim<Time>.Value);
    }
    [Fact]
    public void DoesNotEquateToIdentity()
    {
        Assert.NotEqual(Dim<Time>.Value, Unit.Identity);
    }
    [Fact]
    public void EquatesToScalarOfEqualQuantity()
    {
        Assert.Equal(Dim<Time>.Value, Dim<Time>.Value);
    }
    [Fact]
    public void EquatesToScalarOfEqualQuantity_EvenWhenTheyAreNotTheSameInstance()
    {
        var left = Dim<Time>.Value;
        var right = Dim<Time>.Value;
        Assert.Equal(left, right);
        Assert.NotSame(left, right);
    }
    [Fact]
    public void EquatesToScalarOfSimilarQuantity()
    {
        Assert.Equal(Dim<Time>.Value, Dim<OtherTime>.Value);
    }
    [Fact]
    public void DoesNotEquateToScalarOfDifferingQuantity()
    {
        Assert.NotEqual(Dim<Time>.Value, Dim<Length>.Value);
    }
    [Fact]
    public void DoesNotEquateToAnyProduct()
    {
        Assert.NotEqual(Dim<Time>.Value, Dim<Time>.Times<Length>());
    }
    [Fact]
    public void DoesNotEquateToSameScalarRaisedToPowerOtherThanOne()
    {
        var scalar = Dim<Time>.Value;
        var squareScalar = Dim<Time>.Pow(2);
        Assert.NotEqual(scalar, squareScalar);
    }
    [Fact]
    public void DoesNotEquateToScalarOfDifferentMultiplicity()
    {
        var any = Dim<Time>.Pow(-2);
        var anyOther = Dim<Time>.Pow(2);
        Assert.NotEqual(any, anyOther);
    }
    [Fact]
    public void DoesNotEquateToScalarOfSameMultiplicityButDifferingQuantity()
    {
        const Int32 multiplicity = 5;
        var any = Dim<Time>.Pow(multiplicity);
        var anyOther = Dim<Length>.Pow(multiplicity);
        Assert.NotEqual(any, anyOther);
    }

    [Fact]
    public void ZerothPowerOfScalarIsUnit()
    {
        Assert.IsType<Unit>(Dim<Length>.Pow(0));
    }
    [Fact]
    public void RaisedToThePowerOfOneIsTheSameInstance()
    {
        var scalar = Dim<Time>.Value;

        var actual = scalar.Pow(1);

        Assert.Same(scalar, actual);
    }
    [Fact]
    public void ProductOfDifferingScalarsIsOfTypeProduct()
    {
        Assert.IsType<Product>(Dim<Time>.Value * Dim<Length>.Value);
    }
    [Fact]
    public void QuotientOfDifferingScalarsIsOfTypeProduct()
    {
        Assert.IsType<Product>(Dim<Time>.Value / Dim<Length>.Value);
    }
    [Fact]
    public void ProductOfSameScalarsIsOfTypeScalar()
    {
        Assert.IsAssignableFrom<Scalar>(Dim<Time>.Value * Dim<Time>.Value);
    }
    [Fact]
    public void QuotientOfSameScalarsIsOfTypeUnit()
    {
        Assert.IsType<Unit>(Dim<Time>.Value / Dim<Time>.Value);
    }
    [Fact]
    public void ProductOfSameQuantityIsTheSameQuantitySquared()
    {
        var expected = Dim<Time>.Pow(2);
        var actual = Dim<Time>.Times<Time>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void TimeSimilarQuantityResultsInASquaredQuantity()
    {
        var expected = Dim<Time>.Pow(2);
        var actual = Dim<Time>.Times<OtherTime>();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void MultiplicationIsCommutative()
    {
        var left = Dim<Time>.Value;
        var right = Dim<Length>.Value;

        var expected = left * right;
        var actual = right * left;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MultiplicitiesOfSimilarScalarsAreAddedWhenMultiplied()
    {
        var left = Dim<Time>.Value.Pow(14);
        var right = Dim<OtherTime>.Value.Pow(-5);

        var actual = left * right;

        Assert.Equal(left.E + right.E, actual.E);
    }
    [Fact]
    public void MultiplicitiesOfDifferingScalarsAreAbsorbedByProduct()
    {
        var left = Dim<Time>.Value.Pow(14);
        var right = Dim<Length>.Value.Pow(-5);

        var actual = left * right;

        Assert.Equal(1, actual.E);
    }

    [Fact]
    public void MultiplicitiesAreSubtractedWhenDivided()
    {
        var left = Dim<Time>.Value.Pow(12);
        var right = Dim<OtherTime>.Value.Pow(4);

        var actual = left / right;

        Assert.Equal(left.E - right.E, actual.E);
    }
    [Fact]
    public void ProductsAreProperlySimplifiedOnRightHandTerm()
    {
        var scalar = Dim<Length>.Value;
        var rhProduct = Dim<Current>.Times<Length>();
        var expected = Dim<Current>.Value * Dim<Length>.Pow(2);

        var actual = scalar * rhProduct;

        Assert.Equal(expected, actual);
    }
    [Fact]
    public void ProductsAreProperlySimplifiedOnLeftHandTerm()
    {
        var scalar = Dim<Current>.Value;
        var lhProduct = Dim<Current>.Times<Length>();
        var expected = Dim<Current>.Value.Pow(2) * Dim<Length>.Value;

        var actual = scalar * lhProduct;

        Assert.Equal(expected, actual);
    }
    [Fact]
    public void EnumeratesItselfOnly()
    {
        Dimension self = Dim<Time>.Value;
        IEnumerable<Scalar> actual = self;
        IEnumerable<Scalar> expected = new Scalar[] { (Scalar)self };

        Assert.Equal(expected, actual, new DimComparer()); // default comparer seems to compare recursively!
    }

    [Theory]
    [InlineData(-142, "⁻¹⁴²")]
    [InlineData(-10, "⁻¹⁰")]
    [InlineData(-9, "⁻⁹")]
    [InlineData(-8, "⁻⁸")]
    [InlineData(-7, "⁻⁷")]
    [InlineData(-6, "⁻⁶")]
    [InlineData(-5, "⁻⁵")]
    [InlineData(-4, "⁻⁴")]
    [InlineData(-3, "⁻³")]
    [InlineData(-2, "⁻²")]
    [InlineData(-1, "⁻¹")]
    [InlineData(0, "𝟙")]
    [InlineData(1, "")]
    [InlineData(2, "²")]
    [InlineData(3, "³")]
    [InlineData(4, "⁴")]
    [InlineData(5, "⁵")]
    [InlineData(6, "⁶")]
    [InlineData(7, "⁷")]
    [InlineData(8, "⁸")]
    [InlineData(9, "⁹")]
    [InlineData(10, "¹⁰")]
    [InlineData(372, "³⁷²")]
    public void ToStringHandlesExponentsCorrectly(Int32 exp, String exponent)
    {
        var value = Dim<Time>.Value.Pow(exp);

        var expected = Math.Abs(exp) == 0 ? exponent : $"Time{exponent}";
        Assert.Equal(expected, value.ToString());
    }
}
