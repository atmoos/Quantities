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
|                 CreateObject |  6.852 ns | 0.0687 ns | 0.0642 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
|                 CreateStruct |  1.192 ns | 0.0078 ns | 0.0069 ns |  0.17 |    0.00 |      - |         - |        0.00 |
|           CreateFastQuantity |  4.224 ns | 0.0187 ns | 0.0175 ns |  0.62 |    0.01 |      - |         - |        0.00 |
| CreateFastQuantityWithoutOpt |  8.176 ns | 0.0279 ns | 0.0233 ns |  1.20 |    0.01 |      - |         - |        0.00 |
|           CreateSlowQuantity |  6.587 ns | 0.0801 ns | 0.0710 ns |  0.96 |    0.01 |      - |         - |        0.00 |
|     CreateAllocatingQuantity | 17.145 ns | 0.0475 ns | 0.0397 ns |  2.51 |    0.03 | 0.0057 |      24 B |        1.00 |
*/
