using BenchmarkDotNet.Attributes;
using Quantities.Prefixes;
using Quantities.Units.Si;

using nLength = UnitsNet.Length;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser]
public class ScalarCreation
{
    private static readonly Double value = Math.E;

    [Benchmark(Baseline = true)]
    public Length Quantity() => Length.Of(value).Si<Centi, Metre>();

    [Benchmark]
    public nLength UnitsNet() => nLength.FromCentimeters(value);
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
| Quantity | 10.68 ns | 0.150 ns | 0.141 ns |  1.00 |    0.00 |         - |          NA |
| UnitsNet | 12.84 ns | 0.139 ns | 0.130 ns |  1.20 |    0.02 |         - |          NA |
*/
