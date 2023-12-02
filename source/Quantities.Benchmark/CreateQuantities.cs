using System.Runtime.CompilerServices;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;

internal readonly struct DummyQuantity
{
    private readonly Double value;
    public Double Value => this.value;
    public DummyQuantity(in Double value) => this.value = value;
}

public sealed class DummyObject : ICastOperators<DummyObject, Double>
{
    private readonly DummyQuantity value;
    private DummyObject(in DummyQuantity value) => this.value = value;
    public static DummyObject Of(in Double value) => new(new DummyQuantity(in value));
    public static implicit operator Double(DummyObject obj) => obj.value.Value;
}

public readonly struct DummyStruct : ICastOperators<DummyStruct, Double>
{
    private readonly DummyQuantity value;
    private DummyStruct(in DummyQuantity value) => this.value = value;
    public static DummyStruct Of(in Double value) => new(new DummyQuantity(in value));
    public static implicit operator Double(DummyStruct obj) => obj.value.Value;
}

[MemoryDiagnoser]
//[EventPipeProfiler(EventPipeProfile.CpuSampling)]
public class CreateQuantities
{
    private static readonly Creation.Product<Watt, Hour> kwh = Si<Kilo, Watt>().Times(Metric<Hour>());
    private static readonly Random random = new();
    private readonly Double value = random.NextDouble();

    [Benchmark(Baseline = true)]
    public DummyObject CreateObject() => DummyObject.Of(in this.value);

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public DummyStruct CreateStruct() => DummyStruct.Of(in this.value);

    [Benchmark]
    public Length CreateScalarQuantity() => Length.Of(in this.value, Si<Kilo, Metre>());

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public Length CreateScalarQuantityWithoutOpt() => Length.Of(in this.value, Si<Centi, Metre>());

    [Benchmark]
    public Velocity CreateQuotientQuantity() => Velocity.Of(in this.value, Si<Kilo, Metre>().Per(Metric<Hour>()));

    [Benchmark]
    public Energy CreateProductQuantity() => Energy.Of(in this.value, Si<Kilo, Watt>().Times(Metric<Hour>()));

    [Benchmark]
    public Energy CreateCachedProductQuantity() => Energy.Of(in this.value, in kwh);

    [Benchmark]
    public Area CreateScalarPowerQuantity() => Area.Of(in this.value, Metric<Are>());

    [Benchmark]
    public Area CreateSquarePowerQuantity() => Area.Of(in this.value, Square(Si<Metre>()));
}

/* Summary *

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


 Method                         | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
------------------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
 CreateObject                   |  6.667 ns | 0.1870 ns | 0.2000 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
 CreateStruct                   |  1.221 ns | 0.0065 ns | 0.0054 ns |  0.19 |    0.01 |      - |         - |        0.00 |
 CreateScalarQuantity           |  1.061 ns | 0.0082 ns | 0.0068 ns |  0.16 |    0.00 |      - |         - |        0.00 |
 CreateScalarQuantityWithoutOpt |  3.490 ns | 0.0106 ns | 0.0089 ns |  0.53 |    0.02 |      - |         - |        0.00 |
 CreateQuotientQuantity         | 16.478 ns | 0.1448 ns | 0.1284 ns |  2.49 |    0.08 |      - |         - |        0.00 |
 CreateProductQuantity          | 14.364 ns | 0.1109 ns | 0.0983 ns |  2.17 |    0.06 |      - |         - |        0.00 |
 CreateCachedProductQuantity    |  1.164 ns | 0.0118 ns | 0.0110 ns |  0.18 |    0.01 |      - |         - |        0.00 |
 CreateScalarPowerQuantity      | 10.756 ns | 0.0373 ns | 0.0331 ns |  1.63 |    0.05 |      - |         - |        0.00 |
 CreateSquarePowerQuantity      |  1.066 ns | 0.0121 ns | 0.0107 ns |  0.16 |    0.01 |      - |         - |        0.00 |
*/
