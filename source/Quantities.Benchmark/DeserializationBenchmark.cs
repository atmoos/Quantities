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

/* Summary *

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


 Method                 | Mean       | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
----------------------- |-----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
 SystemTriple           |   169.8 ns |  0.39 ns |  0.32 ns |  1.00 |    0.00 | 0.0095 |      40 B |        1.00 |
 SystemQuantity         |   587.0 ns |  1.95 ns |  1.73 ns |  3.45 |    0.01 | 0.0381 |     160 B |        4.00 |
 PrefixedQuantity       |   735.2 ns |  2.51 ns |  2.23 ns |  4.33 |    0.02 | 0.0534 |     224 B |        5.60 |
 FractionalQuantity     | 1,319.3 ns |  3.99 ns |  3.54 ns |  7.77 |    0.02 | 0.1774 |     744 B |       18.60 |
 MultiplicativeQuantity | 1,348.7 ns | 21.16 ns | 19.80 ns |  7.92 |    0.12 | 0.1774 |     744 B |       18.60 |
 PowerQuantity          | 1,108.7 ns |  4.29 ns |  4.01 ns |  6.52 |    0.02 | 0.1411 |     592 B |       14.80 |
 ScalarPowerQuantity    |   807.4 ns |  2.54 ns |  2.26 ns |  4.75 |    0.01 | 0.0553 |     232 B |        5.80 |
*/
