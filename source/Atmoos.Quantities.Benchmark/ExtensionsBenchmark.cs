using Atmoos.Quantities.Physics;
using Atmoos.Quantities.Units.Si;
using BenchmarkDotNet.Diagnosers;

using Byte = Atmoos.Quantities.Units.Si.Metric.UnitsOfInformation.Byte;

namespace Atmoos.Quantities.Benchmark;

[ShortRunJob]
[MemoryDiagnoser(displayGenColumns: false)]
public class ExtensionsBenchmark
{
    private Time time = Time.Of(2, Si<Second>());
    private DataRate dataRate = DataRate.Of(3, Metric<Byte>().Per(Si<Second>()));

    [Benchmark(Baseline = true)]
    public Data Optimised() => this.time * this.dataRate;

    [Benchmark]
    public Data Extension() => this.dataRate * this.time;
}

/* Summary

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 2.91GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.100
  [Host]   : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  ShortRun : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

| Method    | Mean     | Error      | Ratio | Allocated | Alloc Ratio |
|---------- |---------:|-----------:|------:|----------:|------------:|
| Optimised | 6.352 ns |  0.9135 ns |  1.00 |         - |          NA |
| Extension | 6.908 ns | 11.8711 ns |  1.09 |         - |          NA |
*/
