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
    private static readonly Energy energy = Energy.Of(3).Si<Milli, Watt>().Times.Si<Kilo, Second>();

    [Benchmark(Baseline = true)]
    public Double TrivialImplementation() => trivialFoot.To(kiloMetre);

    [Benchmark]
    public Double QuantityImplementation() => foot.To(Si<Kilo, Metre>());

    [Benchmark]
    public Double QuantityToSame() => energy.To.Si<Milli, Watt>().Times.Si<Kilo, Second>();

    [Benchmark]
    public Double QuantityToVeryDifferent() => energy.To.Si<Kilo, Watt>().Times.Si<Milli, Second>();
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
| TrivialImplementation   |  0.5770 ns | 0.0167 ns | 0.0140 ns |  1.00 |    0.00 |         - |          NA |
| QuantityImplementation  |  2.9454 ns | 0.0453 ns | 0.0401 ns |  5.11 |    0.11 |         - |          NA |
| QuantityToSame          | 19.9317 ns | 0.0646 ns | 0.0504 ns | 34.52 |    0.83 |         - |          NA |
| QuantityToVeryDifferent | 25.7943 ns | 0.2820 ns | 0.2637 ns | 44.66 |    1.31 |         - |          NA |
*/
