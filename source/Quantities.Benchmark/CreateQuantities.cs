using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;
using Quantities.Dimensions;

namespace Quantities.Benchmark;

internal readonly struct DummyQuant
{
    private readonly Double value;
    public Double Value => this.value;
    public DummyQuant(in Double value) => this.value = value;
}

public sealed class DummyObject : ICastOperators<DummyObject, Double>
{
    private readonly DummyQuant value;
    private DummyObject(in DummyQuant value) => this.value = value;
    public static DummyObject Of(in Double value) => new(new DummyQuant(in value));
    public static implicit operator Double(DummyObject obj) => obj.value.Value;
}

public readonly struct DummyStruct : ICastOperators<DummyStruct, Double>
{
    private readonly DummyQuant value;
    private DummyStruct(in DummyQuant value) => this.value = value;
    public static DummyStruct Of(in Double value) => new(new DummyQuant(in value));
    public static implicit operator Double(DummyStruct obj) => obj.value.Value;
}

[MemoryDiagnoser]
//[EventPipeProfiler(EventPipeProfile.CpuSampling)]
public class CreateQuantities
{
    private static readonly Creation.Product<Watt, Hour> kwh = Si<Kilo, Watt>().Dot(Metric<Hour>());
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
    public Energy CreateProductQuantity() => Energy.Of(in this.value, Si<Kilo, Watt>().Dot(Metric<Hour>()));

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
.NET SDK 7.0.113
  [Host]     : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.13 (7.0.1323.52501), X64 RyuJIT AVX2


| Method                         | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------------------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| CreateObject                   |  6.340 ns | 0.0561 ns | 0.0497 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
| CreateStruct                   |  1.643 ns | 0.0202 ns | 0.0189 ns |  0.26 |    0.00 |      - |         - |        0.00 |
| CreateScalarQuantity           |  5.738 ns | 0.0110 ns | 0.0098 ns |  0.90 |    0.01 |      - |         - |        0.00 |
| CreateScalarQuantityWithoutOpt |  4.704 ns | 0.0108 ns | 0.0090 ns |  0.74 |    0.01 |      - |         - |        0.00 |
| CreateQuotientQuantity         | 15.821 ns | 0.0588 ns | 0.0491 ns |  2.50 |    0.02 |      - |         - |        0.00 |
| CreateProductQuantity          | 16.747 ns | 0.3553 ns | 0.3802 ns |  2.64 |    0.06 |      - |         - |        0.00 |
| CreateCachedProductQuantity    |  5.459 ns | 0.1217 ns | 0.1138 ns |  0.86 |    0.02 |      - |         - |        0.00 |
| CreateScalarPowerQuantity      | 14.589 ns | 0.0475 ns | 0.0397 ns |  2.30 |    0.02 |      - |         - |        0.00 |
| CreateSquarePowerQuantity      |  5.511 ns | 0.0224 ns | 0.0198 ns |  0.87 |    0.01 |      - |         - |        0.00 |
*/
