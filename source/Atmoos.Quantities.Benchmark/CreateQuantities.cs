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

BenchmarkDotNet v0.15.2, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.109
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


| Method                         | Mean      | Error     | Ratio | Allocated | Alloc Ratio |
|------------------------------- |----------:|----------:|------:|----------:|------------:|
| CreateObject                   | 21.443 ns | 0.1887 ns |  1.00 |      24 B |        1.00 |
| CreateStruct                   |  9.131 ns | 0.0403 ns |  0.43 |         - |        0.00 |
| CreateScalarQuantity           |  1.937 ns | 0.0544 ns |  0.09 |         - |        0.00 |
| CreateScalarQuantityWithoutOpt | 26.373 ns | 0.1340 ns |  1.23 |         - |        0.00 |
| CreateQuotientQuantity         | 76.296 ns | 0.2102 ns |  3.56 |         - |        0.00 |
| CreateProductQuantity          | 70.632 ns | 0.2229 ns |  3.29 |         - |        0.00 |
| CreateCachedProductQuantity    |  2.148 ns | 0.0559 ns |  0.10 |         - |        0.00 |
| CreateScalarPowerQuantity      | 70.282 ns | 0.4174 ns |  3.28 |         - |        0.00 |
| CreateSquarePowerQuantity      |  1.951 ns | 0.0747 ns |  0.09 |         - |        0.00 |
| CreateInvertibleQuantity       | 69.879 ns | 0.4880 ns |  3.26 |         - |        0.00 |
| CreateCachedInvertibleQuantity | 63.239 ns | 0.2772 ns |  2.95 |         - |        0.00 |
*/
