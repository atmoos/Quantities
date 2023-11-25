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

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method              | Exponent | Mean      | Error     | StdDev    | Ratio |
|-------------------- |--------- |----------:|----------:|----------:|------:|
| Baseline            | -4       | 25.120 ns | 0.1208 ns | 0.1071 ns |  1.00 |
| MetricPrefixScaling | -4       |  8.571 ns | 0.0295 ns | 0.0261 ns |  0.34 |
| BinaryPrefixScaling | -4       |  3.134 ns | 0.0959 ns | 0.0801 ns |  0.12 |
|                     |          |           |           |           |       |
| Baseline            | 0        | 24.874 ns | 0.4716 ns | 0.4411 ns |  1.00 |
| MetricPrefixScaling | 0        |  3.651 ns | 0.0189 ns | 0.0168 ns |  0.15 |
| BinaryPrefixScaling | 0        |  3.943 ns | 0.0242 ns | 0.0227 ns |  0.16 |
|                     |          |           |           |           |       |
| Baseline            | 5        | 24.662 ns | 0.1814 ns | 0.1417 ns |  1.00 |
| MetricPrefixScaling | 5        |  9.632 ns | 0.0318 ns | 0.0265 ns |  0.39 |
| BinaryPrefixScaling | 5        |  8.815 ns | 0.0711 ns | 0.0665 ns |  0.36 |
*/
