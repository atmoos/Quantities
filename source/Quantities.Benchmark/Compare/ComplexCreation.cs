using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser]
public class ComplexCreation
{
    private static readonly Double value = Math.E;

    [Benchmark(Baseline = true)]
    public Volume CreateQuantity() => Volume.Of(value, Cubic(Si<Centi, Metre>()));

    [Benchmark]
    public UnitsNet.Volume CreateUnitsNet() => UnitsNet.Volume.FromCubicCentimeters(value);

    [Benchmark]
    public Volume CreateQuantityCentiLitre() => Volume.Of(value, Metric<Centi, Litre>());

    [Benchmark]
    public UnitsNet.Volume CreateUnitsNetCentiLitre() => UnitsNet.Volume.FromCentiliters(value);
}
/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.113
  [Host]     : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2


| Method                   | Mean      | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------- |----------:|----------:|----------:|------:|--------:|----------:|------------:|
| CreateQuantity           |  5.076 ns | 0.0454 ns | 0.0425 ns |  1.00 |    0.00 |         - |          NA |
| CreateUnitsNet           | 13.141 ns | 0.0400 ns | 0.0355 ns |  2.59 |    0.02 |         - |          NA |
| CreateQuantityCentiLitre | 13.738 ns | 0.0480 ns | 0.0449 ns |  2.71 |    0.03 |         - |          NA |
| CreateUnitsNetCentiLitre | 12.870 ns | 0.0431 ns | 0.0403 ns |  2.54 |    0.02 |         - |          NA |
*/
