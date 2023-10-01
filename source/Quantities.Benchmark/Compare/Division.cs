using BenchmarkDotNet.Attributes;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Metric;
using nLength = UnitsNet.Length;
using nVolume = UnitsNet.Volume;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser]
public class Division
{
    private static readonly Volume left = Volume.Of(32).Metric<Centi, Litre>();
    private static readonly Length right = Length.Of(4).Imperial<Foot>();
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
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method   | Mean     | Error    | StdDev   | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |---------:|---------:|---------:|------:|--------:|----------:|------------:|
| Quantity | 12.06 ns | 0.055 ns | 0.046 ns |  1.00 |    0.00 |         - |          NA |
| UnitsNet | 80.38 ns | 0.243 ns | 0.216 ns |  6.66 |    0.03 |         - |          NA |
*/
