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
    private const Double kilo = 1e3;
    private const Double micro = 1e-6;
    private Length largeMetric = Length.Si<Kilo, Metre>(3);
    private Length smallMetric = Length.Si<Micro, Metre>(23);
    private Length largeImperial = Length.Imperial<Mile>(-3);
    private Length smallImperial = Length.Imperial<Inch>(55);

    private Double largeTrivial = 3;
    private Double smallTrivial = 5;

    [Benchmark(Baseline = true)]
    public Double Trivial() => this.largeTrivial * micro * this.smallTrivial / kilo;

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


|           Method |       Mean |     Error |    StdDev |  Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |-----------:|----------:|----------:|-------:|--------:|------:|------:|------:|----------:|
|          Trivial |  0.0728 ns | 0.0054 ns | 0.0048 ns |   1.00 |    0.00 |     - |     - |     - |         - |
|       MultiplySi | 19.0518 ns | 0.0919 ns | 0.0859 ns | 262.59 |   16.90 |     - |     - |     - |         - |
| MultiplyImperial | 19.3571 ns | 0.0565 ns | 0.0472 ns | 265.77 |   17.14 |     - |     - |     - |         - |
|    MultiplyMixed | 19.5089 ns | 0.0803 ns | 0.0712 ns | 268.84 |   17.02 |     - |     - |     - |         - |
*/
