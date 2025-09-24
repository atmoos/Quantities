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

BenchmarkDotNet v0.15.3, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method              | Exponent | Mean      | Error     | Ratio |
|-------------------- |--------- |----------:|----------:|------:|
| Baseline            | -4       | 56.837 ns | 0.2542 ns |  1.00 |
| MetricPrefixScaling | -4       | 42.279 ns | 0.1829 ns |  0.74 |
| BinaryPrefixScaling | -4       |  8.124 ns | 0.0447 ns |  0.14 |
|                     |          |           |           |       |
| Baseline            | 0        | 57.987 ns | 0.2911 ns |  1.00 |
| MetricPrefixScaling | 0        |  8.917 ns | 0.0913 ns |  0.15 |
| BinaryPrefixScaling | 0        |  8.112 ns | 0.0500 ns |  0.14 |
|                     |          |           |           |       |
| Baseline            | 5        | 56.693 ns | 0.2004 ns |  1.00 |
| MetricPrefixScaling | 5        | 43.606 ns | 0.1711 ns |  0.77 |
| BinaryPrefixScaling | 5        | 40.364 ns | 0.1401 ns |  0.71 |
*/
