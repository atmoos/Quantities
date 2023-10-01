using BenchmarkDotNet.Attributes;
using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser]
public class ComplexCreation
{
    private static readonly Double value = Math.E;

    [Benchmark(Baseline = true)]
    public Volume CreateQuantity() => Volume.Of(value).Cubic.Si<Centi, Metre>();

    [Benchmark]
    public UnitsNet.Volume CreateUnitsNet() => UnitsNet.Volume.FromCubicCentimeters(value);

    [Benchmark]
    public Volume CreateQuantityCentiLitre() => Volume.Of(value).Metric<Centi, Litre>();

    [Benchmark]
    public UnitsNet.Volume CreateUnitsNetCentiLitre() => UnitsNet.Volume.FromCentiliters(value);
}
/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method                   | Mean     | Error    | StdDev   | Ratio | Allocated | Alloc Ratio |
|------------------------- |---------:|---------:|---------:|------:|----------:|------------:|
| CreateQuantity           | 23.63 ns | 0.247 ns | 0.231 ns |  1.00 |         - |          NA |
| CreateUnitsNet           | 12.92 ns | 0.044 ns | 0.041 ns |  0.55 |         - |          NA |
| CreateQuantityCentiLitre | 27.19 ns | 0.068 ns | 0.064 ns |  1.15 |         - |          NA |
| CreateUnitsNetCentiLitre | 12.66 ns | 0.095 ns | 0.089 ns |  0.54 |         - |          NA |
*/
