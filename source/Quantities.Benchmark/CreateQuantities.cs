using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
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
| CreateObject                   |  6.458 ns | 0.0909 ns | 0.0850 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
| CreateStruct                   |  1.635 ns | 0.0607 ns | 0.0596 ns |  0.25 |    0.01 |      - |         - |        0.00 |
| CreateScalarQuantity           |  5.428 ns | 0.0489 ns | 0.0457 ns |  0.84 |    0.01 |      - |         - |        0.00 |
| CreateScalarQuantityWithoutOpt |  4.546 ns | 0.0143 ns | 0.0119 ns |  0.71 |    0.01 |      - |         - |        0.00 |
| CreateQuotientQuantity         | 15.384 ns | 0.0957 ns | 0.0849 ns |  2.39 |    0.03 |      - |         - |        0.00 |
| CreateProductQuantity          | 15.997 ns | 0.3390 ns | 0.3628 ns |  2.47 |    0.04 |      - |         - |        0.00 |
| CreateCachedProductQuantity    |  5.515 ns | 0.0123 ns | 0.0115 ns |  0.85 |    0.01 |      - |         - |        0.00 |
| CreateScalarPowerQuantity      | 13.346 ns | 0.0560 ns | 0.0467 ns |  2.07 |    0.02 |      - |         - |        0.00 |
| CreateSquarePowerQuantity      |  5.402 ns | 0.0391 ns | 0.0347 ns |  0.84 |    0.01 |      - |         - |        0.00 |
*/
