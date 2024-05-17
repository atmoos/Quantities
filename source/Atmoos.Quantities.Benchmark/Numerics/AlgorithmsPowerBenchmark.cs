using Quantities.Core.Numerics;

namespace Quantities.Benchmark.Numerics;

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

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.103
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 14.588 ns | 0.0776 ns |  1.00 |
| AlgorithmsPow | 2        |  3.249 ns | 0.0208 ns |  0.22 |
|               |          |           |           |       |
| MathPow       | 5        | 15.163 ns | 0.0746 ns |  1.00 |
| AlgorithmsPow | 5        |  5.688 ns | 0.0322 ns |  0.38 |
|               |          |           |           |       |
| MathPow       | 12       | 14.850 ns | 0.0297 ns |  1.00 |
| AlgorithmsPow | 12       |  9.526 ns | 0.0305 ns |  0.64 |
|               |          |           |           |       |
| MathPow       | 23       | 14.843 ns | 0.0284 ns |  1.00 |
| AlgorithmsPow | 23       |  9.376 ns | 0.0575 ns |  0.63 |
*/
