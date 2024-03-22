using BenchmarkDotNet.Configs;
using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Metric;

using static Towel.Measurements.MeasurementsSyntax;

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

    [BenchmarkCategory(Scalar), Benchmark]
    public QuantityTypes.Length CreateScalarQuantityTypes() => value * QuantityTypes.Length.Centimetre;

    [BenchmarkCategory(Scalar), Benchmark]
    public Towel.Measurements.Length<Double> CreateScalarTowelMeasurements() => (value, Centimeters);

    [BenchmarkCategory(Cubed), Benchmark(Baseline = true)]
    public Volume CreateCubedQuantity() => Volume.Of(value, Cubic(Si<Centi, Metre>()));

    [BenchmarkCategory(Cubed), Benchmark]
    public UnitsNet.Volume CreateCubedUnitsNet() => UnitsNet.Volume.FromCubicCentimeters(value);

    [BenchmarkCategory(Cubed), Benchmark]
    public QuantityTypes.Volume CreateCubedQuantityTypes() => value * QuantityTypes.Volume.CubicCentimetre;

    [BenchmarkCategory(Cubed), Benchmark]
    public Towel.Measurements.Volume<Double> CreateCubedTowelMeasurement() => (value, Centimeters * Centimeters * Centimeters);

    [BenchmarkCategory(Aliasing), Benchmark(Baseline = true)]
    public Volume CreateAliasedQuantity() => Volume.Of(value, Metric<Centi, Litre>());

    [BenchmarkCategory(Aliasing), Benchmark]
    public UnitsNet.Volume CreateAliasedUnitsNet() => UnitsNet.Volume.FromCentiliters(value);

    [BenchmarkCategory(Aliasing), Benchmark]
    public QuantityTypes.Volume CreateAliasedQuantityTypes() => value * QuantityTypes.Volume.Litre / 100; // cannot create centilitres...

    [BenchmarkCategory(Aliasing), Benchmark]
    public Towel.Measurements.Volume<Double> CreateAliasedTowelMeasurements() => (1000 * value, Millimeters * Millimeters * Millimeters); // cannot create litres...
}

/* Summary *

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.103
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


| Method                         | Categories | Mean       | Error     | Ratio  | Allocated | Alloc Ratio |
|------------------------------- |----------- |-----------:|----------:|-------:|----------:|------------:|
| CreateAliasedQuantity          | Aliasing   | 13.0551 ns | 0.1992 ns |  1.000 |         - |          NA |
| CreateAliasedUnitsNet          | Aliasing   | 11.8207 ns | 0.0689 ns |  0.906 |         - |          NA |
| CreateAliasedQuantityTypes     | Aliasing   |  0.0018 ns | 0.0059 ns |  0.000 |         - |          NA |
| CreateAliasedTowelMeasurements | Aliasing   |  0.0728 ns | 0.0298 ns |  0.006 |         - |          NA |
|                                |            |            |           |        |           |             |
| CreateCubedQuantity            | Cubed      |  0.7939 ns | 0.0129 ns |  1.000 |         - |          NA |
| CreateCubedUnitsNet            | Cubed      | 11.8563 ns | 0.0491 ns | 14.906 |         - |          NA |
| CreateCubedQuantityTypes       | Cubed      |  0.0000 ns | 0.0000 ns |  0.000 |         - |          NA |
| CreateCubedTowelMeasurement    | Cubed      |  0.0975 ns | 0.0216 ns |  0.123 |         - |          NA |
|                                |            |            |           |        |           |             |
| CreateScalarQuantity           | Scalar     |  0.7578 ns | 0.0110 ns |  1.000 |         - |          NA |
| CreateScalarUnitsNet           | Scalar     | 11.7537 ns | 0.0513 ns | 15.513 |         - |          NA |
| CreateScalarQuantityTypes      | Scalar     |  0.0038 ns | 0.0061 ns |  0.005 |         - |          NA |
| CreateScalarTowelMeasurements  | Scalar     |  0.0634 ns | 0.0092 ns |  0.085 |         - |          NA |
*/
