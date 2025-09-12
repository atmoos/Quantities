using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.Si;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Benchmark;

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
    public Double ProjectOntoSame() => kiloMetre / kiloMetre * Math.Tau;

    [Benchmark]
    public Double ProjectOntoOther() => kiloMetre / ångström * Math.Tau;
}

/* Summary

BenchmarkDotNet v0.15.2, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.109
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


| Method           | Mean       | Error     | Ratio | 
|----------------- |-----------:|----------:|------:|
| ProjectTrivial   |  0.6525 ns | 0.0751 ns |  1.01 | 
| ProjectOntoSame  | 31.5157 ns | 0.2301 ns | 48.85 | 
| ProjectOntoOther | 31.6438 ns | 0.1123 ns | 49.05 | 
*/
