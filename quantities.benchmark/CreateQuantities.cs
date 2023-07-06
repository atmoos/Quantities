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
    public Double CreateProductQuantity() => Energy.Of(in this.value).Metric<Kilo, Watt, Hour>();

    [Benchmark]
    public Double CreateScalarPowerQuantity() => Area.Of(in this.value).Metric<Are>();

    [Benchmark]
    public Double CreateSquarePowerQuantity() => Area.Of(in this.value).Square.Si<Metre>();
}

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.107
  [Host]     : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2


|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
|                   CreateObject |  4.8580 ns | 0.0676 ns | 0.0565 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
|                   CreateStruct |  2.8438 ns | 0.0282 ns | 0.0250 ns |  0.58 |    0.01 |      - |         - |        0.00 |
|           CreateScalarQuantity |  0.1595 ns | 0.0210 ns | 0.0197 ns |  0.03 |    0.00 |      - |         - |        0.00 |
| CreateScalarQuantityWithoutOpt |  9.9848 ns | 0.0438 ns | 0.0410 ns |  2.06 |    0.03 |      - |         - |        0.00 |
|         CreateQuotientQuantity | 17.8134 ns | 0.0855 ns | 0.0714 ns |  3.67 |    0.05 | 0.0057 |      24 B |        1.00 |
|          CreateProductQuantity |  2.1948 ns | 0.0296 ns | 0.0276 ns |  0.45 |    0.01 |      - |         - |        0.00 |
|      CreateScalarPowerQuantity | 18.3432 ns | 0.0785 ns | 0.0734 ns |  3.78 |    0.05 |      - |         - |        0.00 |
|      CreateSquarePowerQuantity |  8.5504 ns | 0.0295 ns | 0.0247 ns |  1.76 |    0.02 |      - |         - |        0.00 |
*/
