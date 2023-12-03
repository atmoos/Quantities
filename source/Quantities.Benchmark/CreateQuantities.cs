using System.Runtime.CompilerServices;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;

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
}

/* Summary *

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method                         | Mean       | Error     | Ratio | Allocated | Alloc Ratio |
|------------------------------- |-----------:|----------:|------:|----------:|------------:|
| CreateObject                   |  6.5121 ns | 0.0446 ns |  1.00 |      24 B |        1.00 |
| CreateStruct                   |  1.3764 ns | 0.0507 ns |  0.21 |         - |        0.00 |
| CreateScalarQuantity           |  0.8206 ns | 0.0354 ns |  0.13 |         - |        0.00 |
| CreateScalarQuantityWithoutOpt |  3.4004 ns | 0.0235 ns |  0.52 |         - |        0.00 |
| CreateQuotientQuantity         | 13.1254 ns | 0.0655 ns |  2.02 |         - |        0.00 |
| CreateProductQuantity          | 15.1995 ns | 0.1573 ns |  2.33 |         - |        0.00 |
| CreateCachedProductQuantity    |  1.0541 ns | 0.0074 ns |  0.16 |         - |        0.00 |
| CreateScalarPowerQuantity      | 11.3300 ns | 0.2369 ns |  1.74 |         - |        0.00 |
| CreateSquarePowerQuantity      |  0.7973 ns | 0.0156 ns |  0.12 |         - |        0.00 |
*/
