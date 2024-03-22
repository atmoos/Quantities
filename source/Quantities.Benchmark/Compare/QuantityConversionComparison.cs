using BenchmarkDotNet.Configs;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using UnitsNet.Units;

using nLength = UnitsNet.Length;
using qtLength = QuantityTypes.Length;
using tLength = Towel.Measurements.Length<System.Double>;

using static Towel.Measurements.MeasurementsSyntax;

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
    private static readonly qtLength qtSi = 3 * qtLength.Millimetre;
    private static readonly qtLength qtImperial = 4 * qtLength.Foot;
    private static readonly tLength tSi = (3d, Millimeters);
    private static readonly tLength tImperial = (4d, Feet);

    [BenchmarkCategory(ToSi), Benchmark(Baseline = true)]
    public Length QuantityToSi() => imperial.To(Si<Milli, Metre>());
    [BenchmarkCategory(ToSi), Benchmark]
    public nLength UnitsNetToSi() => nImperial.ToUnit(LengthUnit.Millimeter);
    [BenchmarkCategory(ToSi), Benchmark]
    public qtLength QuantityTypesToSi() => qtImperial.ConvertTo(qtLength.Millimetre) * qtLength.Millimetre;
    [BenchmarkCategory(ToSi), Benchmark]
    public tLength TowelMeasurementsToSi() => (tImperial[Millimeters], Millimeters);

    [BenchmarkCategory(ToImperial), Benchmark(Baseline = true)]
    public Length QuantityToImperial() => si.To(Imperial<Foot>());
    [BenchmarkCategory(ToImperial), Benchmark]
    public nLength UnitsNetToImperial() => nSi.ToUnit(LengthUnit.Foot);
    [BenchmarkCategory(ToImperial), Benchmark]
    public qtLength QuantityTypesToImperial() => qtSi.ConvertTo(qtLength.Foot) * qtLength.Foot;
    [BenchmarkCategory(ToImperial), Benchmark]
    public tLength TowelMeasurementsToImperial() => (tSi[Feet], Feet);

    [BenchmarkCategory(ToSame), Benchmark(Baseline = true)]
    public Length QuantityToSame() => imperial.To(Imperial<Foot>());
    [BenchmarkCategory(ToSame), Benchmark]
    public nLength UnitsNetToSame() => nImperial.ToUnit(LengthUnit.Foot);
    [BenchmarkCategory(ToSame), Benchmark]
    public qtLength QuantityTypesToSame() => qtImperial.ConvertTo(qtLength.Foot) * qtLength.Foot;
    [BenchmarkCategory(ToSame), Benchmark]
    public tLength TowelMeasurementsToSame() => (tImperial[Feet], Feet);
}

/* Summary *

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.103
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


| Method                      | Mean       | Error     | Ratio  | Allocated | Alloc Ratio |
|---------------------------- |-----------:|----------:|-------:|----------:|------------:|
| QuantityToImperial          |  2.7528 ns | 0.0701 ns |  1.000 |         - |          NA |
| UnitsNetToImperial          | 76.2580 ns | 0.5021 ns | 27.712 |      48 B |          NA |
| QuantityTypesToImperial     |  0.0049 ns | 0.0084 ns |  0.002 |         - |          NA |
| TowelMeasurementsToImperial |  3.9093 ns | 0.0157 ns |  1.421 |         - |          NA |
|                             |            |           |        |           |             |
| QuantityToSame              |  2.5044 ns | 0.0480 ns |  1.000 |         - |          NA |
| UnitsNetToSame              | 16.1036 ns | 0.0959 ns |  6.432 |         - |          NA |
| QuantityTypesToSame         |  0.0028 ns | 0.0079 ns |  0.001 |         - |          NA |
| TowelMeasurementsToSame     |  0.0340 ns | 0.0060 ns |  0.013 |         - |          NA |
|                             |            |           |        |           |             |
| QuantityToSi                |  3.6184 ns | 0.0270 ns |  1.000 |         - |          NA |
| UnitsNetToSi                | 75.7462 ns | 0.2194 ns | 20.934 |      48 B |          NA |
| QuantityTypesToSi           |  0.0038 ns | 0.0056 ns |  0.001 |         - |          NA |
| TowelMeasurementsToSi       |  3.4991 ns | 0.0125 ns |  0.967 |         - |          NA |
*/
