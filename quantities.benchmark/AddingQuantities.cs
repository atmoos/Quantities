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
    private Length largeMetric = Length.Si<Kilo, Metre>(3);
    private Length smallMetric = Length.Si<Micro, Metre>(23);
    private Length largeImperial = Length.Imperial<Mile>(-3);
    private Length smallImperial = Length.Imperial<Inch>(55);
    private Trivial<Metre> largeTrivial = Trivial<Metre>.Si(Prefix.Kilo, 3);
    private Trivial<Metre> smallTrivial = Trivial<Metre>.Si(Prefix.Micro, 12);

    [Benchmark(Baseline = true)]
    public Double Trivial() => this.largeTrivial + this.smallTrivial;

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


|      Method |     Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------ |---------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|     Trivial | 1.536 ns | 0.0512 ns | 0.0479 ns |  1.00 |    0.00 |     - |     - |     - |         - |
|       AddSi | 7.524 ns | 0.0340 ns | 0.0318 ns |  4.90 |    0.15 |     - |     - |     - |         - |
| AddImperial | 7.882 ns | 0.0511 ns | 0.0453 ns |  5.12 |    0.16 |     - |     - |     - |         - |
|    AddMixed | 8.128 ns | 0.1766 ns | 0.3606 ns |  5.57 |    0.12 |     - |     - |     - |         - |
*/
