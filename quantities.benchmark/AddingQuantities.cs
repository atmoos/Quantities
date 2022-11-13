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
|     Trivial | 0.3549 ns | 0.0158 ns | 0.0140 ns |  1.00 |    0.00 |     - |     - |     - |         - |
|       AddSi | 7.0305 ns | 0.0502 ns | 0.0445 ns | 19.84 |    0.77 |     - |     - |     - |         - |
| AddImperial | 6.9846 ns | 0.0460 ns | 0.0408 ns | 19.71 |    0.84 |     - |     - |     - |         - |
|    AddMixed | 7.0029 ns | 0.1715 ns | 0.1520 ns | 19.77 |    1.03 |     - |     - |     - |         - |
*/
