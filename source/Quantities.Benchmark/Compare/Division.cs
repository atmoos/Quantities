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

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.113
  [Host]     : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2


| Method   | Mean     | Error    | StdDev   | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |---------:|---------:|---------:|------:|--------:|----------:|------------:|
| Quantity | 12.70 ns | 0.211 ns | 0.187 ns |  1.00 |    0.00 |         - |          NA |
| UnitsNet | 81.20 ns | 0.413 ns | 0.386 ns |  6.40 |    0.10 |         - |          NA |
*/
