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
| Trivial        | 16.879 ns | 0.2173 ns | 0.2032 ns |  1.00 |         - |          NA |
| DivideSi       |  7.071 ns | 0.1696 ns | 0.1666 ns |  0.42 |         - |          NA |
| DivideImperial |  6.835 ns | 0.0365 ns | 0.0342 ns |  0.40 |         - |          NA |
| DivideMixed    |  7.308 ns | 0.0999 ns | 0.0934 ns |  0.43 |         - |          NA |
| DivideAliased  |  7.354 ns | 0.0914 ns | 0.0810 ns |  0.44 |         - |          NA |
| DividePureSi   |  6.769 ns | 0.0395 ns | 0.0330 ns |  0.40 |         - |          NA |
*/
