using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Measures;

public class MeasureDivisionTest
{
    [Fact]
    public void IdentityPerAnyIsInverseAny()
    {
        var conversion = Expect<Power<Si<Micro, Second>, Negative<One>>>.ToBeQuotientOf<Identity, Si<Micro, Second>>();
        Assert.Equal(Polynomial.One, conversion);
    }
    [Fact]
    public void AnyPerIdentityIsAny()
    {
        var conversion = Expect<Si<Pico, Second>>.ToBeQuotientOf<Si<Pico, Second>, Identity>();
        Assert.Equal(Polynomial.One, conversion);
    }
    [Fact]
    public void ScalarPerOtherScalarIsQuotient()
    {
        var conversion = Expect<Product<Si<Metre>, Power<Si<Second>, Negative<One>>>>.ToBeQuotientOf<Si<Metre>, Si<Second>>();
        Assert.Equal(Polynomial.One, conversion);
    }
    [Fact]
    public void ScalarPerSameScalarIsIdentity()
    {
        var conversion = Expect<Identity>.ToBeQuotientOf<Metric<Hour>, Si<Kilo, Second>>();
        Assert.Equal((Polynomial.Of<Hour>() / Polynomial.Of<Kilo>()).Simplify(), conversion);
    }
    [Fact]
    public void SquareScalarPerSimilarScalarIsScalar()
    {
        var conversion = Expect<Si<Kilo, Metre>>.ToBeQuotientOf<Power<Si<Kilo, Metre>, Two>, Imperial<Foot>>();
        Assert.Equal((Polynomial.Of<Kilo>() / Polynomial.Of<Foot>()).Simplify(), conversion);
    }
    [Fact]
    public void CubicScalarPerSimilarScalarIsSquare()
    {
        var conversion = Expect<Power<Si<Kilo, Metre>, Two>>.ToBeQuotientOf<Power<Si<Kilo, Metre>, Three>, Imperial<Foot>>();
        Assert.Equal((Polynomial.Of<Kilo>() / Polynomial.Of<Foot>()).Simplify(), conversion);
    }

    [Fact]
    public void CubicScalarPerSquareScalarIsScalar()
    {
        var conversion = Expect<Si<Metre>>.ToBeQuotientOf<Power<Si<Metre>, Three>, Power<Si<Metre>, Two>>();
        Assert.Equal(Polynomial.One, conversion);
    }
}
