using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Si;
using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Benchmark;

[MemoryDiagnoser(displayGenColumns: false)]
public class DeserializationBenchmark
{
    private static readonly String triple = Triple().Serialize();
    private static readonly String simpleQuantity = Length.Of(Math.PI, Si<Metre>()).Serialize();
    private static readonly String prefixedQuantity = Length.Of(Math.PI, Si<Kilo, Metre>()).Serialize();
    private static readonly String fractionalQuantity = Velocity.Of(Math.PI, Si<Kilo, Metre>().Per(Metric<Hour>())).Serialize();
    private static readonly String multiplicativeQuantity = Energy.Of(Math.PI, Si<Kilo, Watt>().Times(Metric<Hour>())).Serialize();
    private static readonly String powerQuantity = Volume.Of(Math.PI, Cubic(Si<Deci, Metre>())).Serialize();
    private static readonly String scalarPowerQuantity = Volume.Of(Math.PI, Metric<Deci, Litre>()).Serialize();

    [Benchmark(Baseline = true)]
    public (Double, String, String) SystemTriple() => triple.Deserialize<(Double, String, String)>();

    [Benchmark]
    public Length SystemQuantity() => simpleQuantity.Deserialize<Length>();

    [Benchmark]
    public Length PrefixedQuantity() => prefixedQuantity.Deserialize<Length>();

    [Benchmark]
    public Velocity FractionalQuantity() => fractionalQuantity.Deserialize<Velocity>();

    [Benchmark]
    public Energy MultiplicativeQuantity() => multiplicativeQuantity.Deserialize<Energy>();

    [Benchmark]
    public Volume PowerQuantity() => powerQuantity.Deserialize<Volume>();

    [Benchmark]
    public Volume ScalarPowerQuantity() => scalarPowerQuantity.Deserialize<Volume>();

    private static (Double value, String prefix, String unit) Triple() => (Math.PI, "K", "m");
}

/* Summary

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.111
  [Host]     : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method                 | Mean        | Error    | Ratio | Allocated | Alloc Ratio |
|----------------------- |------------:|---------:|------:|----------:|------------:|
| SystemTriple           |    95.70 ns | 0.100 ns |  1.00 |      40 B |        1.00 |
| SystemQuantity         |   629.59 ns | 0.760 ns |  6.58 |     136 B |        3.40 |
| PrefixedQuantity       |   751.49 ns | 9.819 ns |  7.85 |     160 B |        4.00 |
| FractionalQuantity     | 1,777.99 ns | 4.545 ns | 18.58 |    1072 B |       26.80 |
| MultiplicativeQuantity | 1,635.35 ns | 7.143 ns | 17.09 |    1072 B |       26.80 |
| PowerQuantity          |   881.82 ns | 3.613 ns |  9.21 |     160 B |        4.00 |
| ScalarPowerQuantity    |   841.39 ns | 0.953 ns |  8.79 |     168 B |        4.20 |
*/
