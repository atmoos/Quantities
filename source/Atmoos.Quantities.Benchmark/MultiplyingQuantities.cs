using Atmoos.Quantities.Physics;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.Si;
using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Metric;
using BenchmarkDotNet.Diagnosers;

namespace Atmoos.Quantities.Benchmark;

[MemoryDiagnoser(displayGenColumns: false)]
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
    public Si<Metre> Trivial() => this.largeTrivial * this.smallTrivial;

    [Benchmark]
    public Area MultiplySi() => this.largeMetric * this.smallMetric;

    [Benchmark]
    public Area MultiplyImperial() => this.largeImperial * this.smallImperial;

    [Benchmark]
    public Area MultiplyMixed() => this.smallMetric * this.largeImperial;

    [Benchmark]
    public ElectricPotential MultiplyPureSi() => this.current * this.resistance;

    [Benchmark]
    public Volume MultiplyPowerQuantity() => this.powerArea * this.largeMetric;

    [Benchmark]
    public Volume MultiplyAliasQuantity() => this.aliasArea * this.largeMetric;
}

/* Summary

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 3.39GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method                | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|---------------------- |----------:|----------:|------:|----------:|------------:|
| Trivial               | 15.875 ns | 0.0373 ns |  1.00 |         - |          NA |
| MultiplySi            |  6.506 ns | 0.0614 ns |  0.41 |         - |          NA |
| MultiplyImperial      |  6.128 ns | 0.0345 ns |  0.39 |         - |          NA |
| MultiplyMixed         |  6.235 ns | 0.0233 ns |  0.39 |         - |          NA |
| MultiplyPureSi        |  6.241 ns | 0.0248 ns |  0.39 |         - |          NA |
| MultiplyPowerQuantity |  6.567 ns | 0.0223 ns |  0.41 |         - |          NA |
| MultiplyAliasQuantity |  6.581 ns | 0.0181 ns |  0.41 |         - |          NA |
*/
