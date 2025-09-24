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

BenchmarkDotNet v0.15.3, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method                 | Mean       | Error    | Ratio | Allocated | Alloc Ratio |
|----------------------- |-----------:|---------:|------:|----------:|------------:|
| SystemTriple           |   339.5 ns |  0.97 ns |  1.00 |      40 B |        1.00 |
| SystemQuantity         | 1,978.5 ns |  7.53 ns |  5.83 |     136 B |        3.40 |
| PrefixedQuantity       | 2,426.4 ns |  8.53 ns |  7.15 |     160 B |        4.00 |
| FractionalQuantity     | 4,430.3 ns | 17.18 ns | 13.05 |     488 B |       12.20 |
| MultiplicativeQuantity | 4,049.8 ns |  7.78 ns | 11.93 |     488 B |       12.20 |
| PowerQuantity          | 2,901.6 ns |  7.19 ns |  8.55 |     160 B |        4.00 |
| ScalarPowerQuantity    | 2,698.5 ns |  7.07 ns |  7.95 |     168 B |        4.20 |
*/
