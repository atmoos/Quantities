using BenchmarkDotNet.Configs;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using nLength = UnitsNet.Length;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser(displayGenColumns: false)]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class ValueConversionComparison
{
    private const String ToSi = nameof(ToSi);
    private const String ToImperial = nameof(ToImperial);
    private const String ToSame = nameof(ToSame);
    private static readonly Length si = Length.Of(3, Si<Milli, Metre>());
    private static readonly Length imperial = Length.Of(4, Imperial<Foot>());
    private static readonly nLength nSi = nLength.FromMillimeters(3);
    private static readonly nLength nImperial = nLength.FromFeet(4);

    [BenchmarkCategory(ToSi), Benchmark(Baseline = true)]
    public Double QuantityToSi() => imperial.To(Si<Milli, Metre>());
    [BenchmarkCategory(ToSi), Benchmark]
    public Double UnitsNetToSi() => nImperial.Millimeters;

    [BenchmarkCategory(ToImperial), Benchmark(Baseline = true)]
    public Double QuantityToImperial() => si.To(Imperial<Foot>());
    [BenchmarkCategory(ToImperial), Benchmark]
    public Double UnitsNetToImperial() => nSi.Feet;

    [BenchmarkCategory(ToSame), Benchmark(Baseline = true)]
    public Double QuantityToSame() => imperial.To(Imperial<Foot>());
    [BenchmarkCategory(ToSame), Benchmark]
    public Double UnitsNetToSame() => nImperial.Feet;
}

/* Summary *

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-1260P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method             | Mean       | Error     | Ratio | Allocated | Alloc Ratio |
|------------------- |-----------:|----------:|------:|----------:|------------:|
| QuantityToImperial |  0.7591 ns | 0.0442 ns |  1.00 |         - |          NA |
| UnitsNetToImperial | 39.1487 ns | 0.4276 ns | 51.91 |      48 B |          NA |
|                    |            |           |       |           |             |
| QuantityToSame     |  0.3554 ns | 0.0148 ns |  1.00 |         - |          NA |
| UnitsNetToSame     |  0.1525 ns | 0.0145 ns |  0.43 |         - |          NA |
|                    |            |           |       |           |             |
| QuantityToSi       |  0.7803 ns | 0.0433 ns |  1.00 |         - |          NA |
| UnitsNetToSi       | 39.7448 ns | 0.3878 ns | 51.33 |      48 B |          NA |
*/
