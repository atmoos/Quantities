using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Measures;

namespace Atmoos.Quantities.Test.Creation;

public sealed class FactoryTest
{
    [Fact]
    public void DivisionInvertsMeasureByInvertingIt()
    {
        var nominator = Factory.Of<Si<Kilo, Metre>>();
        var denominator = Factory.Of<Quantities.Measures.Power<Si<Second>, Two>>();
        var expected = Factory.Of<Quantities.Measures.Product<Si<Kilo, Metre>, Quantities.Measures.Power<Si<Second>, Negative<Two>>>>();

        var actual = nominator.Divide(denominator);

        Assert.Same(expected, actual);
    }

    [Fact]
    public void MultiplicationRetainsOrder()
    {
        var left = Factory.Of<Si<Kilo, Metre>>();
        var right = Factory.Of<Si<Second>>();
        var expected = Factory.Of<Quantities.Measures.Product<Si<Kilo, Metre>, Si<Second>>>();

        var actual = left.Multiply(right);

        Assert.Same(expected, actual);
    }
}
