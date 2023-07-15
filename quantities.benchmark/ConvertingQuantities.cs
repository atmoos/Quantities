using BenchmarkDotNet.Attributes;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class ConvertingQuantities
{
    private const Double feetToMetre = 0.3048;
    private static readonly Si<Metre> kiloMetre = Si<Metre>.Of(Prefix.Kilo, 3);
    private static readonly Imperial<Foot> trivialFoot = new(10, feetToMetre);
    private static readonly Length foot = Length.Of(10).Imperial<Foot>();
    private static readonly Energy energy = Energy.Of(3).Si<Milli, Watt>().Times.Si<Kilo, Second>();

    [Benchmark(Baseline = true)]
    public Double TrivialImplementation() => trivialFoot.To(kiloMetre);

    [Benchmark]
    public Double QuantityImplementation() => foot.To.Si<Kilo, Metre>();

    [Benchmark]
    public Double QuantityToSame() => energy.To.Si<Milli, Watt>().Times.Si<Kilo, Second>();

    [Benchmark]
    public Double QuantityToVeryDifferent() => energy.To.Si<Kilo, Watt>().Times.Si<Milli, Second>();
}

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.107
  [Host]     : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2


|                  Method |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------ |-----------:|----------:|----------:|-----------:|------:|--------:|----------:|------------:|
|   TrivialImplementation |  0.5576 ns | 0.0457 ns | 0.0594 ns |  0.5181 ns |  1.00 |    0.00 |         - |          NA |
|  QuantityImplementation | 11.5073 ns | 0.2566 ns | 0.2520 ns | 11.3873 ns | 19.74 |    1.70 |         - |          NA |
|          QuantityToSame | 20.6856 ns | 0.1183 ns | 0.1107 ns | 20.7004 ns | 35.20 |    3.13 |         - |          NA |
| QuantityToVeryDifferent | 25.6578 ns | 0.1213 ns | 0.1076 ns | 25.6349 ns | 43.26 |    3.80 |         - |          NA |

*/
