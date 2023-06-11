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
|                 CreateObject |  7.199 ns | 0.0771 ns | 0.0721 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
|                 CreateStruct |  1.266 ns | 0.0559 ns | 0.0574 ns |  0.18 |    0.01 |      - |         - |        0.00 |
|           CreateFastQuantity |  4.245 ns | 0.0240 ns | 0.0224 ns |  0.59 |    0.01 |      - |         - |        0.00 |
| CreateFastQuantityWithoutOpt |  8.865 ns | 0.1366 ns | 0.1277 ns |  1.23 |    0.03 |      - |         - |        0.00 |
|           CreateSlowQuantity |  6.507 ns | 0.0594 ns | 0.0527 ns |  0.90 |    0.01 |      - |         - |        0.00 |
|     CreateAllocatingQuantity | 18.276 ns | 0.0393 ns | 0.0307 ns |  2.55 |    0.02 | 0.0057 |      24 B |        1.00 |
*/
