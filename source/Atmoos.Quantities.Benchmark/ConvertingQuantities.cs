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

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.103
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


| Method                  | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|------------------------ |----------:|----------:|------:|----------:|------------:|
| TrivialImplementation   |  2.073 ns | 0.0135 ns |  1.00 |         - |          NA |
| QuantityImplementation  |  3.579 ns | 0.0351 ns |  1.73 |         - |          NA |
| QuantityToSame          | 14.795 ns | 0.0288 ns |  7.14 |         - |          NA |
| QuantityToVeryDifferent | 15.621 ns | 0.0210 ns |  7.54 |         - |          NA |
*/
