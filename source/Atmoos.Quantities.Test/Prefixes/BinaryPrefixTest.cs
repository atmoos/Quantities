namespace Atmoos.Quantities.Test.Prefixes;

public class BinaryPrefixTest
{
    private static readonly IPrefixInject<Double> injector = new GetValue();

    [Theory]
    [MemberData(nameof(BinaryMaxValues))]
    public void ScaleSiPrefixes(Double valueToNormalize)
    {
        Double actualValue = BinaryPrefix.Scale(valueToNormalize, injector);

        PrecisionIsBounded(1d, actualValue);
    }

    [Theory]
    [MemberData(nameof(BinaryMaxValues))]
    public void ScalesAllValuesWithinOneAndThousand(Double value)
    {
        var seed = Math.Sqrt(2) * Math.E / (2d * Math.E);
        var range = new Double[] { (1d + Math.Pow(16, -12)) / seed, 2, 4, 5, 6, 8, 9 }
            .Select(r => seed * r)
            .ToArray();
        var inputValues = Enumerable.Range(0, 3).SelectMany(e => range.Select(r => r * value * Math.Pow(10, e))).ToArray();

        var actual = inputValues.Select(v => BinaryPrefix.Scale(v, injector)).ToArray();

        Assert.All(actual, a => Assert.InRange(a, 1d, 1000d));
    }

    [Theory]
    [MemberData(nameof(VeryLargeMaxValues))]
    public void ScaleVeryLargeValues(Double valueToNormalize)
    {
        Double expectedValue = Normalize<Yobi>(valueToNormalize);
        Double actualValue = BinaryPrefix.Scale(valueToNormalize, injector);

        Assert.Equal(expectedValue, actualValue);
    }

    public static TheoryData<Double> BinaryMaxValues() => [
        MaxValue<Yobi>(),
        MaxValue<Zebi>(),
        MaxValue<Exbi>(),
        MaxValue<Pebi>(),
        MaxValue<Tebi>(),
        MaxValue<Gibi>(),
        MaxValue<Mebi>(),
        MaxValue<Kibi>(),
        1d // unit prefix
    ];

    public static TheoryData<Double> VeryLargeMaxValues()
    {
        Double quettaMaxValue = MaxValue<Yobi>();
        return Enumerable.Range(0, 5).Select(e => Math.Pow(16, e) * quettaMaxValue).ToTheoryData();
    }

    private static Double MaxValue<TPrefix>()
        where TPrefix : IBinaryPrefix => Metrics<TPrefix>.MaxValue();

    private static Double Normalize<TPrefix>(Double value)
        where TPrefix : IBinaryPrefix => Metrics<TPrefix>.Normalize(in value);

    private sealed class GetValue : IPrefixInject<Double>
    {
        public Double Identity(in Double value) => value;

        public Double Inject<TPrefix>(in Double value)
            where TPrefix : IPrefix => value;
    }
}
