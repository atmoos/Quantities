using BenchmarkDotNet.Configs;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;

using nLength = UnitsNet.Length;
using qtLength = QuantityTypes.Length;
using tLength = Towel.Measurements.Length<System.Double>;

using static Towel.Measurements.MeasurementsSyntax;

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
    private static readonly qtLength qtSi = 3 * qtLength.Millimetre;
    private static readonly qtLength qtImperial = 4 * qtLength.Foot;
    private static readonly tLength tSi = (3d, Millimeters);
    private static readonly tLength tImperial = (4d, Feet);

    [BenchmarkCategory(ToSi), Benchmark(Baseline = true)]
    public Double QuantityToSi() => imperial.To(Si<Milli, Metre>());
    [BenchmarkCategory(ToSi), Benchmark]
    public Double UnitsNetToSi() => nImperial.Millimeters;
    [BenchmarkCategory(ToSi), Benchmark]
    public Double QuantityTypesToSi() => qtImperial.ConvertTo(qtLength.Millimetre);
    [BenchmarkCategory(ToSi), Benchmark]
    public Double TowelMeasurementsToSi() => tImperial[Millimeters];

    [BenchmarkCategory(ToImperial), Benchmark(Baseline = true)]
    public Double QuantityToImperial() => si.To(Imperial<Foot>());
    [BenchmarkCategory(ToImperial), Benchmark]
    public Double UnitsNetToImperial() => nSi.Feet;
    [BenchmarkCategory(ToImperial), Benchmark]
    public Double QuantityTypesToImperial() => qtSi.ConvertTo(qtLength.Foot);
    [BenchmarkCategory(ToImperial), Benchmark]
    public Double TowelMeasurementsToImperial() => tSi[Feet];

    [BenchmarkCategory(ToSame), Benchmark(Baseline = true)]
    public Double QuantityToSame() => imperial.To(Imperial<Foot>());
    [BenchmarkCategory(ToSame), Benchmark]
    public Double UnitsNetToSame() => nImperial.Feet;
    [BenchmarkCategory(ToSame), Benchmark]
    public Double QuantityTypesToSame() => qtImperial.ConvertTo(qtLength.Foot);
    [BenchmarkCategory(ToSame), Benchmark]
    public Double TowelMeasurementsToSame() => tImperial[Feet];
}

/* Summary *

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.103
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


| Method                      | Mean       | Error     | Ratio  | Allocated | Alloc Ratio |
|---------------------------- |-----------:|----------:|-------:|----------:|------------:|
| QuantityToImperial          |  3.5543 ns | 0.0593 ns |   1.00 |         - |          NA |
| UnitsNetToImperial          | 80.8526 ns | 1.5024 ns |  22.78 |      48 B |          NA |
| QuantityTypesToImperial     |  0.0746 ns | 0.0038 ns |   0.02 |         - |          NA |
| TowelMeasurementsToImperial |  3.6869 ns | 0.0139 ns |   1.04 |         - |          NA |
|                             |            |           |        |           |             |
| QuantityToSame              |  1.2234 ns | 0.0062 ns |  1.000 |         - |          NA |
| UnitsNetToSame              |  0.4822 ns | 0.0077 ns |  0.394 |         - |          NA |
| QuantityTypesToSame         |  0.0184 ns | 0.0041 ns |  0.015 |         - |          NA |
| TowelMeasurementsToSame     |  0.0004 ns | 0.0009 ns |  0.000 |         - |          NA |
|                             |            |           |        |           |             |
| QuantityToSi                |  3.5698 ns | 0.0140 ns |  1.000 |         - |          NA |
| UnitsNetToSi                | 82.4369 ns | 0.4410 ns | 23.109 |      48 B |          NA |
| QuantityTypesToSi           |  0.0000 ns | 0.0000 ns |  0.000 |         - |          NA |
| TowelMeasurementsToSi       |  4.3121 ns | 0.0674 ns |  1.216 |         - |          NA |
*/
