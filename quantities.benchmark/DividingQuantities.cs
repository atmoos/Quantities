using System;
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
    private Trivial<Metre> largeTrivial = Trivial<Metre>.Si(Prefix.Kilo, 3);
    private Trivial<Metre> smallTrivial = Trivial<Metre>.Si(Prefix.Micro, 12);

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

BenchmarkDotNet=v0.13.2, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.12801), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.12801), X64 RyuJIT AVX2


|         Method |     Mean |    Error |   StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------- |---------:|---------:|---------:|------:|--------:|----------:|------------:|
|        Trivial | 16.57 ns | 0.127 ns | 0.112 ns |  1.00 |    0.00 |         - |          NA |
|       DivideSi | 31.98 ns | 0.642 ns | 0.569 ns |  1.93 |    0.03 |         - |          NA |
| DivideImperial | 30.06 ns | 0.195 ns | 0.173 ns |  1.81 |    0.02 |         - |          NA |
|    DivideMixed | 28.86 ns | 0.117 ns | 0.103 ns |  1.74 |    0.01 |         - |          NA |
|  DivideAliased | 42.24 ns | 0.478 ns | 0.447 ns |  2.55 |    0.03 |         - |          NA |
|   DividePureSi | 11.35 ns | 0.043 ns | 0.038 ns |  0.69 |    0.00 |         - |          NA |
*/
