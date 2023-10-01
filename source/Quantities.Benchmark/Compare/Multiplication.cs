using BenchmarkDotNet.Attributes;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;

using nLength = UnitsNet.Length;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser]
public class Multiplication
{
    private static readonly Length left = Length.Of(3).Si<Milli, Metre>();
    private static readonly Length right = Length.Of(4).Imperial<Foot>();
    private static readonly nLength nLeft = nLength.FromMillimeters(3);
    private static readonly nLength nRight = nLength.FromMillimeters(4);

    [Benchmark(Baseline = true)]
    public UnitsNet.Area UnitsNetMultiply() => nLeft * nRight;

    [Benchmark]
    public Area QuantityMultiply() => left * right;
}

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method           | Mean     | Error    | StdDev   | Ratio | Allocated | Alloc Ratio |
|----------------- |---------:|---------:|---------:|------:|----------:|------------:|
| UnitsNetMultiply | 78.01 ns | 0.527 ns | 0.493 ns |  1.00 |         - |          NA |
| QuantityMultiply | 10.64 ns | 0.046 ns | 0.041 ns |  0.14 |         - |          NA |
*/
