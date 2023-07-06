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
|           SystemTriple |   206.1 ns |  0.86 ns |  0.76 ns |  1.00 |    0.00 | 0.0095 |      40 B |        1.00 |
|         SystemQuantity |   748.0 ns |  2.96 ns |  2.31 ns |  3.63 |    0.02 | 0.0381 |     160 B |        4.00 |
|       PrefixedQuantity |   852.1 ns |  6.16 ns |  4.81 ns |  4.14 |    0.03 | 0.0534 |     224 B |        5.60 |
|     FractionalQuantity | 2,121.4 ns | 14.59 ns | 12.93 ns | 10.29 |    0.07 | 0.1717 |     728 B |       18.20 |
| MultiplicativeQuantity | 2,060.6 ns |  9.53 ns |  8.45 ns | 10.00 |    0.05 | 0.1717 |     728 B |       18.20 |
|          PowerQuantity | 1,979.2 ns | 10.44 ns |  9.77 ns |  9.60 |    0.06 | 0.1373 |     584 B |       14.60 |
*/