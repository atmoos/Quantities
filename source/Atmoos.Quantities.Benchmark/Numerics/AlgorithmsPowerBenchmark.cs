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

BenchmarkDotNet v0.15.7, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 13.207 ns | 0.0644 ns |  1.00 |
| AlgorithmsPow | 2        |  1.453 ns | 0.0045 ns |  0.11 |
|               |          |           |           |       |
| MathPow       | 5        | 12.918 ns | 0.0392 ns |  1.00 |
| AlgorithmsPow | 5        |  3.620 ns | 0.0082 ns |  0.28 |
|               |          |           |           |       |
| MathPow       | 12       | 13.348 ns | 0.0676 ns |  1.00 |
| AlgorithmsPow | 12       |  5.981 ns | 0.0341 ns |  0.45 |
|               |          |           |           |       |
| MathPow       | 23       | 13.007 ns | 0.0636 ns |  1.00 |
| AlgorithmsPow | 23       |  7.977 ns | 0.0587 ns |  0.61 |
*/
