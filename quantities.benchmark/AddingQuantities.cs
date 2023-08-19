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
    private Si<Metre> largeTrivial = Si<Metre>.Of(Prefix.Kilo, 3);
    private Si<Metre> smallTrivial = Si<Metre>.Of(Prefix.Micro, 12);

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

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.107
  [Host]     : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2


|      Method |     Mean |     Error |    StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------ |---------:|----------:|----------:|------:|--------:|----------:|------------:|
|     Trivial | 1.690 ns | 0.0646 ns | 0.0840 ns |  1.00 |    0.00 |         - |          NA |
|       AddSi | 5.642 ns | 0.1415 ns | 0.1889 ns |  3.35 |    0.26 |         - |          NA |
|   AddSiSame | 2.133 ns | 0.0223 ns | 0.0186 ns |  1.31 |    0.07 |         - |          NA |
| AddImperial | 5.261 ns | 0.0154 ns | 0.0136 ns |  3.20 |    0.16 |         - |          NA |
|    AddMixed | 5.435 ns | 0.0393 ns | 0.0348 ns |  3.31 |    0.16 |         - |          NA |
*/
