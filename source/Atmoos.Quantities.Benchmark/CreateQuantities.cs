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

BenchmarkDotNet v0.15.2, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.109
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


| Method                         | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|------------------------------- |----------:|----------:|------:|----------:|------------:|
| CreateObject                   | 21.673 ns | 0.2039 ns |  1.00 |      24 B |        1.00 |
| CreateStruct                   |  9.213 ns | 0.1370 ns |  0.43 |         - |        0.00 |
| CreateScalarQuantity           |  2.034 ns | 0.1174 ns |  0.09 |         - |        0.00 |
| CreateScalarQuantityWithoutOpt | 21.399 ns | 0.2338 ns |  0.99 |         - |        0.00 |
| CreateQuotientQuantity         | 74.740 ns | 0.5929 ns |  3.45 |         - |        0.00 |
| CreateProductQuantity          | 70.499 ns | 0.5938 ns |  3.25 |         - |        0.00 |
| CreateCachedProductQuantity    |  1.915 ns | 0.0701 ns |  0.09 |         - |        0.00 |
| CreateScalarPowerQuantity      | 62.525 ns | 0.5207 ns |  2.89 |         - |        0.00 |
| CreateSquarePowerQuantity      | 35.169 ns | 0.2405 ns |  1.62 |         - |        0.00 |
| CreateInvertibleQuantity       | 62.359 ns | 0.3838 ns |  2.88 |         - |        0.00 |
| CreateCachedInvertibleQuantity | 65.282 ns | 0.5242 ns |  3.01 |         - |        0.00 |
*/
