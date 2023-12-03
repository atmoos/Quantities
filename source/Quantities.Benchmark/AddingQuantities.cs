using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;

namespace Quantities.Benchmark;

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

/* Summary *

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method      | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|------------ |----------:|----------:|------:|----------:|------------:|
| Trivial     | 1.4131 ns | 0.0185 ns |  1.00 |         - |          NA |
| AddSi       | 2.5279 ns | 0.0790 ns |  1.78 |         - |          NA |
| AddSiSame   | 0.7713 ns | 0.0469 ns |  0.54 |         - |          NA |
| AddImperial | 2.4031 ns | 0.0066 ns |  1.70 |         - |          NA |
| AddMixed    | 2.4445 ns | 0.0193 ns |  1.73 |         - |          NA |
*/
