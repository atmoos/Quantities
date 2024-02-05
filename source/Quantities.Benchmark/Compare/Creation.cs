using BenchmarkDotNet.Configs;
using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser]
[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class Creation
{
    private const String Scalar = nameof(Scalar);
    private const String Cubed = nameof(Cubed);
    private const String Aliasing = nameof(Aliasing);
    private static readonly Double value = Math.E;

    [BenchmarkCategory(Scalar), Benchmark(Baseline = true)]
    public Length CreateScalarQuantity() => Length.Of(value, Si<Centi, Metre>());

    [BenchmarkCategory(Scalar), Benchmark]
    public UnitsNet.Length CreateScalarUnitsNet() => UnitsNet.Length.FromCentimeters(value);

    [BenchmarkCategory(Cubed), Benchmark(Baseline = true)]
    public Volume CreateCubedQuantity() => Volume.Of(value, Cubic(Si<Centi, Metre>()));

    [BenchmarkCategory(Cubed), Benchmark]
    public UnitsNet.Volume CreateCubedUnitsNet() => UnitsNet.Volume.FromCubicCentimeters(value);

    [BenchmarkCategory(Aliasing), Benchmark(Baseline = true)]
    public Volume CreateAliasedQuantity() => Volume.Of(value, Metric<Centi, Litre>());

    [BenchmarkCategory(Aliasing), Benchmark]
    public UnitsNet.Volume CreateAliasedUnitsNet() => UnitsNet.Volume.FromCentiliters(value);
}

/* Summary *

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-1260P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method                | Categories | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|---------------------- |----------- |----------:|----------:|------:|----------:|------------:|
| CreateAliasedQuantity | Aliasing   | 11.847 ns | 0.1295 ns |  1.00 |         - |          NA |
| CreateAliasedUnitsNet | Aliasing   |  3.543 ns | 0.0291 ns |  0.30 |         - |          NA |
|                       |            |           |           |       |           |             |
| CreateCubedQuantity   | Cubed      |  1.576 ns | 0.0293 ns |  1.00 |         - |          NA |
| CreateCubedUnitsNet   | Cubed      |  3.562 ns | 0.0355 ns |  2.26 |         - |          NA |
|                       |            |           |           |       |           |             |
| CreateScalarQuantity  | Scalar     |  1.349 ns | 0.0181 ns |  1.00 |         - |          NA |
| CreateScalarUnitsNet  | Scalar     |  2.943 ns | 0.0353 ns |  2.18 |         - |          NA |
*/
