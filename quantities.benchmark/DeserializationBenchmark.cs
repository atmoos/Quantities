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
| SystemTriple           |   189.4 ns |  1.23 ns |  1.15 ns |  1.00 |    0.00 | 0.0095 |      40 B |        1.00 |
| SystemQuantity         |   705.0 ns |  3.35 ns |  2.97 ns |  3.72 |    0.03 | 0.0381 |     160 B |        4.00 |
| PrefixedQuantity       |   862.0 ns | 12.43 ns | 11.63 ns |  4.55 |    0.05 | 0.0534 |     224 B |        5.60 |
| FractionalQuantity     | 2,141.5 ns |  4.82 ns |  4.02 ns | 11.32 |    0.08 | 0.1793 |     752 B |       18.80 |
| MultiplicativeQuantity | 2,213.5 ns |  4.98 ns |  4.15 ns | 11.70 |    0.07 | 0.1793 |     752 B |       18.80 |
| PowerQuantity          | 1,930.0 ns | 37.47 ns | 35.05 ns | 10.19 |    0.17 | 0.1411 |     600 B |       15.00 |
| ScalarPowerQuantity    |   966.8 ns |  9.33 ns |  8.72 ns |  5.11 |    0.05 | 0.0553 |     232 B |        5.80 |
*/
