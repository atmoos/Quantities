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
| TrivialImplementation   |  0.5988 ns | 0.0069 ns | 0.0058 ns |  1.00 |    0.00 |         - |          NA |
| QuantityImplementation  |  4.0346 ns | 0.0202 ns | 0.0158 ns |  6.74 |    0.06 |         - |          NA |
| QuantityToSame          | 14.9515 ns | 0.1545 ns | 0.1370 ns | 24.94 |    0.35 |         - |          NA |
| QuantityToVeryDifferent | 17.1767 ns | 0.0614 ns | 0.0575 ns | 28.67 |    0.28 |         - |          NA |
*/
