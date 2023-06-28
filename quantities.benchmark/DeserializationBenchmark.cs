using System;
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
    private static readonly String multiplicativeQuantity = Energy.Of(Math.PI).Metric<Kilo, Watt, Hour>().Serialize();
    private static readonly String powerQuantity = Volume.Of(Math.PI).Cubic.Si<Deci, Metre>().Serialize();

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

    private static (Double value, String prefix, String unit) Triple() => (Math.PI, "K", "m");
}

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.107
  [Host]     : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2


|                 Method |       Mean |    Error |   StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|----------------------- |-----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
|           SystemTriple |   205.6 ns |  0.94 ns |  0.84 ns |  1.00 |    0.00 | 0.0095 |      40 B |        1.00 |
|         SystemQuantity |   727.0 ns |  3.62 ns |  3.03 ns |  3.54 |    0.02 | 0.0381 |     160 B |        4.00 |
|       PrefixedQuantity |   914.9 ns | 16.44 ns | 13.73 ns |  4.45 |    0.07 | 0.0534 |     224 B |        5.60 |
|     FractionalQuantity | 4,447.2 ns | 20.33 ns | 16.97 ns | 21.64 |    0.11 | 0.4349 |    1849 B |       46.23 |
| MultiplicativeQuantity | 4,226.6 ns | 32.53 ns | 30.43 ns | 20.57 |    0.18 | 0.4349 |    1849 B |       46.23 |
|          PowerQuantity | 1,930.0 ns | 37.31 ns | 36.65 ns |  9.39 |    0.22 | 0.1373 |     584 B |       14.60 |
*/