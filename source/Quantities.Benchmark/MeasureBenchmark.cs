using BenchmarkDotNet.Attributes;
using Quantities.Core;
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
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method           | Mean      | Error     | StdDev    | Ratio | RatioSD |
|----------------- |----------:|----------:|----------:|------:|--------:|
| ProjectTrivial   | 0.6425 ns | 0.0063 ns | 0.0056 ns |  1.00 |    0.00 |
| ProjectOntoSame  | 1.9094 ns | 0.0128 ns | 0.0106 ns |  2.97 |    0.03 |
| ProjectOntoOther | 2.3987 ns | 0.0180 ns | 0.0160 ns |  3.73 |    0.04 |
*/
