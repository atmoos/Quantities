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

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


 Method           | Mean      | Error     | StdDev    | Ratio | RatioSD |
----------------- |----------:|----------:|----------:|------:|--------:|
 ProjectTrivial   | 0.3750 ns | 0.0075 ns | 0.0067 ns |  1.00 |    0.00 |
 ProjectOntoSame  | 0.9192 ns | 0.0162 ns | 0.0144 ns |  2.45 |    0.05 |
 ProjectOntoOther | 1.5805 ns | 0.0070 ns | 0.0062 ns |  4.22 |    0.08 |
*/
