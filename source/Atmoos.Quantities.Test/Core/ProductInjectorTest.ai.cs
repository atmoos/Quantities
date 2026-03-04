using Atmoos.Quantities.Core.Construction;
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Serialization;
using Atmoos.Quantities.Units.Si;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Core;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public sealed class ProductInjectorTest
{
    [Fact]
    public void LeftTermBuildReturnsTheInjectedScalarMeasure()
    {
        Double value = 12.5;
        IInject<IBuilder> injector = new ProductInjector(1, 1);
        IBuilder left = injector.Inject<Si<Metre>>();
        Quantity expected = Quantity.Of<Si<Metre>>(in value);

        Quantity actual = left.Build(in value);

        Assert.True(actual.EqualsExactly(expected));
    }

    [Fact]
    public void PositivePositiveExponentsCreateDirectProduct()
    {
        AssertProductBuild(1, 1, static value => Quantity.Of<Product<Si<Metre>, Metric<Hour>>>(value));
    }

    [Fact]
    public void PositiveNegativeExponentsCreateRightInverseProduct()
    {
        AssertProductBuild(1, -1, static value => Quantity.Of<Product<Si<Metre>, Power<Metric<Hour>, Negative<One>>>>(value));
    }

    [Fact]
    public void NegativePositiveExponentsCreateLeftInverseProduct()
    {
        AssertProductBuild(-1, 1, static value => Quantity.Of<Product<Power<Si<Metre>, Negative<One>>, Metric<Hour>>>(value));
    }

    [Fact]
    public void NegativeNegativeExponentsCreateInverseOfProduct()
    {
        AssertProductBuild(-1, -1, static value => Quantity.Of<Power<Product<Si<Metre>, Metric<Hour>>, Negative<One>>>(value));
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(0, 0)]
    public void ZeroExponentCombinationsAreRejected(Int32 leftExponent, Int32 rightExponent)
    {
        IInject<IBuilder> injector = new ProductInjector(leftExponent, rightExponent);
        IBuilder left = injector.Inject<Si<Metre>>();
        IInject<IBuilder> right = Assert.IsAssignableFrom<IInject<IBuilder>>(left);

        Assert.Throws<NotSupportedException>(() => right.Inject<Metric<Hour>>());
    }

    private static void AssertProductBuild(Int32 leftExponent, Int32 rightExponent, Func<Double, Quantity> expected)
    {
        Double value = 3.75;
        IInject<IBuilder> injector = new ProductInjector(leftExponent, rightExponent);
        IBuilder left = injector.Inject<Si<Metre>>();
        IInject<IBuilder> right = Assert.IsAssignableFrom<IInject<IBuilder>>(left);
        IBuilder product = right.Inject<Metric<Hour>>();

        Quantity actual = product.Build(in value);

        Assert.True(actual.EqualsExactly(expected(value)));
    }
}
