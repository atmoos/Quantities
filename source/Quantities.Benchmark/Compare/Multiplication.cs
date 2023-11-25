using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;

using nLength = UnitsNet.Length;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser]
public class Multiplication
{
    private static readonly Length left = Length.Of(3, Si<Milli, Metre>());
    private static readonly Length right = Length.Of(4, Imperial<Foot>());
    private static readonly nLength nLeft = nLength.FromMillimeters(3);
    private static readonly nLength nRight = nLength.FromMillimeters(4);

    [Benchmark(Baseline = true)]
    public Area Quantity() => left * right;

    [Benchmark]
    public UnitsNet.Area UnitsNet() => nLeft * nRight;
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
| Quantity | 10.80 ns | 0.200 ns | 0.187 ns |  1.00 |    0.00 |         - |          NA |
| UnitsNet | 74.44 ns | 0.903 ns | 0.845 ns |  6.89 |    0.10 |         - |          NA |
*/
