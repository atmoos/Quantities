using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class MultiplyingQuantities
{
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
}

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.110
  [Host]     : .NET 7.0.10 (7.0.1023.41001), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.10 (7.0.1023.41001), X64 RyuJIT AVX2


| Method           | Mean      | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------- |----------:|----------:|----------:|------:|--------:|----------:|------------:|
| Trivial          | 16.834 ns | 0.2695 ns | 0.2521 ns |  1.00 |    0.00 |         - |          NA |
| MultiplySi       | 20.351 ns | 0.1017 ns | 0.0849 ns |  1.21 |    0.02 |         - |          NA |
| MultiplyImperial | 20.141 ns | 0.1917 ns | 0.1793 ns |  1.20 |    0.03 |         - |          NA |
| MultiplyMixed    | 18.876 ns | 0.0838 ns | 0.0700 ns |  1.12 |    0.01 |         - |          NA |
| MultiplyPureSi   |  7.648 ns | 0.0604 ns | 0.0536 ns |  0.45 |    0.01 |         - |          NA |
*/
