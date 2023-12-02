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

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method              | Exponent | Mean      | Error     | StdDev    | Ratio |
|-------------------- |--------- |----------:|----------:|----------:|------:|
| Baseline            | -4       | 25.071 ns | 0.3816 ns | 0.3570 ns |  1.00 |
| MetricPrefixScaling | -4       |  8.621 ns | 0.0469 ns | 0.0366 ns |  0.34 |
| BinaryPrefixScaling | -4       |  1.395 ns | 0.0181 ns | 0.0169 ns |  0.06 |
|                     |          |           |           |           |       |
| Baseline            | 0        | 15.515 ns | 0.0676 ns | 0.0565 ns |  1.00 |
| MetricPrefixScaling | 0        |  3.065 ns | 0.0144 ns | 0.0120 ns |  0.20 |
| BinaryPrefixScaling | 0        |  1.381 ns | 0.0089 ns | 0.0083 ns |  0.09 |
|                     |          |           |           |           |       |
| Baseline            | 5        | 25.002 ns | 0.0715 ns | 0.0668 ns |  1.00 |
| MetricPrefixScaling | 5        |  7.801 ns | 0.0368 ns | 0.0327 ns |  0.31 |
| BinaryPrefixScaling | 5        |  7.296 ns | 0.0302 ns | 0.0268 ns |  0.29 |
*/
