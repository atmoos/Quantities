using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using Quantities.Prefixes;
using Quantities.Quantities;
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

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.107
  [Host]     : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2


|     Method |     Mean |     Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|----------- |---------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
|   TextJson | 2.237 us | 0.0217 us | 0.0193 us |  1.00 |    0.00 | 0.1869 |     784 B |        1.00 |
| Newtonsoft | 3.156 us | 0.0349 us | 0.0326 us |  1.41 |    0.02 | 0.9613 |    4024 B |        5.13 |

*/
