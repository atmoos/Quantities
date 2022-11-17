using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Unit.Imperial.Area;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Si;
using Quantities.Unit.Si.Accepted;

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
}

/*
// * Summary *

BenchmarkDotNet=v0.12.1, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.100
  [Host]     : .NET Core 7.0.0 (CoreCLR 7.0.22.56001, CoreFX 7.0.22.56001), X64 RyuJIT
  DefaultJob : .NET Core 7.0.0 (CoreCLR 7.0.22.56001, CoreFX 7.0.22.56001), X64 RyuJIT


|         Method |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|        Trivial | 17.03 ns | 0.089 ns | 0.083 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|       DivideSi | 31.09 ns | 0.280 ns | 0.262 ns |  1.83 |    0.02 |      - |     - |     - |         - |
| DivideImperial | 31.14 ns | 0.212 ns | 0.198 ns |  1.83 |    0.02 |      - |     - |     - |         - |
|    DivideMixed | 32.90 ns | 0.351 ns | 0.329 ns |  1.93 |    0.03 |      - |     - |     - |         - |
|  DivideAliased | 71.14 ns | 1.175 ns | 0.982 ns |  4.18 |    0.07 | 0.0114 |     - |     - |      48 B |
*/
