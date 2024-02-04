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
ARMv7 Processor rev 4 (v7l), 4 logical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT


| Method              | Exponent | Mean      | Error    | Ratio |
|-------------------- |--------- |----------:|---------:|------:|
| Baseline            | -4       | 542.16 ns | 1.376 ns |  1.00 |
| MetricPrefixScaling | -4       | 239.70 ns | 0.277 ns |  0.44 |
| BinaryPrefixScaling | -4       |  51.92 ns | 0.834 ns |  0.10 |
|                     |          |           |          |       |
| Baseline            | 0        | 355.62 ns | 0.326 ns |  1.00 |
| MetricPrefixScaling | 0        |  58.75 ns | 0.502 ns |  0.17 |
| BinaryPrefixScaling | 0        |  48.05 ns | 0.625 ns |  0.14 |
|                     |          |           |          |       |
| Baseline            | 5        | 501.07 ns | 0.941 ns |  1.00 |
| MetricPrefixScaling | 5        | 252.55 ns | 1.059 ns |  0.50 |
| BinaryPrefixScaling | 5        | 237.13 ns | 0.125 ns |  0.47 |
*/
