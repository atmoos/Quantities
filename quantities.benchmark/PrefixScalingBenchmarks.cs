using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Quantities.Prefixes;

namespace Quantities.Benchmark;

public class PrefixScalingBenchmarks
{
    private static readonly Random rand = new();
    private static readonly IPrefixInject<Double> toDouble = new ToDouble();
    private Double value;

    [Params(-20, -4, 0, 5, 23)]
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
    public Double SiPrefixScaling() => MetricPrefix.Scale(in this.value, toDouble);

    private sealed class ToDouble : IPrefixInject<Double>
    {
        public Double Identity(in Double value) => value;

        public Double Inject<TPrefix>(in Double value) where TPrefix : IPrefix => value;
    }

    public IEnumerable<(Int32 exp, Double scaling)> Map()
    {
        foreach (var exp in Enumerable.Range(-9, 19)) {
            yield return (exp, Math.Pow(10, -exp));
        }
    }
}

/*
// * Summary *
BenchmarkDotNet=v0.13.2, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.56001), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.0 (7.0.22.56001), X64 RyuJIT AVX2


|          Method | Exponent |      Mean |     Error |    StdDev | Ratio |
|---------------- |--------- |----------:|----------:|----------:|------:|
|        Baseline |      -20 | 24.759 ns | 0.1024 ns | 0.0799 ns |  1.00 |
| SiPrefixScaling |      -20 |  9.348 ns | 0.0531 ns | 0.0471 ns |  0.38 |
|                 |          |           |           |           |       |
|        Baseline |       -4 | 24.927 ns | 0.1148 ns | 0.1074 ns |  1.00 |
| SiPrefixScaling |       -4 |  7.765 ns | 0.1489 ns | 0.1392 ns |  0.31 |
|                 |          |           |           |           |       |
|        Baseline |        0 | 24.186 ns | 0.1129 ns | 0.1056 ns |  1.00 |
| SiPrefixScaling |        0 |  3.368 ns | 0.0240 ns | 0.0225 ns |  0.14 |
|                 |          |           |           |           |       |
|        Baseline |        5 | 24.935 ns | 0.0541 ns | 0.0506 ns |  1.00 |
| SiPrefixScaling |        5 |  7.988 ns | 0.0556 ns | 0.0520 ns |  0.32 |
|                 |          |           |           |           |       |
|        Baseline |       23 | 23.809 ns | 0.1636 ns | 0.1530 ns |  1.00 |
| SiPrefixScaling |       23 | 10.629 ns | 0.0630 ns | 0.0589 ns |  0.45 |
*/