using System;
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

BenchmarkDotNet=v0.13.2, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.56001), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.0 (7.0.22.56001), X64 RyuJIT AVX2


|              Method | Exponent |      Mean |     Error |    StdDev | Ratio |
|-------------------- |--------- |----------:|----------:|----------:|------:|
|            Baseline |       -4 | 24.671 ns | 0.1431 ns | 0.1195 ns |  1.00 |
| MetricPrefixScaling |       -4 |  7.808 ns | 0.0678 ns | 0.0601 ns |  0.32 |
| BinaryPrefixScaling |       -4 |  3.031 ns | 0.0333 ns | 0.0312 ns |  0.12 |
|                     |          |           |           |           |       |
|            Baseline |        0 | 23.823 ns | 0.1862 ns | 0.1651 ns |  1.00 |
| MetricPrefixScaling |        0 |  3.477 ns | 0.0996 ns | 0.1066 ns |  0.14 |
| BinaryPrefixScaling |        0 |  3.014 ns | 0.0099 ns | 0.0083 ns |  0.13 |
|                     |          |           |           |           |       |
|            Baseline |        5 | 23.781 ns | 0.0595 ns | 0.0464 ns |  1.00 |
| MetricPrefixScaling |        5 |  7.852 ns | 0.0594 ns | 0.0526 ns |  0.33 |
| BinaryPrefixScaling |        5 |  7.003 ns | 0.0221 ns | 0.0185 ns |  0.29 |
*/