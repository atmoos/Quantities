namespace Atmoos.Quantities.Test;

public class OperatorsTest
{
    private static readonly Length length = Length.Of(23, Si<Metre>());
    private static readonly Length epsilon = Length.Of(2, Si<Pico, Metre>());
    private static readonly Double epsilonValue = epsilon.To(Si<Metre>()).Value;
    private static readonly Double someScalar = Math.Tau / Math.E;

    [Fact]
    public void AdditionWorks()
    {
        var sum = length + epsilon;
        Assert.Equal(length.Value + epsilonValue, sum.Value);
    }

    [Fact]
    public void SubtractionWorks()
    {
        var difference = length - epsilon;
        Assert.Equal(length.Value - epsilonValue, difference.Value);
    }

    [Fact]
    public void ScalarLeftMultiplicationWorks()
    {
        var scaled = someScalar * length;
        Assert.Equal(someScalar * (Double)length.Value, scaled.Value);
    }

    [Fact]
    public void ScalarRightMultiplicationWorks()
    {
        var scaled = length * someScalar;
        Assert.Equal(someScalar * (Double)length.Value, scaled.Value);
    }

    [Fact]
    public void ScalarDivisionWorks()
    {
        var scaled = length / someScalar;
        Assert.Equal(((Double)length.Value) / someScalar, scaled.Value);
    }

    [Fact]
    public void DivisionWorks()
    {
        var one = length / length;
        Assert.Equal(1d, one);
    }

    [Fact]
    public void StrictlySmallerComparesFalseWithEqualValues()
    {
        var converted = length.To(Si<Deci, Metre>());
        Assert.False(length < converted);
    }

    [Fact]
    public void StrictlySmallerComparesFalseWithSmallerValue()
    {
        Assert.False(length < length - epsilon);
    }

    [Fact]
    public void StrictlySmallerComparesTrueWithGreaterValue()
    {
        Assert.True(length < length + epsilon);
    }

    [Fact]
    public void StrictlyGreaterComparesFalseWithEqualValues()
    {
        var converted = length.To(Si<Deci, Metre>());
        Assert.False(length > converted);
    }

    [Fact]
    public void StrictlyGreaterComparesFalseWithGreaterValue()
    {
        Assert.False(length > length + epsilon);
    }

    [Fact]
    public void StrictlyGreaterComparesTrueWithSmallerValue()
    {
        Assert.True(length > length - epsilon);
    }

    [Fact]
    public void SmallerOrEqualComparesTrueWithEqualValues()
    {
        var converted = length.To(Si<Deci, Metre>());
        Assert.True(length <= converted);
    }

    [Fact]
    public void SmallerOrEqualComparesFalseWithSmallerValue()
    {
        Assert.False(length <= length - epsilon);
    }

    [Fact]
    public void SmallerOrEqualComparesTrueWithGreaterValue()
    {
        Assert.True(length <= length + epsilon);
    }

    [Fact]
    public void GreaterOrEqualComparesTrueWithEqualValues()
    {
        var converted = length.To(Si<Deci, Metre>());
        Assert.True(length >= converted);
    }

    [Fact]
    public void GreaterOrEqualComparesFalseWithGreaterValue()
    {
        Assert.False(length >= length + epsilon);
    }

    [Fact]
    public void GreaterOrEqualComparesTrueWithSomeSmallerValue()
    {
        Assert.True(length >= length - epsilon);
    }

    [Fact]
    public void EqualComparesTrueWithSameValue()
    {
        var converted = length.To(Si<Deci, Metre>());
        Assert.True(length == converted);
    }

    [Fact]
    public void EqualComparesFalseWithSmallerValue()
    {
        Assert.False(length == length - epsilon);
    }

    [Fact]
    public void EqualComparesFalseWithGreaterValue()
    {
        Assert.False(length == length + epsilon);
    }

    [Fact]
    public void NotEqualComparesFalseWithSameValue()
    {
        var converted = length.To(Si<Deci, Metre>());
        Assert.False(length != converted);
    }

    [Fact]
    public void NotEqualComparesTrueWithSmallerValue()
    {
        Assert.True(length != length - epsilon);
    }

    [Fact]
    public void NotEqualComparesTrueWithGreaterValue()
    {
        Assert.True(length != length + epsilon);
    }
}
