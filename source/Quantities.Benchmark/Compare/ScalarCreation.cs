using Quantities.Prefixes;
using Quantities.Units.Si;

using nLength = UnitsNet.Length;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser]
public class ScalarCreation
{
    private static readonly Double value = Math.E;

    [Benchmark(Baseline = true)]
    public Length Quantity() => Length.Of(value, Si<Centi, Metre>());

    [Benchmark]
    public nLength UnitsNet() => nLength.FromCentimeters(value);
}

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.113
  [Host]     : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2


| Method   | Mean      | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |----------:|----------:|----------:|------:|--------:|----------:|------------:|
| Quantity |  4.877 ns | 0.0473 ns | 0.0395 ns |  1.00 |    0.00 |         - |          NA |
| UnitsNet | 12.723 ns | 0.2395 ns | 0.2240 ns |  2.62 |    0.05 |         - |          NA |
*/
