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

BenchmarkDotNet v0.13.7, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.109
  [Host]     : .NET 7.0.9 (7.0.923.36701), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.9 (7.0.923.36701), X64 RyuJIT AVX2


|                  Method |       Mean |     Error |    StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------ |-----------:|----------:|----------:|------:|--------:|----------:|------------:|
|   TrivialImplementation |  0.6881 ns | 0.0174 ns | 0.0154 ns |  1.00 |    0.00 |         - |          NA |
|  QuantityImplementation | 16.9283 ns | 0.0778 ns | 0.0650 ns | 24.66 |    0.57 |         - |          NA |
|          QuantityToSame | 20.8640 ns | 0.2727 ns | 0.2551 ns | 30.35 |    0.94 |         - |          NA |
| QuantityToVeryDifferent | 29.9271 ns | 0.4869 ns | 0.4316 ns | 43.51 |    1.24 |         - |          NA |
*/
