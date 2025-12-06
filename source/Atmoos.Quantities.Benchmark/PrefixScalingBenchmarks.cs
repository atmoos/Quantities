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

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method              | Exponent | Mean       | Error     | Ratio |
|-------------------- |--------- |-----------:|----------:|------:|
| Baseline            | -4       | 22.3660 ns | 0.1413 ns |  1.00 |
| MetricPrefixScaling | -4       |  8.2526 ns | 0.0583 ns |  0.37 |
| BinaryPrefixScaling | -4       |  0.8026 ns | 0.0052 ns |  0.04 |
|                     |          |            |           |       |
| Baseline            | 0        | 23.1256 ns | 0.0690 ns |  1.00 |
| MetricPrefixScaling | 0        |  1.1907 ns | 0.0036 ns |  0.05 |
| BinaryPrefixScaling | 0        |  0.7663 ns | 0.0217 ns |  0.03 |
|                     |          |            |           |       |
| Baseline            | 5        | 25.4604 ns | 0.1158 ns |  1.00 |
| MetricPrefixScaling | 5        |  7.2370 ns | 0.0150 ns |  0.28 |
| BinaryPrefixScaling | 5        |  6.5000 ns | 0.0049 ns |  0.26 |
*/
