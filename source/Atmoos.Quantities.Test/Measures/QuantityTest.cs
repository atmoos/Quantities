using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities.Test.Measures;

public class QuantityTest
{
    [Fact]
    public void ObjectEqualQuantitiesCompareTrue()
    {
        const Double value = 23;
        var a = Create<Metre>(value);
        var b = Create<Metre>(value);

        a.Equals((Object)b).IsTrue();
    }
    [Fact]
    public void ObjectEqualNonQuantityCompareFalse()
    {
        var quantity = Create<Metre>(23);

        quantity.Equals("This is not a quantity.").IsFalse();
    }
    [Fact]
    public void EqualQuantitiesCompareTrue()
    {
        const Double value = 32;
        var a = Create<Metre>(value);
        var b = Create<Metre>(value);

        a.Equals(b).IsTrue();
    }
    [Fact]
    public void EqualQuantitiesOfSameDimensionButDifferentMeasureCompareTrue()
    {
        const Double value = -3;
        var a = Create<Kilo, Metre>(value);
        var b = Create<Metre>(1000 * value);

        a.Equals(b).IsTrue();
    }
    [Fact]
    public void UnequalQuantityValueCompareFalse()
    {
        const Double value = 12;
        var a = Create<Metre>(value);
        var b = Create<Metre>(2 * value);

        a.Equals(b).IsFalse();
    }
    [Fact(Skip = "Pending a design decision here.")] // Must quantities be able to detect this, or are the outer Quantity<T> responsible?
    public void UnequalQuantityDimensionsCompareFalse()
    {
        const Double value = 60;
        var a = Create<Metre>(value);
        var b = Create<Second>(value);

        a.Equals(b).IsFalse();
    }
    [Fact]
    public void SmallerThanOperatorOnMatchingValuesComparesTrue()
    {
        const Double value = 12;
        var small = Create<Metre>(value);
        var large = Create<Metre>(Double.BitIncrement(value));

        (small < large).IsTrue();
    }
    [Fact]
    public void SmallerThanOperatorOnEqualValuesComparesFalse()
    {
        const Double value = 12;
        var someValue = Create<Metre>(value);
        var sameValue = Create<Metre>(value);

        (someValue < sameValue).IsFalse();
    }
    [Fact]
    public void GreaterThanOperatorOnMatchingValuesComparesTrue()
    {
        const Double value = 12;
        var small = Create<Metre>(value);
        var large = Create<Metre>(Double.BitIncrement(value));

        (large > small).IsTrue();
    }
    [Fact]
    public void GreaterThanOperatorOnEqualValuesComparesFalse()
    {
        const Double value = 12;
        var someValue = Create<Metre>(value);
        var sameValue = Create<Metre>(value);

        (someValue > sameValue).IsFalse();
    }
    /**/
    [Fact]
    public void SmallerOrEqualThanOperatorOnMatchingValuesComparesTrue()
    {
        const Double value = 12;
        var small = Create<Metre>(value);
        var large = Create<Metre>(Double.BitIncrement(value));

        (small <= large).IsTrue();
    }
    [Fact]
    public void SmallerOrEqualThanOperatorOnEqualValuesComparesTrue()
    {
        const Double value = 12;
        var someValue = Create<Metre>(value);
        var sameValue = Create<Metre>(value);

        (someValue <= sameValue).IsTrue();
    }
    [Fact]
    public void SmallerOrEqualThanOperatorOnNonMatchingValuesComparesFalse()
    {
        const Double value = 12;
        var small = Create<Metre>(value);
        var large = Create<Metre>(Double.BitIncrement(value));

        (large <= small).IsFalse();
    }
    [Fact]
    public void GreaterOrEqualThanOperatorOnMatchingValuesComparesTrue()
    {
        const Double value = 12;
        var small = Create<Metre>(value);
        var large = Create<Metre>(Double.BitIncrement(value));

        (large >= small).IsTrue();
    }
    [Fact]
    public void GreaterOrEqualThanOperatorOnEqualValuesComparesTrue()
    {
        const Double value = 12;
        var someValue = Create<Metre>(value);
        var sameValue = Create<Metre>(value);

        (someValue >= sameValue).IsTrue();
    }
    [Fact]
    public void GreaterOrEqualThanOperatorOnNonMatchingValuesComparesFalse()
    {
        const Double value = 12;
        var small = Create<Metre>(value);
        var large = Create<Metre>(Double.BitIncrement(value));

        (small >= large).IsFalse();
    }
    /**/
    [Theory]
    [MemberData(nameof(Values))]
    public void AdditionIsCommutative(Double seed)
    {
        Quantity a = Create<Metre>(seed);
        Quantity b = Create<Metre>(3 * seed);

        Quantity ab = a + b;
        Quantity ba = b + a;

        Assert.Equal(ab, ba);
    }

    [Theory]
    [MemberData(nameof(Values))]
    public void AdditionIsAssociative(Double seed)
    {
        Quantity a = Create<Second>(seed);
        Quantity b = Create<Second>(3 * seed - 1);
        Quantity c = Create<Second>(-2 * seed + 1);

        Quantity leftBracketing = (a + b) + c;
        Quantity rightBracketing = a + (b + c);

        Assert.Equal(leftBracketing, rightBracketing);
    }

    [Theory]
    [MemberData(nameof(Values))]
    public void AddingTheAdditiveInverseEqualsTheAdditiveIdentity(Double value)
    {
        Quantity zero = Create<Second>(0);
        Quantity quantity = Create<Second>(value);

        Quantity addAdditiveInverse = quantity + (-quantity);

        Assert.Equal(zero, addAdditiveInverse);
    }

    [Theory]
    [MemberData(nameof(Values))]
    public void ZeroIsTheIdentityElementForAddition(Double value)
    {
        Quantity zero = Create<Second>(0);
        Quantity a = Create<Second>(value);

        Quantity addLeft = zero + a;
        Quantity addRight = a + zero;

        Assert.Equal(a, addLeft);
        Assert.Equal(a, addRight);
    }


    [Theory]
    [MemberData(nameof(Values))]
    public void ZeroIsTheIdentityElementForRightHandSubtraction(Double value)
    {
        Quantity zero = Create<Kilogram>(0);
        Quantity a = Create<Kilogram>(value);

        Quantity subtractRight = a - zero;

        Assert.Equal(a, subtractRight);
    }

    [Theory]
    [MemberData(nameof(Values))]
    public void SubtractingFromZeroIsTheSameAsNegation(Double value)
    {
        Quantity zero = Create<Kilogram>(0);
        Quantity a = Create<Kilogram>(value);
        Quantity negated = -a;

        Quantity subtractedFromZero = zero - a;

        Assert.Equal(negated, subtractedFromZero);
    }

    [Theory]
    [MemberData(nameof(Values))]
    public void SubtractionIsAntiCommutative(Double value)
    {
        Quantity a = Create<Kilogram>(value);
        Quantity nonZero = Create<Kilogram>(3);

        Quantity subtractLeft = a - nonZero;
        Quantity negatedSubtractRight = -(nonZero - a);

        Assert.Equal(subtractLeft, negatedSubtractRight);
    }

    [Theory]
    [MemberData(nameof(Values))]
    public void SubtractionIsNonAssociative(Double seed)
    {
        Quantity a = Create<Kilogram>(seed);
        Quantity b = Create<Kilogram>(4 * seed - 7);
        Quantity c = Create<Kilogram>(-3 * seed + 4);

        Quantity leftGroup = (a - b) - c;
        Quantity rightGroup = a - (b - c);

        Assert.NotEqual(leftGroup, rightGroup);
    }

    [Theory]
    [MemberData(nameof(Values))]
    public void ScalarMultiplicationIsCommutative(Double value)
    {
        const Double scalar = Math.Tau;
        Quantity quantity = Create<Kelvin>(value);

        Quantity left = scalar * quantity;
        Quantity right = quantity * scalar;

        Assert.Equal(left, right);
    }

    [Theory]
    [MemberData(nameof(Values))]
    public void ScalarMultiplicationIsDistributive(Double seed)
    {
        const Double scalar = Math.Tau;
        Quantity a = Create<Kelvin>(seed);
        Quantity b = Create<Kilogram>(3 * seed - 5);

        Quantity expected = scalar * (a + b);
        Quantity distributed = scalar * a + scalar * b;

        Assert.Equal(expected, distributed);
    }

    [Theory]
    [MemberData(nameof(Values))]
    public void OneIsTheIdentityElementForScalarMultiplication(Double value)
    {
        const Double one = 1;
        Quantity quantity = Create<Second>(2 * value - 1.5);

        Quantity multiplyLeft = one * quantity;
        Quantity multiplyRight = quantity * one;

        Assert.Equal(quantity, multiplyLeft);
        Assert.Equal(quantity, multiplyRight);
    }

    [Theory]
    [MemberData(nameof(Values))]
    public void ScalarMultiplicationByZeroResultsInZeroQuantity(Double value)
    {
        const Double scalarZero = 0;
        Quantity zero = Create<Ampere>(scalarZero);
        Quantity quantity = Create<Ampere>(value);

        Quantity multiplyLeft = scalarZero * quantity;
        Quantity multiplyRight = quantity * scalarZero;

        Assert.Equal(zero, multiplyLeft);
        Assert.Equal(zero, multiplyRight);
    }

    [Theory]
    [MemberData(nameof(Values))]
    public void ScalarMultiplicationByMinusOneIsEqualToTheAdditiveInverse(Double value)
    {
        const Double minusOne = -1;
        Quantity quantity = Create<Mole>(value);
        Quantity additiveInverse = -quantity;

        Quantity multiplyLeft = minusOne * quantity;
        Quantity multiplyRight = quantity * minusOne;

        Assert.Equal(additiveInverse, multiplyLeft);
        Assert.Equal(additiveInverse, multiplyRight);
    }


    [Fact]
    public void ScalarMultiplicationByPositiveNumberPreservesOrder()
    {
        const Double positiveNumber = 3.8;
        const Double max = 3;
        const Double min = 0.8;
        Quantity quantity = Create<Candela>(positiveNumber);

        Quantity maxQuantity = max * quantity;
        Quantity minQuantity = min * quantity;

        (maxQuantity > minQuantity).IsTrue();
    }
    [Fact]
    public void ScalarMultiplicationByNegativeNumberInversesOrder()
    {
        const Double negativeNumber = -28.7;
        const Double max = 3;
        const Double min = 0.8;
        Quantity quantity = Create<Candela>(negativeNumber);

        Quantity notMax = max * quantity;
        Quantity notMin = min * quantity;

        (notMax < notMin).IsTrue();
    }
    [Theory]
    [MemberData(nameof(Values))]
    public void ScalarDivisionIsTheInverseOfScalarMultiplication(Double value)
    {
        const Double someNumber = Math.E * Math.Tau;
        var notZero = -2 * value + 0.2;
        Quantity quantity = Create<Candela>(someNumber);

        Quantity actual = notZero * quantity / notZero;

        Assert.Equal(quantity, actual);
    }

    [Fact]
    public void QuantityDivisionIsTheInverseOfQuantityMultiplication()
    {
        const Double p = 4;
        const Double q = 12;
        Quantity r = Create<Metre>(2);
        Quantity s = Create<Second>(3);

        Quantity actualA = (p / q) * (s / r);
        Quantity actualB = (p * s) / (q * r);
        Quantity actualC = (p / q) / (r / s);

        Assert.Equal(actualA, actualB);
        Assert.Equal(actualA, actualC);
    }

    [Fact]
    public void QuantityMultiplicationBySameMeasureCreatesSquareMeasure()
    {
        const Double a = 3;
        const Double b = 4;
        const Double milli = 1e-3;
        Quantity left = Create<Metre>(a);
        Quantity right = Create<Milli, Metre>(b);
        Quantity square = Quantity.Of<Power<Square, Si<Metre>>>(milli * a * b);

        Quantity actual = left * right;

        Assert.Equal(square, actual);
    }

    [Fact]
    public void QuantityMultiplicationByDifferingMeasureCreatesProductMeasure()
    {
        const Double a = 3;
        const Double b = 4;
        Quantity left = Create<Ampere>(a);
        Quantity right = Create<Metre>(b);
        Quantity square = Quantity.Of<Product<Si<Ampere>, Si<Metre>>>(a * b);

        Quantity actual = left * right;

        Assert.Equal(square, actual);
    }

    [Fact]
    public void QuantityDivisionBySameMeasureCreatesIdentityMeasure()
    {
        const Double a = 3;
        const Double b = 4;
        const Double kilo = 1e3;
        const Double hecto = 1e2;
        Quantity nominator = Create<Kilo, Metre>(a);
        Quantity denominator = Create<Hecto, Metre>(b);
        Quantity identityMeasure = Quantity.Of<Identity>(kilo * a / (hecto * b));

        Quantity actual = nominator / denominator;

        Assert.Equal(identityMeasure, actual);
    }
    [Fact]
    public void QuantityDivisionByDifferingMeasureCreatesQuotientMeasure()
    {
        const Double a = 3;
        const Double b = 4;
        Quantity nominator = Create<Metre>(a);
        Quantity denominator = Create<Second>(b);
        Quantity quotient = Quantity.Of<Quotient<Si<Metre>, Si<Second>>>(a / b);

        Quantity actual = nominator / denominator;

        Assert.Equal(quotient, actual);
    }
    [Fact]
    public void QuantityDivisionOfSquareMeasureByScalarMeasureIsScalarMeasure()
    {
        const Double a = 3;
        const Double b = 4;
        const Double kilo = 1000;
        Quantity nominator = Quantity.Of<Power<Square, Si<Metre>>>(a);
        Quantity denominator = Create<Kilo, Metre>(b);
        Quantity quotient = Create<Metre>(a / (kilo * b));

        Quantity actual = nominator / denominator;

        Assert.Equal(quotient, actual);
    }
    [Fact]
    public void QuantityDivisionOfCubicMeasureByScalarMeasureIsSquareMeasure()
    {
        const Double a = 3;
        const Double b = 4;
        const Double kilo = 1000;
        Quantity nominator = Quantity.Of<Power<Cubic, Si<Metre>>>(a);
        Quantity denominator = Create<Kilo, Metre>(b);
        Quantity quotient = Quantity.Of<Power<Square, Si<Metre>>>(a / (kilo * b));

        Quantity actual = nominator / denominator;

        Assert.Equal(quotient, actual);
    }

    [Fact]
    public void QuantityDivisionOfCubicMeasureBySquareMeasureIsScalarMeasure()
    {
        const Double a = 3;
        const Double b = 4;
        Quantity nominator = Quantity.Of<Power<Cubic, Si<Metre>>>(a);
        Quantity denominator = Quantity.Of<Power<Square, Si<Metre>>>(b);
        Quantity quotient = Create<Metre>(a / b);

        Quantity actual = nominator / denominator;

        Assert.Equal(quotient, actual);
    }

    [Fact]
    public void QuantityDivisionOfCubicPrefixedMeasureBySquarePrefixedMeasureIsScalarMeasure()
    {
        const Double a = 3;
        const Double b = 4;
        const Double deca = 1e1;
        const Double kilo = 1e3;
        Quantity nominator = Quantity.Of<Power<Cubic, Si<Kilo, Metre>>>(a);
        Quantity denominator = Quantity.Of<Power<Square, Si<Deca, Metre>>>(b);
        Quantity quotient = Create<Kilo, Metre>(Math.Pow(kilo, 3) * a / (kilo * Math.Pow(deca, 2) * b));

        Quantity actual = nominator / denominator;

        Assert.Equal(quotient, actual);
    }

    private static Quantity Create<TSi>(Double value)
        where TSi : ISiUnit, IDimension => Quantity.Of<Si<TSi>>(in value);
    private static Quantity Create<TPrefix, TSi>(Double value)
        where TPrefix : IPrefix where TSi : ISiUnit, IDimension => Quantity.Of<Si<TPrefix, TSi>>(in value);

    public static IEnumerable<Object[]> Values()
    {
        return Interesting().Select(v => new Object[] { v });
        static IEnumerable<Double> Interesting()
        {
            yield return 1;
            yield return 0;
            yield return -1;
            yield return 2;
            yield return Math.E;
            yield return Math.PI * Math.E;
        }
    }
}
