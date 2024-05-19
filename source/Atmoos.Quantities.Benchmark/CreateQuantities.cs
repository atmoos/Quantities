using System.Runtime.CompilerServices;
using BenchmarkDotNet.Diagnosers;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Si;
using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Metric;

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
    public Length CreateScalarQuantity() => Length.Of(in this.value, Si<Kilo, Metre>());

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public Length CreateScalarQuantityWithoutOpt() => Length.Of(in this.value, Si<Centi, Metre>());

    [Benchmark]
    public Velocity CreateQuotientQuantity() => Velocity.Of(in this.value, Si<Kilo, Metre>().Per(Metric<Hour>()));

    [Benchmark]
    public Energy CreateProductQuantity() => Energy.Of(in this.value, Si<Kilo, Watt>().Times(Metric<Hour>()));

    [Benchmark]
    public Energy CreateCachedProductQuantity() => Energy.Of(in this.value, in kwh);

    [Benchmark]
    public Area CreateScalarPowerQuantity() => Area.Of(in this.value, Metric<Are>());

    [Benchmark]
    public Area CreateSquarePowerQuantity() => Area.Of(in this.value, Square(Si<Metre>()));

    [Benchmark]
    public Frequency CreateInvertibleQuantity() => Frequency.Of(in this.value, Si<Hertz>());

    [Benchmark]
    public Frequency CreateCachedInvertibleQuantity() => Frequency.Of(in this.value, in kHz);
}

/* Summary

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.103
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


| Method                         | Mean       | Error     | Ratio | Allocated | Alloc Ratio |
|------------------------------- |-----------:|----------:|------:|----------:|------------:|
| CreateObject                   |  6.9026 ns | 0.1083 ns |  1.00 |      24 B |        1.00 |
| CreateStruct                   |  1.2959 ns | 0.0166 ns |  0.19 |         - |        0.00 |
| CreateScalarQuantity           |  0.8059 ns | 0.0054 ns |  0.12 |         - |        0.00 |
| CreateScalarQuantityWithoutOpt |  3.4030 ns | 0.0065 ns |  0.49 |         - |        0.00 |
| CreateQuotientQuantity         | 14.1086 ns | 0.1600 ns |  2.04 |         - |        0.00 |
| CreateProductQuantity          | 15.2728 ns | 0.0593 ns |  2.21 |         - |        0.00 |
| CreateCachedProductQuantity    |  0.7974 ns | 0.0078 ns |  0.12 |         - |        0.00 |
| CreateScalarPowerQuantity      | 14.8134 ns | 0.2844 ns |  2.15 |         - |        0.00 |
| CreateSquarePowerQuantity      |  0.7277 ns | 0.0066 ns |  0.11 |         - |        0.00 |
| CreateInvertibleQuantity       | 14.9650 ns | 0.1502 ns |  2.17 |         - |        0.00 |
| CreateCachedInvertibleQuantity | 11.3702 ns | 0.0257 ns |  1.64 |         - |        0.00 |
*/
