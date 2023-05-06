using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Si;

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
}

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.12801), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.12801), X64 RyuJIT AVX2


|       Method |      Mean |     Error |    StdDev |    Median | Ratio |   Gen0 | Allocated | Alloc Ratio |
|------------- |----------:|----------:|----------:|----------:|------:|-------:|----------:|------------:|
| CreateObject | 4.5856 ns | 0.0727 ns | 0.0680 ns | 4.5781 ns | 1.000 | 0.0057 |      24 B |        1.00 |
| CreateStruct | 0.0086 ns | 0.0156 ns | 0.0167 ns | 0.0000 ns | 0.002 |      - |         - |        0.00 |
| CreateLength | 1.7952 ns | 0.0287 ns | 0.0224 ns | 1.7909 ns | 0.394 |      - |         - |        0.00 |
*/
