using BenchmarkDotNet.Attributes;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;
public class MapBenchmark
{
    private const Double feetToMetre = 0.3048;
    private static readonly Si<Metre> trivialKiloMetre = Si<Metre>.Of(Prefix.Kilo, 3);
    private static readonly Imperial<Foot> trivialFoot = new(10, feetToMetre);
    private static readonly Map kiloMetre = Build<Si<Kilo, Metre>>();
    private static readonly Map ångström = Build<Metric<Mega, Ångström>>();

    [Benchmark(Baseline = true)]
    public Double ProjectTrivial() => trivialKiloMetre.To(trivialFoot);

    [Benchmark]
    public Double ProjectOntoSame() => kiloMetre.Project(in kiloMetre, Math.Tau);

    [Benchmark]
    public Double ProjectOntoOther() => kiloMetre.Project(in ångström, Math.Tau);

    private static Map Build<TMeasure>()
        where TMeasure : IMeasure => new(TMeasure.Poly) {
            Injector = new Linear<TMeasure>(),
            Representation = TMeasure.Representation,
            Serialize = TMeasure.Write
        };
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
| ProjectTrivial   | 0.6773 ns | 0.0189 ns | 0.0177 ns |  1.00 |    0.00 |
| ProjectOntoSame  | 2.9818 ns | 0.0287 ns | 0.0254 ns |  4.39 |    0.11 |
| ProjectOntoOther | 2.9079 ns | 0.0243 ns | 0.0216 ns |  4.29 |    0.11 |

*/
