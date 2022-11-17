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


|         Method |     Mean |    Error |   StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------:|---------:|---------:|------:|--------:|------:|------:|------:|----------:|
|        Trivial | 17.65 ns | 0.156 ns | 0.138 ns |  1.00 |    0.00 |     - |     - |     - |         - |
|       DivideSi | 30.41 ns | 0.237 ns | 0.221 ns |  1.72 |    0.02 |     - |     - |     - |         - |
| DivideImperial | 31.84 ns | 0.216 ns | 0.202 ns |  1.80 |    0.02 |     - |     - |     - |         - |
|    DivideMixed | 31.82 ns | 0.208 ns | 0.184 ns |  1.80 |    0.02 |     - |     - |     - |         - |
|  DivideAliased | 43.34 ns | 0.276 ns | 0.258 ns |  2.46 |    0.02 |     - |     - |     - |         - |
*/
