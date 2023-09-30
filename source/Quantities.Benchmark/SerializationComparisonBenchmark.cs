using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using Quantities.Prefixes;
using Quantities.Serialization.Newtonsoft;
using Quantities.Units.Si;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class SerializationComparisonBenchmark
{
    private static readonly JsonSerializerSettings settings = new JsonSerializerSettings().EnableQuantities();
    private static readonly String fractionalQuantity = Velocity.Of(Math.PI).Si<Kilo, Metre>().Per.Si<Milli, Second>().Serialize();

    [Benchmark(Baseline = true)]
    public Velocity TextJson() => fractionalQuantity.Deserialize<Velocity>();

    [Benchmark]
    public Velocity Newtonsoft() => fractionalQuantity.Deserialize<Velocity>(settings);
}

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method     | Mean     | Error     | StdDev    | Ratio | Gen0   | Allocated | Alloc Ratio |
|----------- |---------:|----------:|----------:|------:|-------:|----------:|------------:|
| TextJson   | 2.353 us | 0.0129 us | 0.0121 us |  1.00 | 0.1907 |     808 B |        1.00 |
| Newtonsoft | 3.367 us | 0.0172 us | 0.0161 us |  1.43 | 0.9651 |    4048 B |        5.01 |
*/
