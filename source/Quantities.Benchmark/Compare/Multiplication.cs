using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;

using nLength = UnitsNet.Length;
using qtLength = QuantityTypes.Length;
using tLength = Towel.Measurements.Length<System.Double>;

using static Towel.Measurements.MeasurementsSyntax;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser(displayGenColumns: false)]
public class Multiplication
{
    private static readonly Length left = Length.Of(3, Si<Milli, Metre>());
    private static readonly Length right = Length.Of(4, Imperial<Foot>());
    private static readonly nLength nLeft = nLength.FromMillimeters(3);
    private static readonly nLength nRight = nLength.FromMillimeters(4);
    private static readonly qtLength qtLeft = 3 * qtLength.Millimetre;
    private static readonly qtLength qtRight = 4 * qtLength.Foot;
    private static readonly tLength tLeft = (3d, Millimeters);
    private static readonly tLength tRight = (4d, Feet);


    [Benchmark(Baseline = true)]
    public Area Quantity() => left * right;

    [Benchmark]
    public UnitsNet.Area UnitsNet() => nLeft * nRight;

    [Benchmark]
    public QuantityTypes.Area QuantityTypes() => qtLeft * qtRight;

    [Benchmark]
    public Towel.Measurements.Area<Double> TowelMeasurements() => tLeft * tRight;
}

/* Summary *

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.103
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


| Method            | Mean       | Error     | Ratio | Allocated | Alloc Ratio |
|------------------ |-----------:|----------:|------:|----------:|------------:|
| Quantity          |  6.8138 ns | 0.0566 ns | 1.000 |         - |          NA |
| UnitsNet          | 42.8554 ns | 0.3723 ns | 6.290 |         - |          NA |
| QuantityTypes     |  0.0006 ns | 0.0024 ns | 0.000 |         - |          NA |
| TowelMeasurements |  6.2112 ns | 0.0824 ns | 0.912 |         - |          NA |
*/
