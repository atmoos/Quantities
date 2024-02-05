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

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-1260P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method                | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|---------------------- |----------:|----------:|------:|----------:|------------:|
| Trivial               | 14.497 ns | 0.2318 ns |  1.00 |         - |          NA |
| MultiplySi            |  5.759 ns | 0.0974 ns |  0.40 |         - |          NA |
| MultiplyImperial      |  5.833 ns | 0.0804 ns |  0.40 |         - |          NA |
| MultiplyMixed         |  5.815 ns | 0.0893 ns |  0.40 |         - |          NA |
| MultiplyPureSi        |  5.894 ns | 0.0560 ns |  0.41 |         - |          NA |
| MultiplyPowerQuantity |  5.786 ns | 0.0387 ns |  0.40 |         - |          NA |
| MultiplyAliasQuantity |  5.924 ns | 0.1458 ns |  0.41 |         - |          NA |
*/
