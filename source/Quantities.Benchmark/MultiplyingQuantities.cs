using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;

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

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method                | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|---------------------- |----------:|----------:|------:|----------:|------------:|
| Trivial               | 16.300 ns | 0.0267 ns |  1.00 |         - |          NA |
| MultiplySi            |  6.998 ns | 0.0881 ns |  0.43 |         - |          NA |
| MultiplyImperial      |  6.398 ns | 0.0263 ns |  0.39 |         - |          NA |
| MultiplyMixed         |  6.500 ns | 0.0178 ns |  0.40 |         - |          NA |
| MultiplyPureSi        |  6.414 ns | 0.0142 ns |  0.39 |         - |          NA |
| MultiplyPowerQuantity |  7.247 ns | 0.0203 ns |  0.44 |         - |          NA |
| MultiplyAliasQuantity |  6.529 ns | 0.0179 ns |  0.40 |         - |          NA |
*/
