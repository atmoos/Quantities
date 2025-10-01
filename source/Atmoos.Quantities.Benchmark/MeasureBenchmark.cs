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

BenchmarkDotNet v0.15.4, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 4.00GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method           | Mean       | Error     | Ratio | 
|----------------- |-----------:|----------:|------:|
| ProjectTrivial   |  0.6149 ns | 0.0656 ns |  1.01 | 
| ProjectOntoSame  | 35.9892 ns | 0.1488 ns | 59.09 | 
| ProjectOntoOther | 30.9130 ns | 0.1042 ns | 50.76 | 
*/
