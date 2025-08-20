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

BenchmarkDotNet v0.15.2, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.109
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


| Method     | Mean     | Error     | Ratio | Allocated | Alloc Ratio |
|----------- |---------:|----------:|------:|----------:|------------:|
| TextJson   | 3.713 μs | 0.0108 μs |  1.00 |     809 B |        1.00 |
| Newtonsoft | 5.947 μs | 0.0134 μs |  1.60 |    4051 B |        5.01 |
*/
