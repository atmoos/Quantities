using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Quantities.Prefixes;

namespace Quantities.Benchmark;

public class PrefixScalingBenchmarks
{
    private static readonly Random rand = new();
    private static readonly ToDouble toDouble = new();
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

BenchmarkDotNet=v0.12.1, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.100
  [Host]     : .NET Core 7.0.0 (CoreCLR 7.0.22.56001, CoreFX 7.0.22.56001), X64 RyuJIT
  DefaultJob : .NET Core 7.0.0 (CoreCLR 7.0.22.56001, CoreFX 7.0.22.56001), X64 RyuJIT


|          Method | Exponent |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|---------------- |--------- |----------:|----------:|----------:|----------:|------:|--------:|
|        Baseline |      -20 | 24.889 ns | 0.5345 ns | 0.9774 ns | 24.363 ns |  1.00 |    0.00 |
| SiPrefixScaling |      -20 | 10.329 ns | 0.0296 ns | 0.0262 ns | 10.330 ns |  0.39 |    0.00 |
|                 |          |           |           |           |           |       |         |
|        Baseline |       -4 | 24.296 ns | 0.1143 ns | 0.1069 ns | 24.313 ns |  1.00 |    0.00 |
| SiPrefixScaling |       -4 |  9.180 ns | 0.0385 ns | 0.0341 ns |  9.178 ns |  0.38 |    0.00 |
|                 |          |           |           |           |           |       |         |
|        Baseline |        0 | 14.965 ns | 0.0466 ns | 0.0389 ns | 14.959 ns |  1.00 |    0.00 |
| SiPrefixScaling |        0 |  3.008 ns | 0.0073 ns | 0.0061 ns |  3.006 ns |  0.20 |    0.00 |
|                 |          |           |           |           |           |       |         |
|        Baseline |        5 | 24.376 ns | 0.2641 ns | 0.2471 ns | 24.264 ns |  1.00 |    0.00 |
| SiPrefixScaling |        5 |  7.807 ns | 0.0225 ns | 0.0210 ns |  7.802 ns |  0.32 |    0.00 |
|                 |          |           |           |           |           |       |         |
|        Baseline |       23 | 24.242 ns | 0.5083 ns | 0.6428 ns | 23.734 ns |  1.00 |    0.00 |
| SiPrefixScaling |       23 | 11.652 ns | 0.2653 ns | 0.2482 ns | 11.512 ns |  0.48 |    0.02 |
*/