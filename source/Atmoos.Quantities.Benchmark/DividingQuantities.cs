using BenchmarkDotNet.Diagnosers;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Imperial.Area;
using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.Si;
using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Metric;

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

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.103
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


| Method         | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|--------------- |----------:|----------:|------:|----------:|------------:|
| Trivial        | 16.767 ns | 0.0231 ns |  1.00 |         - |          NA |
| DivideSi       |  6.172 ns | 0.0122 ns |  0.37 |         - |          NA |
| DivideImperial |  6.470 ns | 0.0255 ns |  0.39 |         - |          NA |
| DivideMixed    |  6.250 ns | 0.0097 ns |  0.37 |         - |          NA |
| DivideAliased  |  6.236 ns | 0.0162 ns |  0.37 |         - |          NA |
| DividePureSi   |  6.310 ns | 0.0149 ns |  0.38 |         - |          NA |
*/
