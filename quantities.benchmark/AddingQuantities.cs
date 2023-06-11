using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class AddingQuantities
{
    private Length sameMetric = Length.Of(-7).Si<Kilo, Metre>();
    private Length largeMetric = Length.Of(3).Si<Kilo, Metre>();
    private Length smallMetric = Length.Of(23).Si<Micro, Metre>();
    private Length largeImperial = Length.Of(-3).Imperial<Mile>();
    private Length smallImperial = Length.Of(55).Imperial<Inch>();
    private Trivial<Metre> largeTrivial = Trivial<Metre>.Si(Prefix.Kilo, 3);
    private Trivial<Metre> smallTrivial = Trivial<Metre>.Si(Prefix.Micro, 12);

    [Benchmark(Baseline = true)]
    public Double Trivial() => this.largeTrivial + this.smallTrivial;

    [Benchmark]
    public Double AddSi() => this.largeMetric + this.smallMetric;

    [Benchmark]
    public Double AddSiSame() => this.sameMetric + this.largeMetric;

    [Benchmark]
    public Double AddImperial() => this.largeImperial + this.smallImperial;

    [Benchmark]
    public Double AddMixed() => this.smallMetric + this.largeImperial;
}

/*
// * Summary *

BenchmarkDotNet=v0.12.1, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.100
  [Host]     : .NET Core 7.0.0 (CoreCLR 7.0.22.56001, CoreFX 7.0.22.56001), X64 RyuJIT
  DefaultJob : .NET Core 7.0.0 (CoreCLR 7.0.22.56001, CoreFX 7.0.22.56001), X64 RyuJIT


|      Method |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------ |---------:|----------:|----------:|---------:|------:|--------:|------:|------:|------:|----------:|
|     Trivial | 1.327 ns | 0.0113 ns | 0.0094 ns | 1.329 ns |  1.00 |    0.00 |     - |     - |     - |         - |
|       AddSi | 5.230 ns | 0.0343 ns | 0.0731 ns | 5.200 ns |  3.94 |    0.06 |     - |     - |     - |         - |
|   AddSiSame | 2.548 ns | 0.0073 ns | 0.0068 ns | 2.548 ns |  1.92 |    0.02 |     - |     - |     - |         - |
| AddImperial | 5.336 ns | 0.0215 ns | 0.0190 ns | 5.336 ns |  4.02 |    0.03 |     - |     - |     - |         - |
|    AddMixed | 6.303 ns | 0.0480 ns | 0.0449 ns | 6.305 ns |  4.75 |    0.05 |     - |     - |     - |         - |
*/
