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


| Method              | Exponent | Mean      | Error     | Ratio |
|-------------------- |--------- |----------:|----------:|------:|
| Baseline            | -4       | 25.017 ns | 0.0783 ns |  1.00 |
| MetricPrefixScaling | -4       |  9.181 ns | 0.0152 ns |  0.37 |
| BinaryPrefixScaling | -4       |  1.181 ns | 0.0191 ns |  0.05 |
|                     |          |           |           |       |
| Baseline            | 0        | 25.581 ns | 0.0561 ns |  1.00 |
| MetricPrefixScaling | 0        |  3.181 ns | 0.0229 ns |  0.12 |
| BinaryPrefixScaling | 0        |  1.372 ns | 0.0596 ns |  0.05 |
|                     |          |           |           |       |
| Baseline            | 5        | 24.651 ns | 0.4814 ns |  1.00 |
| MetricPrefixScaling | 5        |  7.885 ns | 0.0216 ns |  0.32 |
| BinaryPrefixScaling | 5        |  6.984 ns | 0.0732 ns |  0.28 |
*/
