using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
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
| CreateObject                   |  4.916 ns | 0.0673 ns | 0.0630 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
| CreateStruct                   |  2.742 ns | 0.0099 ns | 0.0077 ns |  0.56 |    0.01 |      - |         - |        0.00 |
| CreateScalarQuantity           |  5.969 ns | 0.0906 ns | 0.0847 ns |  1.21 |    0.02 |      - |         - |        0.00 |
| CreateScalarQuantityWithoutOpt | 10.088 ns | 0.1051 ns | 0.0983 ns |  2.05 |    0.04 |      - |         - |        0.00 |
| CreateQuotientQuantity         | 19.650 ns | 0.0675 ns | 0.0631 ns |  4.00 |    0.06 |      - |         - |        0.00 |
| CreateProductQuantity          | 19.563 ns | 0.0500 ns | 0.0443 ns |  3.98 |    0.05 |      - |         - |        0.00 |
| CreateScalarPowerQuantity      | 22.896 ns | 0.1581 ns | 0.1479 ns |  4.66 |    0.07 |      - |         - |        0.00 |
| CreateSquarePowerQuantity      | 18.429 ns | 0.1149 ns | 0.1075 ns |  3.75 |    0.06 |      - |         - |        0.00 |
*/
