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

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.100
  [Host]   : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  ShortRun : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

| Method                       | Mean      | Error     | Allocated |
|----------------------------- |----------:|----------:|----------:|
| ConstructionIsAllocationFree | 0.3908 ns | 0.0302 ns |         - |
| DeconstructIsAllocationFree  | 1.0737 ns | 0.0779 ns |         - |
*/
