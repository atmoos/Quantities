using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class AddingQuantities
{
    private Length sameMetric = Length.Of(-7, Si<Kilo, Metre>());
    private Length largeMetric = Length.Of(3, Si<Kilo, Metre>());
    private Length smallMetric = Length.Of(23, Si<Micro, Metre>());
    private Length largeImperial = Length.Of(-3, Imperial<Mile>());
    private Length smallImperial = Length.Of(55, Imperial<Inch>());
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

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.113
  [Host]     : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2


| Method      | Mean      | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------ |----------:|----------:|----------:|------:|--------:|----------:|------------:|
| Trivial     | 1.6263 ns | 0.0347 ns | 0.0325 ns |  1.00 |    0.00 |         - |          NA |
| AddSi       | 3.2011 ns | 0.0293 ns | 0.0274 ns |  1.97 |    0.05 |         - |          NA |
| AddSiSame   | 0.8805 ns | 0.0386 ns | 0.0361 ns |  0.54 |    0.03 |         - |          NA |
| AddImperial | 3.2012 ns | 0.0227 ns | 0.0213 ns |  1.97 |    0.04 |         - |          NA |
| AddMixed    | 3.3678 ns | 0.0720 ns | 0.0638 ns |  2.07 |    0.07 |         - |          NA |
*/
