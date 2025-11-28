using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Si;

namespace Atmoos.Quantities.Benchmark;

[ShortRunJob]
[MemoryDiagnoser(displayGenColumns: true)]
public class QuantitiesBenchmark
{
    private static Quantity length = Quantity.Of<Measures.Si<Kilo, Metre>>(Math.Tau);

    [Benchmark]
    public void ConstructionIsAllocationFree()
    {
        var length = Quantity.Of<Measures.Si<Milli, Metre>>(Math.Tau);
        GC.KeepAlive(length);
    }

    [Benchmark]
    public (Double, String) DeconstructIsAllocationFree()
    {
        (Double value, String unit) = length;
        return (value, unit);
    }
}

/* Summary

BenchmarkDotNet v0.15.4, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 2.92GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]   : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  ShortRun : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

| Method                       | Mean      | Error     | Allocated |
|----------------------------- |----------:|----------:|----------:|
| ConstructionIsAllocationFree | 0.9076 ns | 0.6502 ns |         - |
| DeconstructIsAllocationFree  | 0.8271 ns | 0.4411 ns |         - |
*/
