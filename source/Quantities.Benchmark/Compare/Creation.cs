using BenchmarkDotNet.Attributes;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser]
public class Creation
{
    private static Double value = Math.E;

    [Benchmark(Baseline = true)]
    public UnitsNet.Length CreateUnitsNet() => UnitsNet.Length.FromCentimeters(value);

    [Benchmark]
    public Length CreateQuantity() => Length.Of(value).Si<Centi, Metre>();
}

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method         | Mean     | Error    | StdDev   | Ratio | Allocated | Alloc Ratio |
|--------------- |---------:|---------:|---------:|------:|----------:|------------:|
| CreateUnitsNet | 13.02 ns | 0.129 ns | 0.114 ns |  1.00 |         - |          NA |
| CreateQuantity | 10.70 ns | 0.046 ns | 0.038 ns |  0.82 |         - |          NA |
*/
