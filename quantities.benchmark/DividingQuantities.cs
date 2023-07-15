using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Imperial.Area;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class DividingQuantities
{
    private Volume metricVolume = Volume.Of(3).Cubic.Si<Kilo, Metre>();
    private Volume metricAcceptedVolume = Volume.Of(3).Metric<Kilo, Litre>();
    private Area metricArea = Area.Of(23).Square.Si<Deca, Metre>();
    private Area imperialPureArea = Area.Of(23).Imperial<Acre>();
    private Volume imperialVolume = Volume.Of(-3).Cubic.Imperial<Mile>();
    private Area imperialArea = Area.Of(55).Square.Imperial<Yard>();
    private ElectricPotential potential = ElectricPotential.Of(33).Si<Kilo, Volt>();
    private ElectricCurrent current = ElectricCurrent.Of(98).Si<Deca, Ampere>();
    private Si<Metre> largeTrivial = Si<Metre>.Of(Prefix.Kilo, 3);
    private Si<Metre> smallTrivial = Si<Metre>.Of(Prefix.Micro, 12);

    [Benchmark(Baseline = true)]
    public Double Trivial() => this.largeTrivial / this.smallTrivial;

    [Benchmark]
    public Double DivideSi() => this.metricVolume / this.metricArea;

    [Benchmark]
    public Double DivideImperial() => this.imperialVolume / this.imperialArea;

    [Benchmark]
    public Double DivideMixed() => this.metricVolume / this.imperialArea;

    [Benchmark]
    public Double DivideAliased() => this.metricAcceptedVolume / this.imperialPureArea;

    [Benchmark]
    public Double DividePureSi() => this.potential / this.current;
}

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.107
  [Host]     : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2


|         Method |     Mean |    Error |   StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------- |---------:|---------:|---------:|------:|--------:|----------:|------------:|
|        Trivial | 16.71 ns | 0.175 ns | 0.163 ns |  1.00 |    0.00 |         - |          NA |
|       DivideSi | 30.29 ns | 0.238 ns | 0.211 ns |  1.81 |    0.02 |         - |          NA |
| DivideImperial | 32.13 ns | 0.671 ns | 0.746 ns |  1.93 |    0.06 |         - |          NA |
|    DivideMixed | 32.49 ns | 0.163 ns | 0.152 ns |  1.94 |    0.02 |         - |          NA |
|  DivideAliased | 44.42 ns | 0.891 ns | 0.991 ns |  2.65 |    0.06 |         - |          NA |
|   DividePureSi | 11.70 ns | 0.116 ns | 0.091 ns |  0.70 |    0.01 |         - |          NA |
*/
