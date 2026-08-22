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

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.111
  [Host]     : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method           | Mean      | Error     | Ratio | 
|----------------- |----------:|----------:|------:|
| ProjectTrivial   | 0.3485 ns | 0.0093 ns |  1.00 | 
| ProjectOntoSame  | 5.0620 ns | 0.0095 ns | 14.53 | 
| ProjectOntoOther | 5.7286 ns | 0.0162 ns | 16.45 | 
*/
