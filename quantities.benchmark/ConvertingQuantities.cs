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
.NET SDK=7.0.109
  [Host]     : .NET 7.0.9 (7.0.923.36701), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.9 (7.0.923.36701), X64 RyuJIT AVX2


|                  Method |       Mean |     Error |    StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------ |-----------:|----------:|----------:|------:|--------:|----------:|------------:|
|   TrivialImplementation |  0.5941 ns | 0.0151 ns | 0.0141 ns |  1.00 |    0.00 |         - |          NA |
|  QuantityImplementation | 18.7820 ns | 0.0960 ns | 0.0851 ns | 31.67 |    0.80 |         - |          NA |
|          QuantityToSame | 20.5653 ns | 0.0734 ns | 0.0651 ns | 34.68 |    0.80 |         - |          NA |
| QuantityToVeryDifferent | 32.6280 ns | 0.5832 ns | 0.5170 ns | 55.04 |    2.00 |         - |          NA |
*/
