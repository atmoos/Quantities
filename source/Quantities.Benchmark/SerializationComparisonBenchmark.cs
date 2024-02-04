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
Unknown processor
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.1 (8.0.123.58001), Arm64 RyuJIT AdvSIMD


| Method     | Mean     | Error     | Ratio | Allocated | Alloc Ratio |
|----------- |---------:|----------:|------:|----------:|------------:|
| TextJson   | 5.064 μs | 0.0027 μs |  1.00 |     801 B |        1.00 |
| Newtonsoft | 8.528 μs | 0.0025 μs |  1.68 |    4043 B |        5.05 |
*/
