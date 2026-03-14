namespace Atmoos.Quantities.Test.Prefixes;

public class MetricPrefixTest
{
    private const Int32 precision = 12;
    private static readonly IPrefixInject<Double> injector = new GetValue();

    [Theory]
    [InlineData(1.2)]
    [InlineData(-12)]
    [InlineData(968)]
    public void ScaleThreeSkipsExponentsSmallerThanKilo(Double value)
    {
        Double actualValue = MetricPrefix.ScaleThree(value, injector);
        Assert.Equal(value, actualValue);
    }

    [Theory]
    [InlineData(0.2231)]
    [InlineData(-0.0582349)]
    [InlineData(-0.0023402173071)]
    public void ScaleThreeSkipsExponentsLargerThanMilli(Double value)
    {
        // Everything usually scaled to deci ot centi should be milli!
        Double actualValue = MetricPrefix.ScaleThree(value, injector);
        Assert.Equal(value * 1e3, actualValue);
    }

    [Theory]
    [MemberData(nameof(SiMaxValues))]
    public void ScaleSiPrefixes(Double valueToNormalize)
    {
        Double actualValue = MetricPrefix.Scale(valueToNormalize, injector);

        Assert.Equal(1d, actualValue, MediumPrecision);
    }

    [Theory]
    [MemberData(nameof(SiMaxValues))]
    public void ScalesAllValuesWithinOneAndThousand(Double value)
    {
        var seed = Math.Sqrt(2) * Math.E / (2d * Math.E);
        var range = new Double[] { (1d + Math.Pow(10, -12)) / seed, 2, 4, 5, 6, 8, 9 }
            .Select(r => seed * r)
            .ToArray();
        var inputValues = Enumerable.Range(0, 3).SelectMany(e => range.Select(r => r * value * Math.Pow(10, e))).ToArray();

        var actual = inputValues.Select(v => MetricPrefix.Scale(v, injector)).ToArray();

        Assert.All(actual, a => Assert.InRange(a, 1d, 1000d));
    }

    [Theory]
    [MemberData(nameof(SmallPowerThreeMaxValues))]
    public void ScaleSmallPowerOfThreePrefixes(Double value)
    {
        Double[] expectedValues = new[] { -3.8, 78, -647 };
        var valuesToNormalize = expectedValues.Select(e => value * e).ToArray();
        var actualValues = valuesToNormalize.Select(v => MetricPrefix.Scale(v, injector)).ToArray();

        Assert.All(expectedValues.Zip(actualValues, (e, a) => (e, a)), c => Assert.Equal(c.e, c.a, precision));
    }

    [Theory]
    [MemberData(nameof(LargePowerThreeMaxValues))]
    public void ScaleLargePowerOfThreePrefixes(Double value)
    {
        Double[] expectedValues = new[] { 1.2, -18, 243 };
        var valuesToNormalize = expectedValues.Select(e => value * e).ToArray();
        var actualValues = valuesToNormalize.Select(v => MetricPrefix.Scale(v, injector)).ToArray();

        Assert.All(expectedValues.Zip(actualValues, (e, a) => (e, a)), c => Assert.Equal(c.e, c.a));
    }

    [Theory]
    [MemberData(nameof(VerySmallMaxValues))]
    public void ScaleVerySmallValues(Double valueToNormalize)
    {
        Double expectedValue = Normalize<Quecto>(valueToNormalize);
        Double actualValue = MetricPrefix.Scale(valueToNormalize, injector);

        Assert.Equal(expectedValue, actualValue);
    }

    [Theory]
    [MemberData(nameof(VeryLargeMaxValues))]
    public void ScaleVeryLargeValues(Double valueToNormalize)
    {
        Double expectedValue = Normalize<Quetta>(valueToNormalize);
        Double actualValue = MetricPrefix.Scale(valueToNormalize, injector);

        Assert.Equal(expectedValue, actualValue);
    }

    public static TheoryData<Double> LargePowerThreeMaxValues()
    {
        Double kiloMaxValue = MaxValue<Kilo>();
        return ((IEnumerable<Double>)SiMaxValues()).Where(e => e >= kiloMaxValue).ToTheoryData();
    }

    public static TheoryData<Double> SmallPowerThreeMaxValues()
    {
        Double milliMaxValue = MaxValue<Milli>();
        return ((IEnumerable<Double>)SiMaxValues()).Where(e => e < milliMaxValue).ToTheoryData();
    }

    public static TheoryData<Double> VeryLargeMaxValues()
    {
        Double quettaMaxValue = MaxValue<Quetta>();
        return Enumerable.Range(0, 5).Select(e => Math.Pow(10, e) * quettaMaxValue).ToTheoryData();
    }

    public static TheoryData<Double> VerySmallMaxValues()
    {
        Double quectoMaxValue = MaxValue<Quecto>();
        return Enumerable.Range(0, 5).Select(e => quectoMaxValue / Math.Pow(10, e)).ToTheoryData();
    }

    public static TheoryData<Double> SiMaxValues() => [
        MaxValue<Quetta>(),
        MaxValue<Ronna>(),
        MaxValue<Yotta>(),
        MaxValue<Zetta>(),
        MaxValue<Exa>(),
        MaxValue<Peta>(),
        MaxValue<Tera>(),
        MaxValue<Giga>(),
        MaxValue<Mega>(),
        MaxValue<Kilo>(),
        MaxValue<Hecto>(),
        MaxValue<Deca>(),
        1d, // unit prefix
        MaxValue<Deci>(),
        MaxValue<Centi>(),
        MaxValue<Milli>(),
        MaxValue<Micro>(),
        MaxValue<Nano>(),
        MaxValue<Pico>(),
        MaxValue<Femto>(),
        MaxValue<Atto>(),
        MaxValue<Zepto>(),
        MaxValue<Yocto>(),
        MaxValue<Ronto>(),
        MaxValue<Quecto>()
    ];

    private static Double MaxValue<TPrefix>()
        where TPrefix : IMetricPrefix => Metrics<TPrefix>.MaxValue();

    private static Double Normalize<TPrefix>(Double value)
        where TPrefix : IMetricPrefix => Metrics<TPrefix>.Normalize(in value);

    private sealed class GetValue : IPrefixInject<Double>
    {
        public Double Identity(in Double value) => value;

        public Double Inject<TPrefix>(in Double value)
            where TPrefix : IPrefix => value;
    }
}
