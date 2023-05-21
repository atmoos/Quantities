using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Si;
using Quantities.Units.Si.Metric;
using Bytes = Quantities.Units.Si.Metric.Byte;

namespace Quantities.Benchmark;

public sealed class DummyObject : ICastOperators<DummyObject>
{
    private readonly Double value;
    public DummyObject(in Double value) => this.value = value;
    public static implicit operator Double(DummyObject obj) => obj.value;
}
public readonly struct DummyStruct : ICastOperators<DummyStruct>
{
    private readonly Double value;
    public DummyStruct(in Double value) => this.value = value;
    public static implicit operator Double(DummyStruct obj) => obj.value;
}

[MemoryDiagnoser]
//[EventPipeProfiler(EventPipeProfile.CpuSampling)]
public class CreateQuantities
{
    private static readonly Random random = new();
    private readonly Double value = random.NextDouble();

    [Benchmark(Baseline = true)]
    public DummyObject CreateObject() => new(in this.value);

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public DummyStruct CreateStruct() => new(in this.value);

    [Benchmark]
    public Length CreateFastQuantity() => Length.Of(in this.value).Si<Kilo, Metre>();

    [Benchmark]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public Length CreateFastQuantityWithoutOpt() => Length.Of(in this.value).Si<Centi, Metre>();

    [Benchmark]
    public Data CreateSlowQuantity() => Data.Of(in this.value).Metric<Kilo, Bytes>();

    [Benchmark]
    public Double CreateAllocatingQuantity() => Velocity.Of(in this.value).Si<Kilo, Metre>().Per.Metric<Hour>();
}

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.12801), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.12801), X64 RyuJIT AVX2

|                       Method |      Mean |     Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|----------------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
|                 CreateObject |  6.661 ns | 0.0832 ns | 0.0778 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
|                 CreateStruct |  1.160 ns | 0.0442 ns | 0.0413 ns |  0.17 |    0.01 |      - |         - |        0.00 |
|           CreateFastQuantity |  4.259 ns | 0.0191 ns | 0.0169 ns |  0.64 |    0.01 |      - |         - |        0.00 |
| CreateFastQuantityWithoutOpt |  8.429 ns | 0.0283 ns | 0.0265 ns |  1.27 |    0.02 |      - |         - |        0.00 |
|           CreateSlowQuantity |  6.738 ns | 0.0590 ns | 0.0523 ns |  1.01 |    0.01 |      - |         - |        0.00 |
|     CreateAllocatingQuantity | 21.105 ns | 0.0615 ns | 0.0545 ns |  3.17 |    0.04 | 0.0057 |      24 B |        1.00 |
*/
