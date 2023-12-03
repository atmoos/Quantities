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

/* Summary *

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method   | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|--------- |----------:|----------:|------:|----------:|------------:|
| Quantity |  6.208 ns | 0.0226 ns |  1.00 |         - |          NA |
| UnitsNet | 42.026 ns | 0.1578 ns |  6.77 |         - |          NA |
*/
