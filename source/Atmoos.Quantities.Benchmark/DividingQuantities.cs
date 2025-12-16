using Atmoos.Quantities.Physics;
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
    public Length DivideSi() => this.metricVolume / this.metricArea;

    [Benchmark]
    public Length DivideImperial() => this.imperialVolume / this.imperialArea;

    [Benchmark]
    public Length DivideMixed() => this.metricVolume / this.imperialArea;

    [Benchmark]
    public Length DivideAliased() => this.metricAcceptedVolume / this.imperialPureArea;

    [Benchmark]
    public ElectricalResistance DividePureSi() => this.potential / this.current;
}

/* Summary

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method         | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|--------------- |----------:|----------:|------:|----------:|------------:|
| Trivial        | 15.598 ns | 0.0320 ns |  1.00 |         - |          NA |
| DivideSi       |  6.415 ns | 0.0599 ns |  0.41 |         - |          NA |
| DivideImperial |  7.924 ns | 0.0144 ns |  0.51 |         - |          NA |
| DivideMixed    |  6.477 ns | 0.0104 ns |  0.42 |         - |          NA |
| DivideAliased  |  7.814 ns | 0.0210 ns |  0.50 |         - |          NA |
| DividePureSi   |  7.664 ns | 0.0210 ns |  0.49 |         - |          NA |
*/
