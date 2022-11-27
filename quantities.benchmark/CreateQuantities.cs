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
    public Length CreateLength() => Length.Si<Kilo, Metre>(in this.value);

    [Benchmark]
    public Velocity CreateVelocity() => Velocity.Si<Kilo, Metre>(in this.value).Per<Hour>();
}

/*
// * Summary *

BenchmarkDotNet=v0.12.1, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=7.0.100
  [Host]     : .NET Core 7.0.0 (CoreCLR 7.0.22.56001, CoreFX 7.0.22.56001), X64 RyuJIT
  DefaultJob : .NET Core 7.0.0 (CoreCLR 7.0.22.56001, CoreFX 7.0.22.56001), X64 RyuJIT


|         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |-----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|   CreateObject |  6.6147 ns | 0.0908 ns | 0.0805 ns | 1.000 |    0.00 | 0.0057 |     - |     - |      24 B |
| CreateObjectIn |  6.0880 ns | 0.0495 ns | 0.0463 ns | 0.921 |    0.01 | 0.0057 |     - |     - |      24 B |
|   CreateStruct |  0.0547 ns | 0.0147 ns | 0.0138 ns | 0.008 |    0.00 |      - |     - |     - |         - |
|   CreateLength |  4.2311 ns | 0.0568 ns | 0.0504 ns | 0.640 |    0.01 |      - |     - |     - |         - |
| CreateVelocity | 10.0543 ns | 0.0173 ns | 0.0135 ns | 1.522 |    0.02 | 0.0057 |     - |     - |      24 B |
*/
