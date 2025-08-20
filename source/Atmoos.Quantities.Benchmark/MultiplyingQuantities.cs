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

/* Summary

BenchmarkDotNet v0.15.2, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.109
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


| Method                | Mean     | Error    | Ratio | Allocated | Alloc Ratio |
|---------------------- |---------:|---------:|------:|----------:|------------:|
| Trivial               | 32.19 ns | 0.230 ns |  1.00 |         - |          NA |
| MultiplySi            | 33.65 ns | 0.125 ns |  1.05 |         - |          NA |
| MultiplyImperial      | 33.78 ns | 0.116 ns |  1.05 |         - |          NA |
| MultiplyMixed         | 33.48 ns | 0.113 ns |  1.04 |         - |          NA |
| MultiplyPureSi        | 33.79 ns | 0.123 ns |  1.05 |         - |          NA |
| MultiplyPowerQuantity | 33.73 ns | 0.141 ns |  1.05 |         - |          NA |
| MultiplyAliasQuantity | 33.56 ns | 0.084 ns |  1.04 |         - |          NA |
*/
