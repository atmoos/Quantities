using BenchmarkDotNet.Attributes;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;

namespace Quantities.Benchmark;

[MemoryDiagnoser]
public class ConvertingQuantities
{
    private const Double feetToMetre = 0.3048;
    private static readonly Si<Metre> kiloMetre = Si<Metre>.Of(Prefix.Kilo, 3);
    private static readonly Imperial<Foot> trivialFoot = new(10, feetToMetre);
    private static readonly Length foot = Length.Of(10, Imperial<Foot>());
    private static readonly Energy energy = Energy.Of(3, Si<Milli, Watt>().Dot(Si<Kilo, Second>()));

    [Benchmark(Baseline = true)]
    public Double TrivialImplementation() => trivialFoot.To(kiloMetre);

    [Benchmark]
    public Double QuantityImplementation() => foot.To(Si<Kilo, Metre>());

    [Benchmark]
    public Double QuantityToSame() => energy.To(Si<Milli, Watt>().Dot(Si<Kilo, Second>()));

    [Benchmark]
    public Double QuantityToVeryDifferent() => energy.To(Si<Kilo, Watt>().Dot(Si<Milli, Second>()));
}

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method                  | Mean       | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------ |-----------:|----------:|----------:|------:|--------:|----------:|------------:|
| TrivialImplementation   |  0.6493 ns | 0.0171 ns | 0.0152 ns |  1.00 |    0.00 |         - |          NA |
| QuantityImplementation  |  2.9082 ns | 0.0899 ns | 0.0923 ns |  4.45 |    0.14 |         - |          NA |
| QuantityToSame          | 26.5881 ns | 0.1631 ns | 0.1525 ns | 40.96 |    1.02 |         - |          NA |
| QuantityToVeryDifferent | 27.2572 ns | 0.1387 ns | 0.1083 ns | 42.25 |    0.86 |         - |          NA |
*/
