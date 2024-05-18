using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units.Si.Metric;

using static Atmoos.Quantities.Core.Numerics.Polynomial;

namespace Atmoos.Quantities.Test.Measures;

public class MeasureDivisionTest
{
    [Fact]
    public void IdentityPerAnyIsInverseAny()
    {
        var conversion = Expect<Inverse<Si<Micro, Second>>>.ToBeQuotientOf<Identity, Si<Micro, Second>>();
        Assert.Equal(One, conversion);
    }
    [Fact]
    public void AnyPerIdentityIsAny()
    {
        var conversion = Expect<Si<Pico, Second>>.ToBeQuotientOf<Si<Pico, Second>, Identity>();
        Assert.Equal(One, conversion);
    }
    [Fact]
    public void ScalarPerOtherScalarIsQuotient()
    {
        var conversion = Expect<Quotient<Si<Metre>, Si<Second>>>.ToBeQuotientOf<Si<Metre>, Si<Second>>();
        Assert.Equal(One, conversion);
    }
    [Fact]
    public void ScalarPerSameScalarIsIdentity()
    {
        var conversion = Expect<Identity>.ToBeQuotientOf<Metric<Hour>, Si<Kilo, Second>>();
        Assert.Equal((Of<Hour>() / Of<Kilo>()).Simplify(), conversion);
    }

    [Fact]
    public void SquareScalarPerSimilarScalarIsScalar()
    {
        var conversion = Expect<Si<Kilo, Metre>>.ToBeQuotientOf<Power<Square, Si<Kilo, Metre>>, Imperial<Foot>>();
        Assert.Equal((Of<Kilo>() / Of<Foot>()).Simplify(), conversion);
    }
    [Fact]
    public void CubicScalarPerSimilarScalarIsSquare()
    {
        var conversion = Expect<Power<Square, Si<Kilo, Metre>>>.ToBeQuotientOf<Power<Cubic, Si<Kilo, Metre>>, Imperial<Foot>>();
        Assert.Equal((Of<Kilo>() / Of<Foot>()).Simplify(), conversion);
    }

    [Fact]
    public void CubicScalarPerSquareScalarIsScalar()
    {
        var conversion = Expect<Si<Metre>>.ToBeQuotientOf<Power<Cubic, Si<Metre>>, Power<Square, Si<Metre>>>();
        Assert.Equal(One, conversion);
    }
}
