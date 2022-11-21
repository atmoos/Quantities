﻿using System;
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


|           Method |     Mean |    Error |   StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|------:|--------:|------:|------:|------:|----------:|
|          Trivial | 17.30 ns | 0.269 ns | 0.252 ns |  1.00 |    0.00 |     - |     - |     - |         - |
|       MultiplySi | 19.59 ns | 0.343 ns | 0.304 ns |  1.13 |    0.02 |     - |     - |     - |         - |
| MultiplyImperial | 22.95 ns | 0.362 ns | 0.338 ns |  1.33 |    0.03 |     - |     - |     - |         - |
|    MultiplyMixed | 20.72 ns | 0.446 ns | 0.815 ns |  1.25 |    0.05 |     - |     - |     - |         - |
|   MultiplyPureSi | 27.68 ns | 0.061 ns | 0.051 ns |  1.60 |    0.02 |     - |     - |     - |         - |
*/
