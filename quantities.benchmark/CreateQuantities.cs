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


|                         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
|                   CreateObject |  4.934 ns | 0.1256 ns | 0.1633 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
|                   CreateStruct |  2.834 ns | 0.0639 ns | 0.0533 ns |  0.59 |    0.03 |      - |         - |        0.00 |
|           CreateScalarQuantity |  7.223 ns | 0.1102 ns | 0.1031 ns |  1.49 |    0.06 |      - |         - |        0.00 |
| CreateScalarQuantityWithoutOpt |  9.999 ns | 0.0342 ns | 0.0320 ns |  2.06 |    0.07 |      - |         - |        0.00 |
|         CreateQuotientQuantity | 20.458 ns | 0.3751 ns | 0.3508 ns |  4.21 |    0.14 |      - |         - |        0.00 |
|          CreateProductQuantity |  3.315 ns | 0.0327 ns | 0.0306 ns |  0.68 |    0.02 |      - |         - |        0.00 |
|      CreateScalarPowerQuantity | 13.909 ns | 0.0880 ns | 0.0823 ns |  2.86 |    0.08 |      - |         - |        0.00 |
|      CreateSquarePowerQuantity | 19.608 ns | 0.3939 ns | 0.3684 ns |  4.04 |    0.19 |      - |         - |        0.00 |
*/
