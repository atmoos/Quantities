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
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method     | Mean     | Error     | Ratio | Allocated | Alloc Ratio |
|----------- |---------:|----------:|------:|----------:|------------:|
| TextJson   | 1.853 μs | 0.0025 μs |  1.00 |   1.06 KB |        1.00 |
| Newtonsoft | 2.763 μs | 0.0062 μs |  1.49 |   4.35 KB |        4.10 |
*/
