using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Quantities.Prefixes;

namespace Quantities.Benchmark;

public class SiPrefixScalingBenchmark
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
    public Double SiPrefixScaling() => SiPrefix.Scale(in this.value, toDouble);

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


|          Method | Exponent |      Mean |     Error |    StdDev | Ratio |
|---------------- |--------- |----------:|----------:|----------:|------:|
|        Baseline |      -20 | 25.365 ns | 0.0823 ns | 0.0729 ns |  1.00 |
| SiPrefixScaling |      -20 | 12.804 ns | 0.2343 ns | 0.2191 ns |  0.50 |
|                 |          |           |           |           |       |
|        Baseline |       -4 | 24.450 ns | 0.1296 ns | 0.1213 ns |  1.00 |
| SiPrefixScaling |       -4 | 10.208 ns | 0.0420 ns | 0.0351 ns |  0.42 |
|                 |          |           |           |           |       |
|        Baseline |        0 | 14.674 ns | 0.0476 ns | 0.0422 ns |  1.00 |
| SiPrefixScaling |        0 |  6.171 ns | 0.0270 ns | 0.0239 ns |  0.42 |
|                 |          |           |           |           |       |
|        Baseline |        5 | 23.392 ns | 0.1361 ns | 0.1206 ns |  1.00 |
| SiPrefixScaling |        5 |  8.328 ns | 0.0277 ns | 0.0245 ns |  0.36 |
|                 |          |           |           |           |       |
|        Baseline |       23 | 24.292 ns | 0.5050 ns | 0.4723 ns |  1.00 |
| SiPrefixScaling |       23 |  6.965 ns | 0.1434 ns | 0.1341 ns |  0.29 |
*/