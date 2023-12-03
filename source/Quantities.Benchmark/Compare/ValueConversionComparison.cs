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

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method             | Mean       | Error     | Ratio | Allocated | Alloc Ratio |
|------------------- |-----------:|----------:|------:|----------:|------------:|
| QuantityToImperial |  3.5448 ns | 0.0686 ns |  1.00 |         - |          NA |
| UnitsNetToImperial | 85.0572 ns | 0.4295 ns | 23.96 |      48 B |          NA |
|                    |            |           |       |           |             |
| QuantityToSame     |  1.4211 ns | 0.0111 ns |  1.00 |         - |          NA |
| UnitsNetToSame     |  0.4489 ns | 0.0062 ns |  0.32 |         - |          NA |
|                    |            |           |       |           |             |
| QuantityToSi       |  3.5082 ns | 0.0215 ns |  1.00 |         - |          NA |
| UnitsNetToSi       | 84.0433 ns | 1.6720 ns | 23.78 |      48 B |          NA |
*/
