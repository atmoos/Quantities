using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Imperial.Area;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Accepted;
using Quantities.Units.Si.Derived;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class DividingQuantities
{
    private Volume metricVolume = Volume.Cubic<Kilo, Metre>(3);
    private Volume metricAcceptedVolume = Volume.Si<Kilo, Litre>(3);
    private Area metricArea = Area.Square<Deca, Metre>(23);
    private Area imperialPureArea = Area.Imperial<Acre>(23);
    private Volume imperialVolume = Volume.CubicImperial<Mile>(-3);
    private Area imperialArea = Area.SquareImperial<Yard>(55);
    private ElectricPotential potential = ElectricPotential.Si<Kilo, Volt>(33);
    private ElectricCurrent current = ElectricCurrent.Si<Deca, Ampere>(98);
    private Trivial<Metre> largeTrivial = Trivial<Metre>.Si(Prefix.Kilo, 3);
    private Trivial<Metre> smallTrivial = Trivial<Metre>.Si(Prefix.Micro, 12);

    [Benchmark(Baseline = true)]
    public Double Trivial() => this.largeTrivial / this.smallTrivial;

    [Benchmark]
    public Double DivideSi() => this.metricVolume / this.metricArea;

    [Benchmark]
    public Double DivideImperial() => this.imperialVolume / this.imperialArea;

    [Benchmark]
    public Double DivideMixed() => this.metricVolume / this.imperialArea;

    [Benchmark]
    public Double DivideAliased() => this.metricAcceptedVolume / this.imperialPureArea;

    [Benchmark]
    public Double DividePureSi() => this.potential / this.current;
}

/*
// * Summary *

BenchmarkDotNet=v0.12.1, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.100
  [Host]     : .NET Core 7.0.0 (CoreCLR 7.0.22.56001, CoreFX 7.0.22.56001), X64 RyuJIT
  DefaultJob : .NET Core 7.0.0 (CoreCLR 7.0.22.56001, CoreFX 7.0.22.56001), X64 RyuJIT


|         Method |     Mean |    Error |   StdDev | Ratio | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------:|---------:|---------:|------:|------:|------:|------:|----------:|
|        Trivial | 16.06 ns | 0.043 ns | 0.036 ns |  1.00 |     - |     - |     - |         - |
|       DivideSi | 30.35 ns | 0.061 ns | 0.057 ns |  1.89 |     - |     - |     - |         - |
| DivideImperial | 27.27 ns | 0.054 ns | 0.048 ns |  1.70 |     - |     - |     - |         - |
|    DivideMixed | 27.91 ns | 0.013 ns | 0.011 ns |  1.74 |     - |     - |     - |         - |
|  DivideAliased | 38.82 ns | 0.072 ns | 0.063 ns |  2.42 |     - |     - |     - |         - |
|   DividePureSi | 12.48 ns | 0.048 ns | 0.045 ns |  0.78 |     - |     - |     - |         - |
*/
