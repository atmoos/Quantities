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
        var range = new Double[] { (1d + Math.Pow(10, -12)) / seed, 2, 4, 5, 6, 8, 9 }.Select(r => seed * r).ToArray();
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

    public static IEnumerable<Object[]> SiMaxValues()
    {
        return AllSiMaxValues().Select(i => new Object[] { i });
    }

    public static IEnumerable<Object[]> LargePowerThreeMaxValues()
    {
        Double kiloMaxValue = MaxValue<Kilo>();
        return AllSiMaxValues().Where(e => e >= kiloMaxValue).Select(i => new Object[] { i });
    }
    public static IEnumerable<Object[]> SmallPowerThreeMaxValues()
    {
        Double milliMaxValue = MaxValue<Milli>();
        return AllSiMaxValues().Where(e => e < milliMaxValue).Select(i => new Object[] { i });
    }

    public static IEnumerable<Object[]> VeryLargeMaxValues()
    {
        Double quettaMaxValue = MaxValue<Quetta>();
        return Enumerable.Range(0, 5).Select(e => new Object[] { Math.Pow(10, e) * quettaMaxValue });
    }

    public static IEnumerable<Object[]> VerySmallMaxValues()
    {
        Double quectoMaxValue = MaxValue<Quecto>();
        return Enumerable.Range(0, 5).Select(e => new Object[] { quectoMaxValue / Math.Pow(10, e) });
    }

    private static IEnumerable<Double> AllSiMaxValues()
    {
        yield return MaxValue<Quetta>();
        yield return MaxValue<Ronna>();
        yield return MaxValue<Yotta>();
        yield return MaxValue<Zetta>();
        yield return MaxValue<Exa>();
        yield return MaxValue<Peta>();
        yield return MaxValue<Tera>();
        yield return MaxValue<Giga>();
        yield return MaxValue<Mega>();
        yield return MaxValue<Kilo>();
        yield return MaxValue<Hecto>();
        yield return MaxValue<Deca>();
        yield return 1d; // unit prefix
        yield return MaxValue<Deci>();
        yield return MaxValue<Centi>();
        yield return MaxValue<Milli>();
        yield return MaxValue<Micro>();
        yield return MaxValue<Nano>();
        yield return MaxValue<Pico>();
        yield return MaxValue<Femto>();
        yield return MaxValue<Atto>();
        yield return MaxValue<Zepto>();
        yield return MaxValue<Yocto>();
        yield return MaxValue<Ronto>();
        yield return MaxValue<Quecto>();
    }

    private static Double MaxValue<TPrefix>()
        where TPrefix : IMetricPrefix => Metrics<TPrefix>.MaxValue();
    private static Double Normalize<TPrefix>(Double value)
        where TPrefix : IMetricPrefix => Metrics<TPrefix>.Normalize(in value);

    private sealed class GetValue : IPrefixInject<Double>
    {
        public Double Identity(in Double value) => value;
        public Double Inject<TPrefix>(in Double value) where TPrefix : IPrefix => value;
    }
}
