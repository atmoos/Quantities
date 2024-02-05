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

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-1260P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method             | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|------:|----------:|------------:|
| QuantityToImperial |  1.925 ns | 0.0554 ns |  1.00 |         - |          NA |
| UnitsNetToImperial | 36.503 ns | 0.5333 ns | 18.94 |      48 B |          NA |
|                    |           |           |       |           |             |
| QuantityToSame     |  1.924 ns | 0.0620 ns |  1.00 |         - |          NA |
| UnitsNetToSame     | 11.712 ns | 0.0609 ns |  5.76 |         - |          NA |
|                    |           |           |       |           |             |
| QuantityToSi       |  1.946 ns | 0.0489 ns |  1.00 |         - |          NA |
| UnitsNetToSi       | 38.461 ns | 0.4820 ns | 19.77 |      48 B |          NA |
*/
