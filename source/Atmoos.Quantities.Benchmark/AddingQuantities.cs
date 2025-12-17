using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.Si;
using BenchmarkDotNet.Diagnosers;

namespace Atmoos.Quantities.Benchmark;

[MemoryDiagnoser(displayGenColumns: false)]
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
    public Si<Metre> Trivial() => this.largeTrivial + this.smallTrivial;

    [Benchmark]
    public Length AddSi() => this.largeMetric + this.smallMetric;

    [Benchmark]
    public Length AddSiSame() => this.sameMetric + this.largeMetric;

    [Benchmark]
    public Length AddImperial() => this.largeImperial + this.smallImperial;

    [Benchmark]
    public Length AddMixed() => this.smallMetric + this.largeImperial;
}

/* Summary

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method      | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|------------ |----------:|----------:|------:|----------:|------------:|
| Trivial     | 1.2829 ns | 0.0205 ns |  1.00 |         - |          NA |
| AddSi       | 2.3519 ns | 0.0157 ns |  1.83 |         - |          NA |
| AddSiSame   | 0.7100 ns | 0.0098 ns |  0.55 |         - |          NA |
| AddImperial | 2.4191 ns | 0.0155 ns |  1.89 |         - |          NA |
| AddMixed    | 2.4079 ns | 0.0126 ns |  1.88 |         - |          NA |
*/
