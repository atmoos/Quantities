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

BenchmarkDotNet v0.15.3, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method     | Mean     | Error     | Ratio | Allocated | Alloc Ratio |
|----------- |---------:|----------:|------:|----------:|------------:|
| TextJson   | 4.942 μs | 0.0141 μs |  1.00 |     504 B |        1.00 |
| Newtonsoft | 9.319 μs | 0.0297 μs |  1.89 |    3896 B |        7.73 |
*/
