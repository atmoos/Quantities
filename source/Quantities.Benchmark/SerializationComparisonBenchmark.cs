using Newtonsoft.Json;
using Quantities.Prefixes;
using Quantities.Serialization.Newtonsoft;
using Quantities.Units.Si;

namespace Quantities.Benchmark;

[MemoryDiagnoser(displayGenColumns: false)]
public class SerializationComparisonBenchmark
{
    private static readonly JsonSerializerSettings settings = new JsonSerializerSettings().EnableQuantities();
    private static readonly String fractionalQuantity = Velocity.Of(Math.PI, Si<Kilo, Metre>().Per(Si<Milli, Second>())).Serialize();

    [Benchmark(Baseline = true)]
    public Velocity TextJson() => fractionalQuantity.Deserialize<Velocity>();

    [Benchmark]
    public Velocity Newtonsoft() => fractionalQuantity.Deserialize<Velocity>(settings);
}

/* Summary *

BenchmarkDotNet v0.13.12, Arch Linux ARM
ARMv7 Processor rev 4 (v7l), 4 logical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm RyuJIT


| Method     | Mean     | Error    | Ratio | Allocated | Alloc Ratio |
|----------- |---------:|---------:|------:|----------:|------------:|
| TextJson   | 21.08 μs | 0.207 μs |  1.00 |     488 B |        1.00 |
| Newtonsoft | 45.01 μs | 0.288 μs |  2.14 |    3362 B |        6.89 |
*/
