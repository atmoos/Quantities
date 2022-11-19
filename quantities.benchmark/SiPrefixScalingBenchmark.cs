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


|          Method | Exponent |     Mean |    Error |   StdDev | Ratio | RatioSD |
|---------------- |--------- |---------:|---------:|---------:|------:|--------:|
|        Baseline |      -20 | 24.84 ns | 0.529 ns | 0.885 ns |  1.00 |    0.00 |
| SiPrefixScaling |      -20 | 24.34 ns | 0.484 ns | 0.453 ns |  0.95 |    0.02 |
|                 |          |          |          |          |       |         |
|        Baseline |       -4 | 24.92 ns | 0.074 ns | 0.062 ns |  1.00 |    0.00 |
| SiPrefixScaling |       -4 | 25.23 ns | 0.482 ns | 0.451 ns |  1.01 |    0.02 |
|                 |          |          |          |          |       |         |
|        Baseline |        0 | 24.15 ns | 0.477 ns | 0.469 ns |  1.00 |    0.00 |
| SiPrefixScaling |        0 | 21.46 ns | 0.102 ns | 0.095 ns |  0.89 |    0.02 |
|                 |          |          |          |          |       |         |
|        Baseline |        5 | 24.37 ns | 0.081 ns | 0.068 ns |  1.00 |    0.00 |
| SiPrefixScaling |        5 | 24.16 ns | 0.160 ns | 0.125 ns |  0.99 |    0.01 |
|                 |          |          |          |          |       |         |
|        Baseline |       23 | 24.27 ns | 0.382 ns | 0.357 ns |  1.00 |    0.00 |
| SiPrefixScaling |       23 | 23.84 ns | 0.036 ns | 0.030 ns |  0.98 |    0.02 |
*/