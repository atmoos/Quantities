using System;
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

public sealed class DummyObject : ICastOperators<DummyObject>
{
    private readonly DummyQuant value;
    private DummyObject(in DummyQuant value) => this.value = value;
    public static DummyObject Of(in Double value) => new(new DummyQuant(in value));
    public static implicit operator Double(DummyObject obj) => obj.value.Value;
}
public readonly struct DummyStruct : ICastOperators<DummyStruct>
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
    public DummyObject CreateObject() => DummyObject.Of(in this.value);

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public DummyStruct CreateStruct() => DummyStruct.Of(in this.value);

    [Benchmark]
    public Length CreateScalarQuantity() => Length.Of(in this.value).Si<Kilo, Metre>();

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public Length CreateScalarQuantityWithoutOpt() => Length.Of(in this.value).Si<Centi, Metre>();

    [Benchmark]
    public Velocity CreateQuotientQuantity() => Velocity.Of(in this.value).Si<Kilo, Metre>().Per.Metric<Hour>();

    [Benchmark]
    public Energy CreateProductQuantity() => Energy.Of(in this.value).Metric<Kilo, Watt, Hour>();

    [Benchmark]
    public Area CreateScalarPowerQuantity() => Area.Of(in this.value).Metric<Are>();

    [Benchmark]
    public Area CreateSquarePowerQuantity() => Area.Of(in this.value).Square.Si<Metre>();
}

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.107
  [Host]     : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2


|                         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
|                   CreateObject |  6.599 ns | 0.0559 ns | 0.0522 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
|                   CreateStruct |  1.409 ns | 0.0576 ns | 0.0616 ns |  0.21 |    0.01 |      - |         - |        0.00 |
|           CreateScalarQuantity |  3.702 ns | 0.0451 ns | 0.0400 ns |  0.56 |    0.01 |      - |         - |        0.00 |
| CreateScalarQuantityWithoutOpt |  8.379 ns | 0.1108 ns | 0.1037 ns |  1.27 |    0.02 |      - |         - |        0.00 |
|         CreateQuotientQuantity | 20.613 ns | 0.0706 ns | 0.0626 ns |  3.12 |    0.03 | 0.0057 |      24 B |        1.00 |
|          CreateProductQuantity |  6.630 ns | 0.0855 ns | 0.0800 ns |  1.00 |    0.01 |      - |         - |        0.00 |
|      CreateScalarPowerQuantity | 22.445 ns | 0.0592 ns | 0.0553 ns |  3.40 |    0.03 |      - |         - |        0.00 |
|      CreateSquarePowerQuantity | 11.862 ns | 0.0583 ns | 0.0545 ns |  1.80 |    0.02 |      - |         - |        0.00 |
*/
