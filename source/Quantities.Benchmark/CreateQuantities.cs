using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;
using Quantities.Dimensions;

using static Quantities.AliasOf<Quantities.Dimensions.ILength>;

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
    public Area CreateScalarPowerQuantity() => Area.Of(in this.value, AliasOf<ILength>.Metric<Are>());

    [Benchmark]
    public Area CreateSquarePowerQuantity() => Area.Of(in this.value, Square(Si<Metre>()));
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
| CreateObject                   |  5.937 ns | 0.1106 ns | 0.1035 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
| CreateStruct                   |  1.621 ns | 0.0464 ns | 0.0411 ns |  0.27 |    0.01 |      - |         - |        0.00 |
| CreateScalarQuantity           |  5.468 ns | 0.0441 ns | 0.0412 ns |  0.92 |    0.02 |      - |         - |        0.00 |
| CreateScalarQuantityWithoutOpt |  4.514 ns | 0.0139 ns | 0.0123 ns |  0.76 |    0.01 |      - |         - |        0.00 |
| CreateQuotientQuantity         | 15.357 ns | 0.0940 ns | 0.0879 ns |  2.59 |    0.05 |      - |         - |        0.00 |
| CreateProductQuantity          | 15.606 ns | 0.2638 ns | 0.2467 ns |  2.63 |    0.08 |      - |         - |        0.00 |
| CreateCachedProductQuantity    |  5.546 ns | 0.0397 ns | 0.0371 ns |  0.93 |    0.01 |      - |         - |        0.00 |
| CreateScalarPowerQuantity      | 15.624 ns | 0.1290 ns | 0.1007 ns |  2.64 |    0.05 |      - |         - |        0.00 |
| CreateSquarePowerQuantity      |  5.448 ns | 0.1203 ns | 0.1066 ns |  0.92 |    0.03 |      - |         - |        0.00 |
*/
