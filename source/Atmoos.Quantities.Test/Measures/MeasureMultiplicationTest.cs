using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units;
using Atmoos.Quantities.Units.Si.Metric;

using static Atmoos.Quantities.Core.Numerics.Polynomial;

namespace Atmoos.Quantities.Test.Measures;

public class MeasureMultiplicationTest
{
    [Fact]
    public void IdentityTimesAnyIsAny()
    {
        var conversion = Expect<Si<Micro, Second>>.ToBeProductOf<Identity, Si<Micro, Second>>();
        Assert.Equal(One, conversion);
    }
    [Fact]
    public void AnyTimesIdentityIsAny()
    {
        var conversion = Expect<Si<Pico, Second>>.ToBeProductOf<Si<Pico, Second>, Identity>();
        Assert.Equal(One, conversion);
    }
    [Fact]
    public void ScalarTimesOtherScalarIsProduct()
    {
        var conversion = Expect<Product<Si<Metre>, Si<Second>>>.ToBeProductOf<Si<Metre>, Si<Second>>();
        Assert.Equal(One, conversion);
    }

    [Fact]
    public void ScalarTimesSimilarScalarIsSquareScalar()
    {
        var conversion = Expect<Power<Square, Si<Metre>>>.ToBeProductOf<Si<Metre>, Imperial<Foot>>();
        Assert.Equal(Of<Foot>(), conversion);
    }
    [Fact]
    public void ScalarTimesSquareSimilarScalarIsCubicScalar()
    {
        var conversion = Expect<Power<Cubic, Metric<Hour>>>.ToBeProductOf<Metric<Hour>, Power<Square, Metric<Minute>>>();
        Assert.Equal((Of<Minute>().Pow(2) / Of<Hour>()).Simplify(), conversion);
    }
    [Fact]
    public void ProductOfMeasureTimesItsInverseIsIdentity()
    {
        var conversion = Expect<Identity>.ToBeProductOf<Metric<Hour>, Si<InverseTime>>();
        Assert.Equal(Of<Hour>().Simplify(), conversion);
    }
}

file readonly struct InverseTime : ISiUnit, IFrequency, IInvertible<ITime>
{
    public static Transformation ToSi(Transformation self) => self;
    static T ISystemInject<ITime>.Inject<T>(ISystems<ITime, T> basis) => basis.Si<Second>();
    public static String Representation => "s⁻¹";
}
