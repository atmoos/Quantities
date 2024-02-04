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

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method   | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|--------- |----------:|----------:|------:|----------:|------------:|
| Quantity |  8.882 ns | 0.0463 ns |  1.00 |         - |          NA |
| UnitsNet | 46.304 ns | 0.1927 ns |  5.21 |         - |          NA |
*/
