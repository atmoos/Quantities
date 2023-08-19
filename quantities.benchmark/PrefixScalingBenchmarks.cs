using BenchmarkDotNet.Attributes;
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

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.107
  [Host]     : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2


|              Method | Exponent |      Mean |     Error |    StdDev | Ratio |
|-------------------- |--------- |----------:|----------:|----------:|------:|
|            Baseline |       -4 | 24.456 ns | 0.5156 ns | 0.5730 ns |  1.00 |
| MetricPrefixScaling |       -4 |  8.305 ns | 0.0387 ns | 0.0362 ns |  0.34 |
| BinaryPrefixScaling |       -4 |  2.945 ns | 0.0868 ns | 0.0812 ns |  0.12 |
|                     |          |           |           |           |       |
|            Baseline |        0 | 24.813 ns | 0.3937 ns | 0.3682 ns |  1.00 |
| MetricPrefixScaling |        0 |  3.659 ns | 0.0363 ns | 0.0340 ns |  0.15 |
| BinaryPrefixScaling |        0 |  2.991 ns | 0.0258 ns | 0.0241 ns |  0.12 |
|                     |          |           |           |           |       |
|            Baseline |        5 | 24.477 ns | 0.0817 ns | 0.0765 ns |  1.00 |
| MetricPrefixScaling |        5 |  8.634 ns | 0.1969 ns | 0.2418 ns |  0.35 |
| BinaryPrefixScaling |        5 |  8.616 ns | 0.0624 ns | 0.0584 ns |  0.35 |
*/
