using BenchmarkDotNet.Attributes;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class DeserializationBenchmark
{
    private static readonly String triple = Triple().Serialize();
    private static readonly String simpleQuantity = Length.Of(Math.PI).Si<Metre>().Serialize();
    private static readonly String prefixedQuantity = Length.Of(Math.PI).Si<Kilo, Metre>().Serialize();
    private static readonly String fractionalQuantity = Velocity.Of(Math.PI).Si<Kilo, Metre>().Per.Metric<Hour>().Serialize();
    private static readonly String multiplicativeQuantity = Energy.Of(Math.PI).Si<Kilo, Watt>().Times.Metric<Hour>().Serialize();
    private static readonly String powerQuantity = Volume.Of(Math.PI).Cubic.Si<Deci, Metre>().Serialize();
    private static readonly String scalarPowerQuantity = Volume.Of(Math.PI).Metric<Deci, Litre>().Serialize();

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
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method                 | Mean       | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------------------- |-----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| SystemTriple           |   202.7 ns |  3.21 ns |  2.85 ns |  1.00 |    0.00 | 0.0095 |      40 B |        1.00 |
| SystemQuantity         |   750.8 ns |  3.76 ns |  3.51 ns |  3.70 |    0.04 | 0.0381 |     160 B |        4.00 |
| PrefixedQuantity       |   863.0 ns |  4.15 ns |  3.68 ns |  4.26 |    0.06 | 0.0534 |     224 B |        5.60 |
| FractionalQuantity     | 2,248.0 ns |  9.45 ns |  8.38 ns | 11.09 |    0.17 | 0.1831 |     768 B |       19.20 |
| MultiplicativeQuantity | 2,290.0 ns |  9.35 ns |  8.75 ns | 11.30 |    0.16 | 0.1831 |     768 B |       19.20 |
| PowerQuantity          | 2,033.7 ns | 33.68 ns | 31.50 ns | 10.03 |    0.14 | 0.1488 |     624 B |       15.60 |
| ScalarPowerQuantity    |   953.5 ns |  5.50 ns |  5.15 ns |  4.71 |    0.07 | 0.0553 |     232 B |        5.80 |
*/
