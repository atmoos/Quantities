using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.Si;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Benchmark;

[MemoryDiagnoser(displayGenColumns: false)]
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

/* Summary

BenchmarkDotNet v0.15.7, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method                  | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|------------------------ |----------:|----------:|------:|----------:|------------:|
| TrivialImplementation   | 0.3508 ns | 0.0338 ns |  1.01 |         - |          NA |
| QuantityImplementation  | 4.3218 ns | 0.0095 ns | 12.41 |         - |          NA |
| QuantityToSame          | 8.0069 ns | 0.0129 ns | 22.99 |         - |          NA |
| QuantityToVeryDifferent | 8.4917 ns | 0.0200 ns | 24.38 |         - |          NA |
*/
