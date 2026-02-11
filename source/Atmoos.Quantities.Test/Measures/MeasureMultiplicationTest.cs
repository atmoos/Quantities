using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Measures;

public class MeasureMultiplicationTest
{
    [Fact]
    public void IdentityTimesAnyIsAny()
    {
        var conversion = Expect<Si<Micro, Second>>.ToBeProductOf<Identity, Si<Micro, Second>>();
        Assert.Equal(Polynomial.One, conversion);
    }

    [Fact]
    public void AnyTimesIdentityIsAny()
    {
        var conversion = Expect<Si<Pico, Second>>.ToBeProductOf<Si<Pico, Second>, Identity>();
        Assert.Equal(Polynomial.One, conversion);
    }

    [Fact]
    public void ScalarTimesOtherScalarIsProduct()
    {
        var conversion = Expect<Product<Si<Metre>, Si<Second>>>.ToBeProductOf<Si<Metre>, Si<Second>>();
        Assert.Equal(Polynomial.One, conversion);
    }

    [Fact]
    public void ScalarTimesSimilarScalarIsSquareScalar()
    {
        var conversion = Expect<Power<Si<Metre>, Two>>.ToBeProductOf<Si<Metre>, Imperial<Foot>>();
        Assert.Equal(Polynomial.Of<Foot>(), conversion);
    }

    [Fact]
    public void ScalarTimesSquareSimilarScalarIsCubicScalar()
    {
        var conversion = Expect<Power<Metric<Hour>, Three>>.ToBeProductOf<Metric<Hour>, Power<Metric<Minute>, Two>>();
        Assert.Equal(Polynomial.Of<Minute>().Pow(-2).Simplify(), conversion);
    }

    [Fact]
    public void ProductOfMeasureTimesItsInverseIsIdentity()
    {
        var conversion = Expect<Identity>.ToBeProductOf<Metric<Hour>, Si<InverseTime>>();
        Assert.Equal(Polynomial.Of<Hour>().Simplify(), conversion);
    }
}

file readonly struct InverseTime : ISiUnit, IFrequency, IInvertible<ITime>
{
    public static Transformation ToSi(Transformation self) => self;

    static T ISystemInject<ITime>.Inject<T>(ISystems<ITime, T> basis) => basis.Si<Second>();

    public static String Representation => "s⁻¹";
}
