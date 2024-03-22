using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si.Metric;

using nLength = UnitsNet.Length;
using nVolume = UnitsNet.Volume;
using qtArea = QuantityTypes.Area;
using qtVolume = QuantityTypes.Volume;
using tLength = Towel.Measurements.Length<System.Double>;
using tVolume = Towel.Measurements.Volume<System.Double>;

using static Towel.Measurements.MeasurementsSyntax;

namespace Quantities.Benchmark.Compare;

[MemoryDiagnoser]
public class Division
{
    private static readonly Volume left = Volume.Of(32, Metric<Centi, Litre>());
    private static readonly Length right = Length.Of(4, Imperial<Foot>());
    private static readonly nVolume nLeft = nVolume.FromCentiliters(32);
    private static readonly nLength nRight = nLength.FromFeet(4);
    private static readonly qtVolume qtLeft = 32 * qtVolume.Litre / 100;
    private static readonly qtArea qtRight = 4 * qtArea.Acre;
    private static readonly tVolume tLeft = (32 * 10, Centimeters * Centimeters * Centimeters);
    private static readonly tLength tRight = (4, Feet);

    [Benchmark(Baseline = true)]
    public Area Quantity() => left / right;

    [Benchmark]
    public UnitsNet.Area UnitsNet() => nLeft / nRight;

    [Benchmark]
    public QuantityTypes.Length QuantityTypes() => qtLeft / qtRight; // cannot divide volume by area :-/

    [Benchmark]
    public Towel.Measurements.Area<Double> TowelMeasurements() => tLeft / tRight;
}

/* Summary *

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.103
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


| Method            | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|------------------ |----------:|----------:|------:|----------:|------------:|
| Quantity          |  8.259 ns | 0.0330 ns |  1.00 |         - |          NA |
| UnitsNet          | 47.356 ns | 0.6768 ns |  5.75 |         - |          NA |
| QuantityTypes     |  1.734 ns | 0.0055 ns |  0.21 |         - |          NA |
| TowelMeasurements |  8.630 ns | 0.0247 ns |  1.04 |         - |          NA |
*/
