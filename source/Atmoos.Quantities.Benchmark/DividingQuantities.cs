using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Imperial.Area;
using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.Si;
using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Metric;
using BenchmarkDotNet.Diagnosers;

namespace Atmoos.Quantities.Benchmark;

[MemoryDiagnoser(displayGenColumns: false)]
public class DividingQuantities
{
    private Volume metricVolume = Volume.Of(3, Cubic(Si<Kilo, Metre>()));
    private Volume metricAcceptedVolume = Volume.Of(3, Metric<Kilo, Litre>());
    private Area metricArea = Area.Of(23, Square(Si<Deca, Metre>()));
    private Area imperialPureArea = Area.Of(23, Imperial<Acre>());
    private Volume imperialVolume = Volume.Of(-3, Cubic(Imperial<Mile>()));
    private Area imperialArea = Area.Of(55, Square(Imperial<Yard>()));
    private ElectricPotential potential = ElectricPotential.Of(33, Si<Kilo, Volt>());
    private ElectricCurrent current = ElectricCurrent.Of(98, Si<Deca, Ampere>());
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

/* Summary

BenchmarkDotNet v0.15.3, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method         | Mean     | Error    | Ratio | Allocated | Alloc Ratio |
|--------------- |---------:|---------:|------:|----------:|------------:|
| Trivial        | 32.27 ns | 0.111 ns |  1.00 |         - |          NA |
| DivideSi       | 32.42 ns | 0.123 ns |  1.00 |         - |          NA |
| DivideImperial | 32.43 ns | 0.120 ns |  1.01 |         - |          NA |
| DivideMixed    | 32.31 ns | 0.135 ns |  1.00 |         - |          NA |
| DivideAliased  | 32.33 ns | 0.145 ns |  1.00 |         - |          NA |
| DividePureSi   | 32.46 ns | 0.114 ns |  1.01 |         - |          NA |
*/
