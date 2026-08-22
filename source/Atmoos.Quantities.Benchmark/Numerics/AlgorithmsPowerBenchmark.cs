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

BenchmarkDotNet v0.15.8, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.111
  [Host]     : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.11 (10.0.11, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 13.938 ns | 0.0275 ns |  1.00 |
| AlgorithmsPow | 2        |  1.388 ns | 0.0141 ns |  0.10 |
|               |          |           |           |       |
| MathPow       | 5        | 14.252 ns | 0.0120 ns |  1.00 |
| AlgorithmsPow | 5        |  3.855 ns | 0.0047 ns |  0.27 |
|               |          |           |           |       |
| MathPow       | 12       | 14.309 ns | 0.0310 ns |  1.00 |
| AlgorithmsPow | 12       |  5.837 ns | 0.1046 ns |  0.41 |
|               |          |           |           |       |
| MathPow       | 23       | 14.248 ns | 0.0178 ns |  1.00 |
| AlgorithmsPow | 23       |  8.156 ns | 0.0148 ns |  0.57 |
*/
