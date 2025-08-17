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

BenchmarkDotNet v0.15.2, Linux Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.109
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 28.251 ns | 0.1409 ns |  1.00 |
| AlgorithmsPow | 2        |  9.993 ns | 0.0517 ns |  0.35 |
|               |          |           |           |       |
| MathPow       | 5        | 28.252 ns | 0.1807 ns |  1.00 |
| AlgorithmsPow | 5        | 28.760 ns | 0.0974 ns |  1.02 |
|               |          |           |           |       |
| MathPow       | 12       | 28.206 ns | 0.1490 ns |  1.00 |
| AlgorithmsPow | 12       | 39.887 ns | 0.1688 ns |  1.41 |
|               |          |           |           |       |
| MathPow       | 23       | 28.200 ns | 0.2059 ns |  1.00 |
| AlgorithmsPow | 23       | 49.235 ns | 0.2083 ns |  1.75 |
*/
