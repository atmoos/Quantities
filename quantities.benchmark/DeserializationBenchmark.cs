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
|           SystemTriple |   206.8 ns |  2.60 ns |  2.31 ns |  1.00 |    0.00 | 0.0095 |      40 B |        1.00 |
|         SystemQuantity | 1,699.8 ns | 10.09 ns |  9.44 ns |  8.22 |    0.12 | 0.1736 |     728 B |       18.20 |
|       PrefixedQuantity | 2,063.8 ns | 40.82 ns | 54.50 ns |  9.89 |    0.24 | 0.1945 |     816 B |       20.40 |
|     FractionalQuantity | 4,241.7 ns | 15.14 ns | 13.42 ns | 20.52 |    0.22 | 0.4425 |    1873 B |       46.83 |
| MultiplicativeQuantity | 4,280.3 ns | 21.53 ns | 20.14 ns | 20.72 |    0.25 | 0.4425 |    1873 B |       46.83 |
|          PowerQuantity | 3,258.7 ns |  8.79 ns |  7.79 ns | 15.76 |    0.17 | 0.2785 |    1176 B |       29.40 |
*/