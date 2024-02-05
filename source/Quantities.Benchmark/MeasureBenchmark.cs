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

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-1260P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method           | Mean      | Error     | Ratio | 
|----------------- |----------:|----------:|------:|-
| ProjectTrivial   | 0.1653 ns | 0.0192 ns |  1.00 | 
| ProjectOntoSame  | 0.5416 ns | 0.0174 ns |  3.30 | 
| ProjectOntoOther | 0.4755 ns | 0.0372 ns |  2.91 | 
*/
