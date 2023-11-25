using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
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

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.113
  [Host]     : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2


| Method                 | Mean       | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------------------- |-----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| SystemTriple           |   195.6 ns |  1.90 ns |  1.68 ns |  1.00 |    0.00 | 0.0095 |      40 B |        1.00 |
| SystemQuantity         |   758.5 ns |  9.67 ns |  9.04 ns |  3.88 |    0.07 | 0.0381 |     160 B |        4.00 |
| PrefixedQuantity       |   847.8 ns | 13.04 ns | 11.56 ns |  4.34 |    0.09 | 0.0534 |     224 B |        5.60 |
| FractionalQuantity     | 2,164.2 ns | 41.96 ns | 56.02 ns | 11.03 |    0.31 | 0.1755 |     744 B |       18.60 |
| MultiplicativeQuantity | 2,102.1 ns | 14.80 ns | 13.12 ns | 10.75 |    0.10 | 0.1755 |     744 B |       18.60 |
| PowerQuantity          | 1,961.4 ns |  8.74 ns |  7.74 ns | 10.03 |    0.11 | 0.1411 |     592 B |       14.80 |
| ScalarPowerQuantity    |   934.2 ns |  2.81 ns |  2.63 ns |  4.78 |    0.04 | 0.0553 |     232 B |        5.80 |
*/
