using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Measures;

public class InversionTest
{
    private const String zeroWidthNonJoiner = "\u200C";
    [Fact]
    public void SiInvertedIsInverseOfSi()
    {
        Expect<Power<Si<Kilo, Metre>, Negative<One>>>.ToBeInverseOf<Si<Kilo, Metre>>("km⁻¹");
    }
    [Fact]
    public void InverseOfInvertedSiIsSi()
    {
        Expect<Si<Metre>>.ToBeInverseOf<Power<Si<Metre>, Negative<One>>>("m");
    }
    [Fact]
    public void MetricInvertedIsInverseOfMetric()
    {
        Expect<Power<Metric<Milli, Hour>, Negative<One>>>.ToBeInverseOf<Metric<Milli, Hour>>($"m{Hour.Representation}⁻¹");
    }

    [Fact]
    public void ImperialInvertedIsInverseOfImperial()
    {
        Expect<Power<Imperial<Foot>, Negative<One>>>.ToBeInverseOf<Imperial<Foot>>("ft⁻¹");
    }

    [Fact]
    public void NonStandardInvertedIsInverseOfNonStandard()
    {
        Expect<Power<NonStandard<SomeNonStandardUnit>, Negative<One>>>.ToBeInverseOf<NonStandard<SomeNonStandardUnit>>("snu⁻¹");
    }

    [Fact]
    public void ProductInvertedIsProductOfInverses()
    {
        Expect<Product<Power<Si<Metre>, Negative<One>>, Power<Metric<Hour>, Negative<One>>>>.ToBeInverseOf<Product<Si<Metre>, Metric<Hour>>>($"m⁻¹{zeroWidthNonJoiner}h⁻¹");
    }
    [Fact]
    public void QuotientInvertedIsInvertedQuotient()
    {
        Expect<Product<Metric<Hour>, Power<Si<Metre>, Negative<One>>>>.ToBeInverseOf<Product<Si<Metre>, Power<Metric<Hour>, Negative<One>>>>("h/m");
    }
}

file readonly struct SomeNonStandardUnit : INonStandardUnit, IVelocity
{
    public static Transformation ToSi(Transformation self) => self * 1.234;
    public static String Representation => "snu";
}
