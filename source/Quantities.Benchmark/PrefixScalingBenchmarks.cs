using Quantities.Prefixes;

namespace Quantities.Benchmark;

public class PrefixScalingBenchmarks
{
    private static readonly Random rand = new();
    private static readonly IPrefixInject<Double> toDouble = new ToDouble();
    private Double value;

    [Params(-4, 0, 5)]
    public Int32 Exponent { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        this.value = rand.Next((Int32)1e5, (Int32)1e6) * Math.Pow(10, Exponent) / 1e5;
    }

    [Benchmark(Baseline = true)]
    public Double Baseline()
    {
        // Warning! This is not correct!
        // it does the minimal required amount needed to resemble scaling
        // thus serving as a reasonable baseline for proper scaling.
        var fractionalExponent = Math.Log10(this.value);
        var exp = Math.Round(fractionalExponent);
        return this.value * Math.Pow(10, exp);
    }

    [Benchmark]
    public Double MetricPrefixScaling() => MetricPrefix.Scale(in this.value, toDouble);

    [Benchmark]
    public Double BinaryPrefixScaling() => BinaryPrefix.Scale(in this.value, toDouble);

    private sealed class ToDouble : IPrefixInject<Double>
    {
        public Double Identity(in Double value) => value;

        public Double Inject<TPrefix>(in Double value) where TPrefix : IPrefix => value;
    }
}

/* Summary *

BenchmarkDotNet v0.13.12, Arch Linux ARM
Unknown processor
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD


| Method              | Exponent | Mean      | Error     | Ratio |
|-------------------- |--------- |----------:|----------:|------:|
| Baseline            | -4       | 94.290 ns | 0.0054 ns |  1.00 |
| MetricPrefixScaling | -4       | 30.571 ns | 0.0014 ns |  0.32 |
| BinaryPrefixScaling | -4       |  7.211 ns | 0.0014 ns |  0.08 |
|                     |          |           |           |       |
| Baseline            | 0        | 94.279 ns | 0.0044 ns |  1.00 |
| MetricPrefixScaling | 0        | 11.660 ns | 0.0017 ns |  0.12 |
| BinaryPrefixScaling | 0        |  7.212 ns | 0.0011 ns |  0.08 |
|                     |          |           |           |       |
| Baseline            | 5        | 94.835 ns | 0.0057 ns |  1.00 |
| MetricPrefixScaling | 5        | 30.931 ns | 0.0091 ns |  0.33 |
| BinaryPrefixScaling | 5        | 28.922 ns | 0.0035 ns |  0.30 |
*/
