using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;
public class MeasureBenchmark
{
    private const Double feetToMetre = 0.3048;
    private static readonly Si<Metre> trivialKiloMetre = Si<Metre>.Of(Prefix.Kilo, 3);
    private static readonly Imperial<Foot> trivialFoot = new(10, feetToMetre);
    private static readonly Measure kiloMetre = Measure.Of<Si<Kilo, Metre>>();
    private static readonly Measure ångström = Measure.Of<Metric<Mega, Ångström>>();

    [Benchmark(Baseline = true)]
    public Double ProjectTrivial() => trivialKiloMetre.To(trivialFoot);

    [Benchmark]
    public Double ProjectOntoSame() => kiloMetre.Project(kiloMetre, Math.Tau);

    [Benchmark]
    public Double ProjectOntoOther() => kiloMetre.Project(ångström, Math.Tau);
}

/* Summary *

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method           | Mean      | Error     | Ratio | 
|----------------- |----------:|----------:|------:|-
| ProjectTrivial   | 0.5526 ns | 0.0089 ns |  1.00 | 
| ProjectOntoSame  | 0.8368 ns | 0.0073 ns |  1.51 | 
| ProjectOntoOther | 1.6007 ns | 0.0078 ns |  2.90 | 
*/
