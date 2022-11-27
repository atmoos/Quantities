using System;
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
    private Length largeMetric = Length.Si<Kilo, Metre>(3);
    private Length smallMetric = Length.Si<Micro, Metre>(23);
    private Length largeImperial = Length.Imperial<Mile>(-3);
    private Length smallImperial = Length.Imperial<Inch>(55);
    private ElectricCurrent current = ElectricCurrent.Si<Micro, Ampere>(200);
    private ElectricalResistance resistance = ElectricalResistance.Si<Kilo, Ohm>(734);
    private Trivial<Metre> largeTrivial = Trivial<Metre>.Si(Prefix.Kilo, 3);
    private Trivial<Metre> smallTrivial = Trivial<Metre>.Si(Prefix.Micro, 12);

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

BenchmarkDotNet=v0.12.1, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.100
  [Host]     : .NET Core 7.0.0 (CoreCLR 7.0.22.56001, CoreFX 7.0.22.56001), X64 RyuJIT
  DefaultJob : .NET Core 7.0.0 (CoreCLR 7.0.22.56001, CoreFX 7.0.22.56001), X64 RyuJIT


|           Method |     Mean |    Error |   StdDev | Ratio | Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|------:|------:|------:|------:|----------:|
|          Trivial | 15.68 ns | 0.057 ns | 0.051 ns |  1.00 |     - |     - |     - |         - |
|       MultiplySi | 16.95 ns | 0.011 ns | 0.008 ns |  1.08 |     - |     - |     - |         - |
| MultiplyImperial | 16.78 ns | 0.029 ns | 0.023 ns |  1.07 |     - |     - |     - |         - |
|    MultiplyMixed | 16.97 ns | 0.058 ns | 0.049 ns |  1.08 |     - |     - |     - |         - |
|   MultiplyPureSi | 11.81 ns | 0.057 ns | 0.054 ns |  0.75 |     - |     - |     - |         - |
*/
