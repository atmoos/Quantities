using System.Runtime.CompilerServices;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Si;
using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Metric;
using BenchmarkDotNet.Diagnosers;

namespace Atmoos.Quantities.Benchmark;

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

[MemoryDiagnoser(displayGenColumns: false)]
//[EventPipeProfiler(EventPipeProfile.CpuSampling)]
public class CreateQuantities
{
    private static readonly Creation.Scalar<Hertz> kHz = Si<Kilo, Hertz>();
    private static readonly Creation.Product<Watt, Hour> kwh = Si<Kilo, Watt>().Times(Metric<Hour>());
    private static readonly Random random = new();
    private readonly Double value = random.NextDouble();

    [Benchmark(Baseline = true)]
    public DummyObject CreateObject() => DummyObject.Of(in this.value);

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public DummyStruct CreateStruct() => DummyStruct.Of(in this.value);

    [Benchmark]
    public Length CreateScalarQuantity() => Length.Of(in this.value, in Si<Kilo, Metre>());

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public Length CreateScalarQuantityWithoutOpt() => Length.Of(in this.value, in Si<Centi, Metre>());

    [Benchmark]
    public Velocity CreateQuotientQuantity() => Velocity.Of(in this.value, Si<Kilo, Metre>().Per(Metric<Hour>()));

    [Benchmark]
    public Energy CreateProductQuantity() => Energy.Of(in this.value, Si<Kilo, Watt>().Times(Metric<Hour>()));

    [Benchmark]
    public Energy CreateCachedProductQuantity() => Energy.Of(in this.value, in kwh);

    [Benchmark]
    public Area CreateScalarPowerQuantity() => Area.Of(in this.value, in Metric<Are>());

    [Benchmark]
    public Area CreateSquarePowerQuantity() => Area.Of(in this.value, Square(in Si<Metre>()));

    [Benchmark]
    public Frequency CreateInvertibleQuantity() => Frequency.Of(in this.value, in Si<Hertz>());

    [Benchmark]
    public Frequency CreateCachedInvertibleQuantity() => Frequency.Of(in this.value, in kHz);
}

/* Summary

BenchmarkDotNet v0.15.3, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method                         | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|------------------------------- |----------:|----------:|------:|----------:|------------:|
| CreateObject                   | 21.356 ns | 0.1811 ns |  1.00 |      24 B |        1.00 |
| CreateStruct                   |  9.055 ns | 0.1069 ns |  0.42 |         - |        0.00 |
| CreateScalarQuantity           |  1.980 ns | 0.0457 ns |  0.09 |         - |        0.00 |
| CreateScalarQuantityWithoutOpt | 21.357 ns | 0.1253 ns |  1.00 |         - |        0.00 |
| CreateQuotientQuantity         | 32.751 ns | 0.1183 ns |  1.53 |         - |        0.00 |
| CreateProductQuantity          | 32.288 ns | 0.1509 ns |  1.51 |         - |        0.00 |
| CreateCachedProductQuantity    |  1.967 ns | 0.0380 ns |  0.09 |         - |        0.00 |
| CreateScalarPowerQuantity      | 61.267 ns | 0.2160 ns |  2.87 |         - |        0.00 |
| CreateSquarePowerQuantity      | 39.499 ns | 0.1724 ns |  1.85 |         - |        0.00 |
| CreateInvertibleQuantity       | 64.749 ns | 0.2370 ns |  3.03 |         - |        0.00 |
| CreateCachedInvertibleQuantity | 62.725 ns | 0.2402 ns |  2.94 |         - |        0.00 |
*/
