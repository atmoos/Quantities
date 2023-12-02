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

/* Summary *

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


 Method                | Mean      | Error     | StdDev    | Ratio | Allocated | Alloc Ratio |
---------------------- |----------:|----------:|----------:|------:|----------:|------------:|
 Trivial               | 16.637 ns | 0.0933 ns | 0.0827 ns |  1.00 |         - |          NA |
 MultiplySi            |  6.541 ns | 0.0508 ns | 0.0424 ns |  0.39 |         - |          NA |
 MultiplyImperial      |  6.516 ns | 0.0318 ns | 0.0282 ns |  0.39 |         - |          NA |
 MultiplyMixed         |  6.763 ns | 0.0422 ns | 0.0374 ns |  0.41 |         - |          NA |
 MultiplyPureSi        |  6.532 ns | 0.0459 ns | 0.0407 ns |  0.39 |         - |          NA |
 MultiplyPowerQuantity |  6.303 ns | 0.1226 ns | 0.1147 ns |  0.38 |         - |          NA |
 MultiplyAliasQuantity |  6.477 ns | 0.0374 ns | 0.0332 ns |  0.39 |         - |          NA |
*/
