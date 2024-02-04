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

BenchmarkDotNet v0.13.12, Arch Linux ARM
Unknown processor
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD


| Method                 | Mean       | Error   | Ratio | Allocated | Alloc Ratio |
|----------------------- |-----------:|--------:|------:|----------:|------------:|
| SystemTriple           |   499.8 ns | 0.13 ns |  1.00 |      40 B |        1.00 |
| SystemQuantity         | 1,895.6 ns | 0.35 ns |  3.79 |     160 B |        4.00 |
| PrefixedQuantity       | 2,296.8 ns | 0.66 ns |  4.60 |     224 B |        5.60 |
| FractionalQuantity     | 4,701.7 ns | 2.45 ns |  9.41 |     745 B |       18.62 |
| MultiplicativeQuantity | 4,645.9 ns | 3.76 ns |  9.29 |     745 B |       18.62 |
| PowerQuantity          | 3,840.3 ns | 1.35 ns |  7.68 |     592 B |       14.80 |
| ScalarPowerQuantity    | 2,504.2 ns | 0.54 ns |  5.01 |     232 B |        5.80 |
*/
