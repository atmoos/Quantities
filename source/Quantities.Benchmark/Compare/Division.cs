using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si.Metric;
using nLength = UnitsNet.Length;
using nVolume = UnitsNet.Volume;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser]
public class Division
{
    private static readonly Volume left = Volume.Of(32, Metric<Centi, Litre>());
    private static readonly Length right = Length.Of(4, Imperial<Foot>());
    private static readonly nVolume nLeft = nVolume.FromCentiliters(32);
    private static readonly nLength nRight = nLength.FromFeet(4);

    [Benchmark(Baseline = true)]
    public Area Quantity() => left / right;

    [Benchmark]
    public UnitsNet.Area UnitsNet() => nLeft / nRight;
}

/* Summary *

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-1260P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method   | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|--------- |----------:|----------:|------:|----------:|------------:|
| Quantity |  6.624 ns | 0.0736 ns |  1.00 |         - |          NA |
| UnitsNet | 24.402 ns | 0.1962 ns |  3.68 |         - |          NA |
*/
