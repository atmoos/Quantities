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

BenchmarkDotNet v0.15.2, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.109
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


| Method              | Exponent | Mean      | Error     | Ratio |
|-------------------- |--------- |----------:|----------:|------:|
| Baseline            | -4       | 58.282 ns | 0.3611 ns |  1.00 |
| MetricPrefixScaling | -4       | 43.596 ns | 0.2668 ns |  0.75 |
| BinaryPrefixScaling | -4       |  8.161 ns | 0.0853 ns |  0.14 |
|                     |          |           |           |       |
| Baseline            | 0        | 56.973 ns | 0.2776 ns |  1.00 |
| MetricPrefixScaling | 0        |  8.958 ns | 0.0634 ns |  0.16 |
| BinaryPrefixScaling | 0        |  8.206 ns | 0.0536 ns |  0.14 |
|                     |          |           |           |       |
| Baseline            | 5        | 57.048 ns | 0.3186 ns |  1.00 |
| MetricPrefixScaling | 5        | 44.039 ns | 0.1538 ns |  0.77 |
| BinaryPrefixScaling | 5        | 41.386 ns | 0.1237 ns |  0.73 |
*/
