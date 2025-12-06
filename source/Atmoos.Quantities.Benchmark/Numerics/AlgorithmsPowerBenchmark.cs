using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Benchmark.Numerics;

public class AlgorithmsPowerBenchmark
{
    private const Double value = Math.Tau;

    [Params(2, 5, 12, 23)]
    public Int32 Exponent { get; set; }

    [Benchmark(Baseline = true)]
    public Double MathPow() => Math.Pow(value, Exponent);

    [Benchmark]
    public Double AlgorithmsPow() => Algorithms.Pow(value, Exponent);
}

/* Summary

BenchmarkDotNet v0.15.4, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean      | Error     | Ratio | 
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 13.877 ns | 0.0245 ns |  1.00 | 
| AlgorithmsPow | 2        |  1.437 ns | 0.0078 ns |  0.10 | 
|               |          |           |           |       | 
| MathPow       | 5        | 14.090 ns | 0.2682 ns |  1.00 | 
| AlgorithmsPow | 5        |  4.368 ns | 0.0118 ns |  0.31 | 
|               |          |           |           |       | 
| MathPow       | 12       | 14.043 ns | 0.0487 ns |  1.00 | 
| AlgorithmsPow | 12       |  6.258 ns | 0.0316 ns |  0.45 | 
|               |          |           |           |       | 
| MathPow       | 23       | 14.070 ns | 0.0552 ns |  1.00 | 
| AlgorithmsPow | 23       |  8.072 ns | 0.0547 ns |  0.57 | 
*/
