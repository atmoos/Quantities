using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;

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

/* Summary *

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method                 | Mean       | Error   | Ratio | Allocated | Alloc Ratio |
|----------------------- |-----------:|--------:|------:|----------:|------------:|
| SystemTriple           |   165.1 ns | 0.26 ns |  1.00 |      40 B |        1.00 |
| SystemQuantity         |   573.2 ns | 2.34 ns |  3.47 |     160 B |        4.00 |
| PrefixedQuantity       |   698.0 ns | 1.61 ns |  4.23 |     224 B |        5.60 |
| FractionalQuantity     | 1,286.4 ns | 1.16 ns |  7.79 |     744 B |       18.60 |
| MultiplicativeQuantity | 1,414.5 ns | 7.17 ns |  8.57 |     744 B |       18.60 |
| PowerQuantity          | 1,101.9 ns | 2.16 ns |  6.67 |     592 B |       14.80 |
| ScalarPowerQuantity    |   781.6 ns | 2.93 ns |  4.73 |     232 B |        5.80 |
*/
