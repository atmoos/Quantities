using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

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
    public Double CreateScalarQuantity() => Length.Of(in this.value).Si<Kilo, Metre>();

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public Double CreateScalarQuantityWithoutOpt() => Length.Of(in this.value).Si<Centi, Metre>();

    [Benchmark]
    public Double CreateQuotientQuantity() => Velocity.Of(in this.value).Si<Kilo, Metre>().Per.Metric<Hour>();

    [Benchmark]
    public Double CreateProductQuantity() => Energy.Of(in this.value).Si<Kilo, Watt>().Times.Metric<Hour>();

    [Benchmark]
    public Double CreateScalarPowerQuantity() => Area.Of(in this.value).Metric<Are>();

    [Benchmark]
    public Double CreateSquarePowerQuantity() => Area.Of(in this.value).Square.Si<Metre>();
}

/*
// * Summary *

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method                         | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------------------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| CreateObject                   |  4.914 ns | 0.0893 ns | 0.0835 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
| CreateStruct                   |  2.845 ns | 0.0329 ns | 0.0308 ns |  0.58 |    0.01 |      - |         - |        0.00 |
| CreateScalarQuantity           |  5.878 ns | 0.0465 ns | 0.0388 ns |  1.20 |    0.01 |      - |         - |        0.00 |
| CreateScalarQuantityWithoutOpt |  9.984 ns | 0.1093 ns | 0.0969 ns |  2.04 |    0.03 |      - |         - |        0.00 |
| CreateQuotientQuantity         | 19.458 ns | 0.1512 ns | 0.1340 ns |  3.97 |    0.07 |      - |         - |        0.00 |
| CreateProductQuantity          | 19.704 ns | 0.1011 ns | 0.0946 ns |  4.01 |    0.07 |      - |         - |        0.00 |
| CreateScalarPowerQuantity      | 45.257 ns | 0.3016 ns | 0.2674 ns |  9.23 |    0.16 |      - |         - |        0.00 |
| CreateSquarePowerQuantity      | 18.550 ns | 0.2748 ns | 0.2436 ns |  3.78 |    0.10 |      - |         - |        0.00 |
*/
