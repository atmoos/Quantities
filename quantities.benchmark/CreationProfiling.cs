using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Quantities.Prefixes;
using Quantities.Quantities;
using Quantities.Units.Si;
using Quantities.Units.Si.Metric;

namespace Quantities.Benchmark;

[ShortRunJob]
[WarmupCount(7)]
[IterationCount(3)]
[EventPipeProfiler(EventPipeProfile.CpuSampling)]
public class CreationProfiling
{
    private static readonly Random random = new();
    private readonly Double value = random.NextDouble();

    [Benchmark]
    public Velocity CreateQuotientQuantity() => Velocity.Of(in this.value).Si<Kilo, Metre>().Per.Metric<Hour>();
}

/*
// * Summary *

BenchmarkDotNet=v0.13.5, OS=arch 
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.107
  [Host]     : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2
  Job-PMWSBJ : .NET 7.0.7 (7.0.723.32201), X64 RyuJIT AVX2

IterationCount=3  LaunchCount=1  WarmupCount=7  

|             Method |     Mean |    Error |   StdDev |
|------------------- |---------:|---------:|---------:|
| CreateFastQuantity | 10.78 ns | 2.145 ns | 0.118 ns |
*/