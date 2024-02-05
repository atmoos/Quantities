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

[MemoryDiagnoser(displayGenColumns: false)]
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

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-1260P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method                         | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|------------------------------- |----------:|----------:|------:|----------:|------------:|
| CreateObject                   |  2.477 ns | 0.0535 ns |  1.00 |      24 B |        1.00 |
| CreateStruct                   |  1.162 ns | 0.0512 ns |  0.47 |         - |        0.00 |
| CreateScalarQuantity           |  1.637 ns | 0.0241 ns |  0.66 |         - |        0.00 |
| CreateScalarQuantityWithoutOpt |  3.255 ns | 0.0649 ns |  1.31 |         - |        0.00 |
| CreateQuotientQuantity         | 13.743 ns | 0.2433 ns |  5.55 |         - |        0.00 |
| CreateProductQuantity          | 14.735 ns | 0.1806 ns |  5.95 |         - |        0.00 |
| CreateCachedProductQuantity    |  1.623 ns | 0.0278 ns |  0.66 |         - |        0.00 |
| CreateScalarPowerQuantity      | 11.253 ns | 0.1016 ns |  4.54 |         - |        0.00 |
| CreateSquarePowerQuantity      |  1.425 ns | 0.0546 ns |  0.57 |         - |        0.00 |
*/
