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

BenchmarkDotNet v0.15.3, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Max: 0.40GHz) (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.110
  [Host]     : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 28.081 ns | 0.1494 ns |  1.00 |
| AlgorithmsPow | 2        |  9.936 ns | 0.0671 ns |  0.35 |
|               |          |           |           |       |
| MathPow       | 5        | 28.069 ns | 0.1399 ns |  1.00 |
| AlgorithmsPow | 5        | 28.775 ns | 0.1818 ns |  1.03 |
|               |          |           |           |       |
| MathPow       | 12       | 28.129 ns | 0.1644 ns |  1.00 |
| AlgorithmsPow | 12       | 39.712 ns | 0.1012 ns |  1.41 |
|               |          |           |           |       |
| MathPow       | 23       | 28.031 ns | 0.1168 ns |  1.00 |
| AlgorithmsPow | 23       | 48.921 ns | 0.1507 ns |  1.75 |
*/
