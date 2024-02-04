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

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method                | Categories | Mean       | Error     | Ratio | Allocated | Alloc Ratio |
|---------------------- |----------- |-----------:|----------:|------:|----------:|------------:|
| CreateAliasedQuantity | Aliasing   | 11.6151 ns | 0.1729 ns |  1.00 |         - |          NA |
| CreateAliasedUnitsNet | Aliasing   | 11.3702 ns | 0.0411 ns |  0.98 |         - |          NA |
|                       |            |            |           |       |           |             |
| CreateCubedQuantity   | Cubed      |  0.7358 ns | 0.0085 ns |  1.00 |         - |          NA |
| CreateCubedUnitsNet   | Cubed      | 11.7242 ns | 0.0352 ns | 15.94 |         - |          NA |
|                       |            |            |           |       |           |             |
| CreateScalarQuantity  | Scalar     |  0.7579 ns | 0.0066 ns |  1.00 |         - |          NA |
| CreateScalarUnitsNet  | Scalar     | 11.7629 ns | 0.0114 ns | 15.53 |         - |          NA |
*/
