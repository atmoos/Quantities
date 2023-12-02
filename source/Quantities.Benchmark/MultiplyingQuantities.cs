using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class MultiplyingQuantities
{
    private Area aliasArea = Area.Of(3, Metric<Are>());
    private Area powerArea = Area.Of(3, Square(Si<Metre>()));
    private Length largeMetric = Length.Of(3, Si<Kilo, Metre>());
    private Length smallMetric = Length.Of(23, Si<Micro, Metre>());
    private Length largeImperial = Length.Of(-3, Imperial<Mile>());
    private Length smallImperial = Length.Of(55, Imperial<Inch>());
    private ElectricCurrent current = ElectricCurrent.Of(200, Si<Micro, Ampere>());
    private ElectricalResistance resistance = ElectricalResistance.Of(734, Si<Kilo, Ohm>());
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

    [Benchmark]
    public Double MultiplyPowerQuantity() => this.powerArea * this.largeMetric;

    [Benchmark]
    public Double MultiplyAliasQuantity() => this.aliasArea * this.largeMetric;
}

/* It's approx 30ns / ratio of 1.8 at allocated 88 B 
   when optimizations are turned off.
   AllocationFree<T>, caching of Results etc...
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method                | Mean      | Error     | StdDev    | Ratio | Allocated | Alloc Ratio |
|---------------------- |----------:|----------:|----------:|------:|----------:|------------:|
| Trivial               | 17.870 ns | 0.0739 ns | 0.0655 ns |  1.00 |         - |          NA |
| MultiplySi            |  6.561 ns | 0.1519 ns | 0.1347 ns |  0.37 |         - |          NA |
| MultiplyImperial      |  6.524 ns | 0.0430 ns | 0.0381 ns |  0.37 |         - |          NA |
| MultiplyMixed         |  6.674 ns | 0.0186 ns | 0.0174 ns |  0.37 |         - |          NA |
| MultiplyPureSi        |  6.242 ns | 0.0528 ns | 0.0468 ns |  0.35 |         - |          NA |
| MultiplyPowerQuantity |  6.075 ns | 0.0170 ns | 0.0133 ns |  0.34 |         - |          NA |
| MultiplyAliasQuantity |  6.219 ns | 0.0128 ns | 0.0120 ns |  0.35 |         - |          NA |
*/
