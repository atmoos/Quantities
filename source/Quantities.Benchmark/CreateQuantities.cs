using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

using static Quantities.Systems;

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
    private static readonly Random random = new();
    private readonly Double value = random.NextDouble();

    [Benchmark(Baseline = true)]
    public Double CreateObject() => DummyObject.Of(in this.value);

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public Double CreateStruct() => DummyStruct.Of(in this.value);

    [Benchmark]
    public Double CreateScalarQuantity() => Length.Of(in this.value, Si<Kilo, Metre>());

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public Double CreateScalarQuantityWithoutOpt() => Length.Of(in this.value, Si<Centi, Metre>());

    [Benchmark]
    public Double CreateQuotientQuantity() => Velocity.Of(in this.value, Si<Kilo, Metre>().Per(Metric<Hour>()));

    [Benchmark]
    public Double CreateProductQuantity() => Energy.Of(in this.value, Si<Kilo, Watt>().Dot(Metric<Hour>()));

    [Benchmark]
    public Double CreateScalarPowerQuantity() => Area.Of(in this.value).Metric<Are>();

    [Benchmark]
    public Double CreateSquarePowerQuantity() => Area.Of(in this.value, Square(Si<Metre>()));
}

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method                         | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| CreateObject                   |  5.4106 ns | 0.0767 ns | 0.0718 ns | 1.000 |    0.00 | 0.0057 |      24 B |        1.00 |
| CreateStruct                   |  2.8346 ns | 0.0764 ns | 0.0715 ns | 0.524 |    0.01 |      - |         - |        0.00 |
| CreateScalarQuantity           |  0.0350 ns | 0.0110 ns | 0.0097 ns | 0.006 |    0.00 |      - |         - |        0.00 |
| CreateScalarQuantityWithoutOpt |  5.7208 ns | 0.0389 ns | 0.0364 ns | 1.058 |    0.02 |      - |         - |        0.00 |
| CreateQuotientQuantity         | 24.5403 ns | 0.1599 ns | 0.1335 ns | 4.537 |    0.07 |      - |         - |        0.00 |
| CreateProductQuantity          | 26.7062 ns | 0.3102 ns | 0.2901 ns | 4.937 |    0.10 |      - |         - |        0.00 |
| CreateScalarPowerQuantity      | 22.1133 ns | 0.4733 ns | 0.4427 ns | 4.089 |    0.13 |      - |         - |        0.00 |
| CreateSquarePowerQuantity      | 12.5456 ns | 0.1643 ns | 0.1536 ns | 2.319 |    0.04 |      - |         - |        0.00 |
*/
