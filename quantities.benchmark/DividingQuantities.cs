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

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.110
  [Host]     : .NET 7.0.10 (7.0.1023.41001), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.10 (7.0.1023.41001), X64 RyuJIT AVX2


| Method         | Mean      | Error     | StdDev    | Ratio | Allocated | Alloc Ratio |
|--------------- |----------:|----------:|----------:|------:|----------:|------------:|
| Trivial        | 16.990 ns | 0.2814 ns | 0.2633 ns |  1.00 |         - |          NA |
| DivideSi       |  7.448 ns | 0.0718 ns | 0.0672 ns |  0.44 |         - |          NA |
| DivideImperial |  7.284 ns | 0.0323 ns | 0.0287 ns |  0.43 |         - |          NA |
| DivideMixed    |  7.527 ns | 0.0541 ns | 0.0506 ns |  0.44 |         - |          NA |
| DivideAliased  |  7.725 ns | 0.0544 ns | 0.0454 ns |  0.46 |         - |          NA |
| DividePureSi   |  7.751 ns | 0.0463 ns | 0.0433 ns |  0.46 |         - |          NA |
*/
