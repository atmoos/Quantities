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
    public Double ProjectOntoSame() => kiloMetre.Project(kiloMetre, Math.Tau);

    [Benchmark]
    public Double ProjectOntoOther() => kiloMetre.Project(ångström, Math.Tau);

    private static Measure Build<TMeasure>()
        where TMeasure : IMeasure => Measure.Of<TMeasure>();
}
/* Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.113
  [Host]     : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2


| Method           | Mean      | Error     | StdDev    | Ratio | RatioSD |
|----------------- |----------:|----------:|----------:|------:|--------:|
| ProjectTrivial   | 0.6096 ns | 0.0288 ns | 0.0241 ns |  1.00 |    0.00 |
| ProjectOntoSame  | 1.0746 ns | 0.0110 ns | 0.0103 ns |  1.76 |    0.06 |
| ProjectOntoOther | 1.6110 ns | 0.0504 ns | 0.0472 ns |  2.64 |    0.14 |
*/
