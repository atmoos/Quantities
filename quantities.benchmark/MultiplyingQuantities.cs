using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Si;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class MultiplyingQuantities
{
    private Length largeMetric = Length.Si<Kilo, Metre>(3);
    private Length smallMetric = Length.Si<Micro, Metre>(23);
    private Length largeImperial = Length.Imperial<Mile>(-3);
    private Length smallImperial = Length.Imperial<Inch>(55);
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
|          Trivial | 17.58 ns | 0.360 ns | 0.337 ns |  1.00 |    0.00 |     - |     - |     - |         - |
|       MultiplySi | 19.55 ns | 0.075 ns | 0.067 ns |  1.11 |    0.02 |     - |     - |     - |         - |
| MultiplyImperial | 18.87 ns | 0.103 ns | 0.091 ns |  1.08 |    0.02 |     - |     - |     - |         - |
|    MultiplyMixed | 20.60 ns | 0.419 ns | 0.449 ns |  1.17 |    0.03 |     - |     - |     - |         - |
*/
