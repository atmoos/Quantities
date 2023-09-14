using Quantities.Measures;
using Quantities.Units.Si.Metric;

using static Quantities.Numerics.Polynomial;

namespace Quantities.Test.Measures;

public class MeasureDivisionTest
{
    [Fact]
    public void IdentityPerAnyIsInverseAny()
    {
        var conversion = Expect<Quotient<Identity, Si<Micro, Second>>>.IsQuotientOf<Identity, Si<Micro, Second>>();
        Assert.Equal(One / Of<Micro>(), conversion);
    }
    [Fact]
    public void AnyPerIdentityIsAny()
    {
        var conversion = Expect<Si<Pico, Second>>.IsQuotientOf<Si<Pico, Second>, Identity>();
        Assert.Equal(One, conversion);
    }
    [Fact]
    public void ScalarPerOtherScalarIsQuotient()
    {
        var conversion = Expect<Quotient<Si<Metre>, Si<Second>>>.IsQuotientOf<Si<Metre>, Si<Second>>();
        Assert.Equal(One, conversion);
    }
    [Fact]
    public void ScalarPerSameScalarIsIdentity()
    {
        var conversion = Expect<Identity>.IsQuotientOf<Metric<Hour>, Si<Kilo, Second>>();
        Assert.Equal(Of<Hour>() / Of<Kilo>(), conversion);
    }

    [Fact]
    public void SquareScalarPerSimilarScalarIsScalar()
    {
        var conversion = Expect<Si<Kilo, Metre>>.IsQuotientOf<Power<Square, Si<Kilo, Metre>>, Imperial<Yard>>();
        Assert.Equal(Of<Kilo>() / Of<Yard>(), conversion);
    }
    [Fact]
    public void CubicScalarPerSimilarScalarIsSquare()
    {
        var conversion = Expect<Power<Square, Si<Kilo, Metre>>>.IsQuotientOf<Power<Cubic, Si<Kilo, Metre>>, Imperial<Foot>>();
        Assert.Equal(Of<Kilo>() / Of<Foot>(), conversion);
    }

    [Fact]
    public void CubicScalarPerSquareScalarIsScalar()
    {
        var conversion = Expect<Si<Metre>>.IsQuotientOf<Power<Cubic, Si<Metre>>, Power<Square, Si<Metre>>>();
        Assert.Equal(One, conversion);
    }
}
