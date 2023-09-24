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
| Trivial               | 17.940 ns | 0.1072 ns | 0.1003 ns |  1.00 |         - |          NA |
| MultiplySi            |  7.293 ns | 0.0572 ns | 0.0507 ns |  0.41 |         - |          NA |
| MultiplyImperial      |  7.189 ns | 0.1748 ns | 0.2013 ns |  0.40 |         - |          NA |
| MultiplyMixed         |  6.797 ns | 0.1697 ns | 0.1886 ns |  0.38 |         - |          NA |
| MultiplyPureSi        |  6.753 ns | 0.0376 ns | 0.0352 ns |  0.38 |         - |          NA |
| MultiplyPowerQuantity |  6.946 ns | 0.0771 ns | 0.0684 ns |  0.39 |         - |          NA |
| MultiplyAliasQuantity |  7.026 ns | 0.0432 ns | 0.0361 ns |  0.39 |         - |          NA |
*/
