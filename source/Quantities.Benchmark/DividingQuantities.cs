using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Area;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;

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

/* Summary *

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-1260P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method         | Mean     | Error     | Ratio | Allocated | Alloc Ratio |
|--------------- |---------:|----------:|------:|----------:|------------:|
| Trivial        | 3.476 ns | 0.0764 ns |  1.00 |         - |          NA |
| DivideSi       | 5.891 ns | 0.0892 ns |  1.70 |         - |          NA |
| DivideImperial | 5.828 ns | 0.0751 ns |  1.68 |         - |          NA |
| DivideMixed    | 5.423 ns | 0.1491 ns |  1.73 |         - |          NA |
| DivideAliased  | 5.972 ns | 0.1077 ns |  1.72 |         - |          NA |
| DividePureSi   | 5.876 ns | 0.0585 ns |  1.69 |         - |          NA |
*/
