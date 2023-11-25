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
    private static readonly Energy energy = Energy.Of(3, Si<Milli, Watt>().Times(Si<Kilo, Second>()));

    [Benchmark(Baseline = true)]
    public Double TrivialImplementation() => trivialFoot.To(kiloMetre);

    [Benchmark]
    public Double QuantityImplementation() => foot.To(Si<Kilo, Metre>());

    [Benchmark]
    public Double QuantityToSame() => energy.To(Si<Milli, Watt>().Times(Si<Kilo, Second>()));

    [Benchmark]
    public Double QuantityToVeryDifferent() => energy.To(Si<Kilo, Watt>().Times(Si<Milli, Second>()));
}

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.113
  [Host]     : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2


| Method                  | Mean       | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------ |-----------:|----------:|----------:|------:|--------:|----------:|------------:|
| TrivialImplementation   |  0.6200 ns | 0.0304 ns | 0.0284 ns |  1.00 |    0.00 |         - |          NA |
| QuantityImplementation  |  4.0954 ns | 0.0377 ns | 0.0315 ns |  6.64 |    0.34 |         - |          NA |
| QuantityToSame          | 14.5757 ns | 0.0652 ns | 0.0610 ns | 23.55 |    1.02 |         - |          NA |
| QuantityToVeryDifferent | 16.9657 ns | 0.1247 ns | 0.1041 ns | 27.49 |    1.41 |         - |          NA |
*/
