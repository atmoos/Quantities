using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Si;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class AddingQuantities
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
    public Double Trivial() => this.largeTrivial + micro * this.smallTrivial / kilo;

    [Benchmark]
    public Double AddSi() => this.largeMetric + this.smallMetric;

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


|      Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------ |----------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|     Trivial | 0.3227 ns | 0.0105 ns | 0.0088 ns |  1.00 |    0.00 |     - |     - |     - |         - |
|       AddSi | 7.0637 ns | 0.1214 ns | 0.1135 ns | 21.98 |    0.75 |     - |     - |     - |         - |
| AddImperial | 7.0393 ns | 0.0479 ns | 0.0400 ns | 21.83 |    0.63 |     - |     - |     - |         - |
|    AddMixed | 6.8587 ns | 0.0264 ns | 0.0234 ns | 21.26 |    0.60 |     - |     - |     - |         - |
*/
