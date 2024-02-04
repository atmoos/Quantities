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

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method             | Mean       | Error     | Ratio | Allocated | Alloc Ratio |
|------------------- |-----------:|----------:|------:|----------:|------------:|
| QuantityToImperial |  3.6227 ns | 0.0133 ns |  1.00 |         - |          NA |
| UnitsNetToImperial | 81.2674 ns | 0.2776 ns | 22.43 |      48 B |          NA |
|                    |            |           |       |           |             |
| QuantityToSame     |  1.4156 ns | 0.0133 ns |  1.00 |         - |          NA |
| UnitsNetToSame     |  0.6307 ns | 0.0131 ns |  0.45 |         - |          NA |
|                    |            |           |       |           |             |
| QuantityToSi       |  3.5307 ns | 0.0135 ns |  1.00 |         - |          NA |
| UnitsNetToSi       | 84.8318 ns | 0.4783 ns | 24.03 |      48 B |          NA |
*/
