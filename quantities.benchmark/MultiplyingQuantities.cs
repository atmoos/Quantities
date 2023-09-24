using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class MultiplyingQuantities
{
    private Area aliasArea = Area.Of(3).Metric<Are>();
    private Area powerArea = Area.Of(3).Square.Si<Metre>();
    private Length largeMetric = Length.Of(3).Si<Kilo, Metre>();
    private Length smallMetric = Length.Of(23).Si<Micro, Metre>();
    private Length largeImperial = Length.Of(-3).Imperial<Mile>();
    private Length smallImperial = Length.Of(55).Imperial<Inch>();
    private ElectricCurrent current = ElectricCurrent.Of(200).Si<Micro, Ampere>();
    private ElectricalResistance resistance = ElectricalResistance.Of(734).Si<Kilo, Ohm>();
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
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method                | Mean      | Error     | StdDev    | Ratio | Allocated | Alloc Ratio |
|---------------------- |----------:|----------:|----------:|------:|----------:|------------:|
| Trivial               | 17.806 ns | 0.1566 ns | 0.1465 ns |  1.00 |         - |          NA |
| MultiplySi            |  7.392 ns | 0.0781 ns | 0.0731 ns |  0.42 |         - |          NA |
| MultiplyImperial      |  7.812 ns | 0.0237 ns | 0.0222 ns |  0.44 |         - |          NA |
| MultiplyMixed         |  6.851 ns | 0.0751 ns | 0.0703 ns |  0.38 |         - |          NA |
| MultiplyPureSi        |  7.666 ns | 0.0279 ns | 0.0261 ns |  0.43 |         - |          NA |
| MultiplyPowerQuantity |  6.848 ns | 0.1428 ns | 0.1336 ns |  0.38 |         - |          NA |
| MultiplyAliasQuantity |  7.514 ns | 0.0375 ns | 0.0313 ns |  0.42 |         - |          NA |
*/
