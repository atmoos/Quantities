using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Serialization.Newtonsoft;
using Atmoos.Quantities.Units.Si;
using Newtonsoft.Json;

namespace Atmoos.Quantities.Benchmark;

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

/* Summary

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.111
  [Host]     : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method     | Mean     | Error     | Ratio | Allocated | Alloc Ratio |
|----------- |---------:|----------:|------:|----------:|------------:|
| TextJson   | 1.866 μs | 0.0079 μs |  1.00 |   1.06 KB |        1.00 |
| Newtonsoft | 2.804 μs | 0.0115 μs |  1.50 |   4.35 KB |        4.10 |
*/
