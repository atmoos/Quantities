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

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method              | Exponent | Mean      | Error     | Ratio |
|-------------------- |--------- |----------:|----------:|------:|
| Baseline            | -4       | 24.916 ns | 0.0351 ns |  1.00 |
| MetricPrefixScaling | -4       |  8.622 ns | 0.0429 ns |  0.35 |
| BinaryPrefixScaling | -4       |  1.523 ns | 0.0056 ns |  0.06 |
|                     |          |           |           |       |
| Baseline            | 0        | 24.313 ns | 0.0416 ns |  1.00 |
| MetricPrefixScaling | 0        |  2.939 ns | 0.0047 ns |  0.12 |
| BinaryPrefixScaling | 0        |  1.180 ns | 0.0433 ns |  0.05 |
|                     |          |           |           |       |
| Baseline            | 5        | 25.786 ns | 0.0526 ns |  1.00 |
| MetricPrefixScaling | 5        |  8.173 ns | 0.0459 ns |  0.32 |
| BinaryPrefixScaling | 5        |  7.491 ns | 0.0157 ns |  0.29 |
*/
