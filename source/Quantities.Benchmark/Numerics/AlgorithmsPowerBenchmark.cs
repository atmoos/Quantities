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

/* Summary *

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-1260P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 13.287 ns | 0.1363 ns |  1.00 |
| AlgorithmsPow | 2        |  1.816 ns | 0.0113 ns |  0.14 |
|               |          |           |           |       |
| MathPow       | 5        | 13.292 ns | 0.0695 ns |  1.00 |
| AlgorithmsPow | 5        |  4.139 ns | 0.0549 ns |  0.31 |
|               |          |           |           |       |
| MathPow       | 12       | 13.633 ns | 0.1344 ns |  1.00 |
| AlgorithmsPow | 12       |  5.899 ns | 0.0895 ns |  0.43 |
|               |          |           |           |       |
| MathPow       | 23       | 13.591 ns | 0.0869 ns |  1.00 |
| AlgorithmsPow | 23       |  8.056 ns | 0.0537 ns |  0.59 |
*/
