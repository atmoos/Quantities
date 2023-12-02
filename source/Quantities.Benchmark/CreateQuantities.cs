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

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method                         | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| CreateObject                   |  6.7226 ns | 0.0492 ns | 0.0411 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
| CreateStruct                   |  1.1931 ns | 0.0081 ns | 0.0063 ns |  0.18 |    0.00 |      - |         - |        0.00 |
| CreateScalarQuantity           |  1.1564 ns | 0.0446 ns | 0.0417 ns |  0.17 |    0.01 |      - |         - |        0.00 |
| CreateScalarQuantityWithoutOpt |  3.4051 ns | 0.0248 ns | 0.0232 ns |  0.51 |    0.00 |      - |         - |        0.00 |
| CreateQuotientQuantity         | 14.9541 ns | 0.0320 ns | 0.0284 ns |  2.22 |    0.01 |      - |         - |        0.00 |
| CreateProductQuantity          | 14.6333 ns | 0.2141 ns | 0.2003 ns |  2.17 |    0.03 |      - |         - |        0.00 |
| CreateCachedProductQuantity    |  1.1411 ns | 0.0105 ns | 0.0087 ns |  0.17 |    0.00 |      - |         - |        0.00 |
| CreateScalarPowerQuantity      | 10.8057 ns | 0.1945 ns | 0.1819 ns |  1.61 |    0.04 |      - |         - |        0.00 |
| CreateSquarePowerQuantity      |  0.7609 ns | 0.0294 ns | 0.0275 ns |  0.11 |    0.00 |      - |         - |        0.00 |
*/
