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
    public Si<Metre> TrivialImplementation() => trivialFoot.To(kiloMetre);

    [Benchmark]
    public Length QuantityImplementation() => foot.To(Si<Kilo, Metre>());

    [Benchmark]
    public Energy QuantityToSame() => energy.To(Si<Milli, Watt>().Times(Si<Kilo, Second>()));

    [Benchmark]
    public Energy QuantityToVeryDifferent() => energy.To(Si<Kilo, Watt>().Times(Si<Milli, Second>()));
}

/* Summary

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 3.39GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method                  | Mean       | Error     | Ratio | Allocated | Alloc Ratio |
|------------------------ |-----------:|----------:|------:|----------:|------------:|
| TrivialImplementation   |  0.4851 ns | 0.0049 ns |  1.00 |         - |          NA |
| QuantityImplementation  |  2.8680 ns | 0.0113 ns |  5.91 |         - |          NA |
| QuantityToSame          |  7.1614 ns | 0.0594 ns | 14.76 |         - |          NA |
| QuantityToVeryDifferent | 11.3560 ns | 0.0348 ns | 23.41 |         - |          NA |
*/
