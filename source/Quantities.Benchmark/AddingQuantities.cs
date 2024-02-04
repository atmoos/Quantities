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

BenchmarkDotNet v0.13.12, Arch Linux ARM
ARMv7 Processor rev 4 (v7l), 4 logical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT


| Method      | Mean      | Error    | Ratio | Allocated | Alloc Ratio |
|------------ |----------:|---------:|------:|----------:|------------:|
| Trivial     |  28.37 ns | 0.438 ns |  1.00 |         - |          NA |
| AddSi       | 257.24 ns | 0.593 ns |  9.07 |         - |          NA |
| AddSiSame   |  20.70 ns | 0.015 ns |  0.73 |         - |          NA |
| AddImperial | 246.59 ns | 0.636 ns |  8.69 |         - |          NA |
| AddMixed    | 252.13 ns | 0.655 ns |  8.89 |         - |          NA |
*/
