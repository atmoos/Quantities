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
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 42.42.42.42424), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 13.552 ns | 0.0270 ns |  1.00 |
| AlgorithmsPow | 2        |  1.373 ns | 0.0023 ns |  0.10 |
|               |          |           |           |       |
| MathPow       | 5        | 14.038 ns | 0.0867 ns |  1.00 |
| AlgorithmsPow | 5        |  3.978 ns | 0.0154 ns |  0.28 |
|               |          |           |           |       |
| MathPow       | 12       | 14.221 ns | 0.0795 ns |  1.00 |
| AlgorithmsPow | 12       |  5.660 ns | 0.0327 ns |  0.40 |
|               |          |           |           |       |
| MathPow       | 23       | 13.624 ns | 0.0457 ns |  1.00 |
| AlgorithmsPow | 23       |  7.540 ns | 0.0180 ns |  0.55 |
*/
