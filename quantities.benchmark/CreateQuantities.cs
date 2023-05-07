using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Si;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;

public sealed class DummyObject
{
    private readonly Double value;
    public DummyObject(in Double value) => this.value = value;
    public static implicit operator Double(DummyObject obj) => obj.value;
}
public readonly struct DummyStruct
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
    private Double value = random.NextDouble();

    [Benchmark(Baseline = true)]
    public Double CreateObject() => new DummyObject(in this.value);

    [Benchmark]
    public Double CreateStruct() => new DummyStruct(in this.value);

    [Benchmark]
    public Double CreateLength() => Length.Of(in this.value).Si<Kilo, Metre>();

    [Benchmark]
    public Double CreateVelocity() => Velocity.Of(in this.value).Si<Kilo, Metre>().Per.Metric<Hour>();
}

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.12801), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.12801), X64 RyuJIT AVX2

|         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
|   CreateObject |  4.7630 ns | 0.0353 ns | 0.0313 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
|   CreateStruct |  0.0597 ns | 0.0053 ns | 0.0050 ns |  0.01 |    0.00 |      - |         - |        0.00 |
|   CreateLength |  1.7603 ns | 0.0074 ns | 0.0062 ns |  0.37 |    0.00 |      - |         - |        0.00 |
| CreateVelocity | 20.1067 ns | 0.2936 ns | 0.2602 ns |  4.22 |    0.05 | 0.0057 |      24 B |        1.00 |
*/
