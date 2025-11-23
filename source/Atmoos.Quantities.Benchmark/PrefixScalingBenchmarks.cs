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

BenchmarkDotNet v0.15.7, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method              | Exponent | Mean      | Error     | Ratio |
|-------------------- |--------- |----------:|----------:|------:|
| Baseline            | -4       | 22.256 ns | 0.0200 ns |  1.00 |
| MetricPrefixScaling | -4       |  8.553 ns | 0.0860 ns |  0.38 |
| BinaryPrefixScaling | -4       |  1.010 ns | 0.0069 ns |  0.05 |
|                     |          |           |           |       |
| Baseline            | 0        | 22.258 ns | 0.0361 ns |  1.00 |
| MetricPrefixScaling | 0        |  1.949 ns | 0.0028 ns |  0.09 |
| BinaryPrefixScaling | 0        |  1.044 ns | 0.0068 ns |  0.05 |
|                     |          |           |           |       |
| Baseline            | 5        | 22.012 ns | 0.0950 ns |  1.00 |
| MetricPrefixScaling | 5        |  7.543 ns | 0.0042 ns |  0.34 |
| BinaryPrefixScaling | 5        |  6.564 ns | 0.0133 ns |  0.30 |
*/
