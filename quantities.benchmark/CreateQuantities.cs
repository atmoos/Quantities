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
    public DummyStruct(Double value) => this.value = value;
    public static implicit operator Double(DummyStruct obj) => obj.value;
}

[MemoryDiagnoser]
public class CreateQuantities
{
    private static readonly Random random = new();
    private Double value = random.NextDouble();


    [Benchmark(Baseline = true)]
    public DummyObject CreateObject() => new DummyObject(this.value);

    [Benchmark]
    public DummyObject CreateObjectIn() => new DummyObject(in this.value);

    [Benchmark]
    public DummyStruct CreateStruct() => new DummyStruct(this.value);

    [Benchmark]
    public Length CreateLength() => Length.Of(in this.value).Si<Kilo, Metre>();

    [Benchmark]
    public Velocity CreateVelocity() => Velocity.Si<Kilo, Metre>(in this.value).Per<Hour>();
}

/*
// * Summary *

BenchmarkDotNet=v0.13.2, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.12801), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.12801), X64 RyuJIT AVX2


|         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
|   CreateObject |  6.1821 ns | 0.1096 ns | 0.1025 ns |  1.00 |    0.00 | 0.0057 |      24 B |        1.00 |
| CreateObjectIn |  6.7297 ns | 0.0760 ns | 0.0711 ns |  1.09 |    0.03 | 0.0057 |      24 B |        1.00 |
|   CreateStruct |  0.3338 ns | 0.0049 ns | 0.0043 ns |  0.05 |    0.00 |      - |         - |        0.00 |
|   CreateLength |  4.3623 ns | 0.0216 ns | 0.0192 ns |  0.71 |    0.01 |      - |         - |        0.00 |
| CreateVelocity | 10.5074 ns | 0.0638 ns | 0.0597 ns |  1.70 |    0.03 | 0.0057 |      24 B |        1.00 |
*/
