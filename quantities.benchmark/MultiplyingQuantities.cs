using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class MultiplyingQuantities
{
    private Length largeMetric = Length.Of(3).Si<Kilo, Metre>();
    private Length smallMetric = Length.Of(23).Si<Micro, Metre>();
    private Length largeImperial = Length.Of(-3).Imperial<Mile>();
    private Length smallImperial = Length.Of(55).Imperial<Inch>();
    private ElectricCurrent current = ElectricCurrent.Of(200).Si<Micro, Ampere>();
    private ElectricalResistance resistance = ElectricalResistance.Of(734).Si<Kilo, Ohm>();
    private Si<Metre> largeTrivial = Si<Metre>.Of(Prefix.Kilo, 3);
    private Si<Metre> smallTrivial = Si<Metre>.Of(Prefix.Micro, 12);

    [Benchmark(Baseline = true)]
    public Double Trivial() => this.largeTrivial * this.smallTrivial;

    [Benchmark]
    public Double MultiplySi() => this.largeMetric * this.smallMetric;

    [Benchmark]
    public Double MultiplyImperial() => this.largeImperial * this.smallImperial;

    [Benchmark]
    public Double MultiplyMixed() => this.smallMetric * this.largeImperial;

    [Benchmark]
    public Double MultiplyPureSi() => this.current * this.resistance;
}

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.107
  [Host]     : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2


|           Method |     Mean |    Error |   StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------- |---------:|---------:|---------:|------:|--------:|----------:|------------:|
|          Trivial | 16.59 ns | 0.118 ns | 0.092 ns |  1.00 |    0.00 |         - |          NA |
|       MultiplySi | 18.63 ns | 0.392 ns | 0.385 ns |  1.13 |    0.03 |         - |          NA |
| MultiplyImperial | 18.09 ns | 0.370 ns | 0.346 ns |  1.09 |    0.03 |         - |          NA |
|    MultiplyMixed | 17.80 ns | 0.108 ns | 0.101 ns |  1.07 |    0.01 |         - |          NA |
|   MultiplyPureSi | 11.82 ns | 0.256 ns | 0.263 ns |  0.71 |    0.02 |         - |          NA |
*/
