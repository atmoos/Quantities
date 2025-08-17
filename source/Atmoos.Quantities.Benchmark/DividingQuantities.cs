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

BenchmarkDotNet v0.15.2, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.109
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


| Method         | Mean     | Error    | Ratio | Allocated | Alloc Ratio |
|--------------- |---------:|---------:|------:|----------:|------------:|
| Trivial        | 31.64 ns | 0.096 ns |  1.00 |         - |          NA |
| DivideSi       | 33.79 ns | 0.128 ns |  1.07 |         - |          NA |
| DivideImperial | 33.74 ns | 0.093 ns |  1.07 |         - |          NA |
| DivideMixed    | 33.84 ns | 0.130 ns |  1.07 |         - |          NA |
| DivideAliased  | 33.48 ns | 0.126 ns |  1.06 |         - |          NA |
| DividePureSi   | 35.94 ns | 0.186 ns |  1.14 |         - |          NA |
*/
