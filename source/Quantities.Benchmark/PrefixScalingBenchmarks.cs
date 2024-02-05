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

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-1260P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method              | Exponent | Mean       | Error     | Ratio |
|-------------------- |--------- |-----------:|----------:|------:|
| Baseline            | -4       | 17.8220 ns | 0.2311 ns |  1.00 |
| MetricPrefixScaling | -4       |  6.4642 ns | 0.1337 ns |  0.36 |
| BinaryPrefixScaling | -4       |  0.8934 ns | 0.0389 ns |  0.05 |
|                     |          |            |           |       |
| Baseline            | 0        |  5.9589 ns | 0.0849 ns |  1.00 |
| MetricPrefixScaling | 0        |  0.7204 ns | 0.0353 ns |  0.12 |
| BinaryPrefixScaling | 0        |  0.9548 ns | 0.0397 ns |  0.16 |
|                     |          |            |           |       |
| Baseline            | 5        | 17.8225 ns | 0.1462 ns |  1.00 |
| MetricPrefixScaling | 5        |  6.8488 ns | 0.1414 ns |  0.38 |
| BinaryPrefixScaling | 5        |  6.6939 ns | 0.1130 ns |  0.38 |
*/
