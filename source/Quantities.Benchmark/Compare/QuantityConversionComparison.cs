using BenchmarkDotNet.Configs;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using UnitsNet.Units;
using nLength = UnitsNet.Length;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser(displayGenColumns: false)]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class QuantityConversionComparison
{
    private const String ToSi = nameof(ToSi);
    private const String ToImperial = nameof(ToImperial);
    private const String ToSame = nameof(ToSame);
    private static readonly Length si = Length.Of(3, Si<Milli, Metre>());
    private static readonly Length imperial = Length.Of(4, Imperial<Foot>());
    private static readonly nLength nSi = nLength.FromMillimeters(3);
    private static readonly nLength nImperial = nLength.FromFeet(4);

    [BenchmarkCategory(ToSi), Benchmark(Baseline = true)]
    public Length QuantityToSi() => imperial.To(Si<Milli, Metre>());
    [BenchmarkCategory(ToSi), Benchmark]
    public nLength UnitsNetToSi() => nImperial.ToUnit(LengthUnit.Millimeter);

    [BenchmarkCategory(ToImperial), Benchmark(Baseline = true)]
    public Length QuantityToImperial() => si.To(Imperial<Foot>());
    [BenchmarkCategory(ToImperial), Benchmark]
    public nLength UnitsNetToImperial() => nSi.ToUnit(LengthUnit.Foot);

    [BenchmarkCategory(ToSame), Benchmark(Baseline = true)]
    public Length QuantityToSame() => imperial.To(Imperial<Foot>());
    [BenchmarkCategory(ToSame), Benchmark]
    public nLength UnitsNetToSame() => nImperial.ToUnit(LengthUnit.Foot);
}

/* Summary *

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method             | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|------:|----------:|------------:|
| QuantityToImperial |  2.887 ns | 0.0086 ns |  1.00 |         - |          NA |
| UnitsNetToImperial | 75.957 ns | 0.2416 ns | 26.30 |      48 B |          NA |
|                    |           |           |       |           |             |
| QuantityToSame     |  2.648 ns | 0.0283 ns |  1.00 |         - |          NA |
| UnitsNetToSame     | 14.851 ns | 0.0897 ns |  5.61 |         - |          NA |
|                    |           |           |       |           |             |
| QuantityToSi       |  2.783 ns | 0.0400 ns |  1.00 |         - |          NA |
| UnitsNetToSi       | 74.351 ns | 0.0849 ns | 26.72 |      48 B |          NA |
*/
