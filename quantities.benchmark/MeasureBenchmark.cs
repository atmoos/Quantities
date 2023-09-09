using BenchmarkDotNet.Attributes;
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
    private static readonly Measure kiloMetre = Build<Si<Kilo, Metre>>();
    private static readonly Measure ångström = Build<Metric<Mega, Ångström>>();

    [Benchmark(Baseline = true)]
    public Double ProjectTrivial() => trivialKiloMetre.To(trivialFoot);

    [Benchmark]
    public Double ProjectOntoSame() => kiloMetre.Project(kiloMetre) * Math.Tau;

    [Benchmark]
    public Double ProjectOntoOther() => kiloMetre.Project(ångström) * Math.Tau;

    private static Measure Build<TMeasure>()
        where TMeasure : IMeasure => Measure.Of<TMeasure>();
}
/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.110
  [Host]     : .NET 7.0.10 (7.0.1023.41001), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.10 (7.0.1023.41001), X64 RyuJIT AVX2


| Method           | Mean      | Error     | StdDev    | Ratio | RatioSD |
|----------------- |----------:|----------:|----------:|------:|--------:|
| ProjectTrivial   | 0.6385 ns | 0.0173 ns | 0.0153 ns |  1.00 |    0.00 |
| ProjectOntoSame  | 1.9520 ns | 0.0273 ns | 0.0242 ns |  3.06 |    0.08 |
| ProjectOntoOther | 1.9835 ns | 0.0343 ns | 0.0321 ns |  3.11 |    0.09 |

*/
