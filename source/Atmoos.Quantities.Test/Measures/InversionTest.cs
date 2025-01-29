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
        Expect<Inverse<Si<Kilo, Metre>>>.ToBeInverseOf<Si<Kilo, Metre>>("km⁻¹");
    }

    [Fact]
    public void MetricInvertedIsInverseOfMetric()
    {
        Expect<Inverse<Metric<Milli, Hour>>>.ToBeInverseOf<Metric<Milli, Hour>>($"m{Hour.Representation}⁻¹");
    }

    [Fact]
    public void ImperialInvertedIsInverseOfImperial()
    {
        Expect<Inverse<Imperial<Foot>>>.ToBeInverseOf<Imperial<Foot>>("ft⁻¹");
    }

    [Fact]
    public void NonStandardInvertedIsInverseOfNonStandard()
    {
        Expect<Inverse<NonStandard<SomeNonStandardUnit>>>.ToBeInverseOf<NonStandard<SomeNonStandardUnit>>("snu⁻¹");
    }

    [Fact]
    public void ProductInvertedIsProductOfInverses()
    {
        Expect<Product<Inverse<Si<Metre>>, Inverse<Metric<Hour>>>>.ToBeInverseOf<Product<Si<Metre>, Metric<Hour>>>($"m⁻¹{zeroWidthNonJoiner}h⁻¹");
    }
    [Fact]
    public void QuotientInvertedIsInvertedQuotient()
    {
        Expect<Product<Inverse<Si<Metre>>, Metric<Hour>>>.ToBeInverseOf<Product<Si<Metre>, Inverse<Metric<Hour>>>>("h/m");
    }
}

file readonly struct SomeNonStandardUnit : INonStandardUnit, IVelocity
{
    public static Transformation ToSi(Transformation self) => self * 1.234;
    public static String Representation => "snu";
}
