using Atmoos.Quantities.Prefixes;

namespace Atmoos.Quantities.Benchmark;

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

/* Summary

BenchmarkDotNet v0.15.4, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 4.00GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method              | Exponent | Mean      | Error     | Ratio |
|-------------------- |--------- |----------:|----------:|------:|
| Baseline            | -4       | 56.094 ns | 0.2551 ns |  1.00 |
| MetricPrefixScaling | -4       | 42.126 ns | 0.1336 ns |  0.75 |
| BinaryPrefixScaling | -4       |  8.088 ns | 0.0442 ns |  0.14 |
|                     |          |           |           |       |
| Baseline            | 0        | 49.118 ns | 0.2269 ns |  1.00 |
| MetricPrefixScaling | 0        |  8.861 ns | 0.0656 ns |  0.18 |
| BinaryPrefixScaling | 0        |  8.089 ns | 0.0450 ns |  0.16 |
|                     |          |           |           |       |
| Baseline            | 5        | 56.154 ns | 0.2031 ns |  1.00 |
| MetricPrefixScaling | 5        | 41.835 ns | 0.1193 ns |  0.75 |
| BinaryPrefixScaling | 5        | 40.123 ns | 0.1241 ns |  0.71 |
*/
