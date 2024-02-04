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
ARMv7 Processor rev 4 (v7l), 4 logical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT


| Method                 | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|----------------------- |----------:|----------:|------:|----------:|------------:|
| SystemTriple           |  2.700 μs | 0.0053 μs |  1.00 |      24 B |        1.00 |
| SystemQuantity         |  8.098 μs | 0.0127 μs |  3.00 |     104 B |        4.33 |
| PrefixedQuantity       |  9.545 μs | 0.0160 μs |  3.53 |     148 B |        6.17 |
| FractionalQuantity     | 19.067 μs | 0.0662 μs |  7.06 |     452 B |       18.83 |
| MultiplicativeQuantity | 19.459 μs | 0.0719 μs |  7.21 |     444 B |       18.50 |
| PowerQuantity          | 16.137 μs | 0.0819 μs |  5.97 |     347 B |       14.46 |
| ScalarPowerQuantity    | 10.544 μs | 0.0098 μs |  3.90 |     156 B |        6.50 |
*/
