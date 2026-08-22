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

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.111
  [Host]     : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method                         | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|------------------------------- |----------:|----------:|------:|----------:|------------:|
| CreateObject                   | 6.2039 ns | 0.0350 ns |  1.00 |      24 B |        1.00 |
| CreateStruct                   | 1.1096 ns | 0.0050 ns |  0.18 |         - |        0.00 |
| CreateScalarQuantity           | 0.7869 ns | 0.0061 ns |  0.13 |         - |        0.00 |
| CreateScalarQuantityWithoutOpt | 3.4577 ns | 0.0980 ns |  0.56 |         - |        0.00 |
| CreateQuotientQuantity         | 6.3994 ns | 0.0084 ns |  1.03 |         - |        0.00 |
| CreateProductQuantity          | 6.1587 ns | 0.0221 ns |  0.99 |         - |        0.00 |
| CreateCachedProductQuantity    | 0.5718 ns | 0.0054 ns |  0.09 |         - |        0.00 |
| CreateScalarPowerQuantity      | 8.9148 ns | 0.0543 ns |  1.44 |         - |        0.00 |
| CreateSquarePowerQuantity      | 5.3343 ns | 0.0056 ns |  0.86 |         - |        0.00 |
| CreateInvertibleQuantity       | 7.9004 ns | 0.0062 ns |  1.27 |         - |        0.00 |
| CreateCachedInvertibleQuantity | 8.6515 ns | 0.0216 ns |  1.39 |         - |        0.00 |
*/
