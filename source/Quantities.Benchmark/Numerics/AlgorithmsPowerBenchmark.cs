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

BenchmarkDotNet v0.13.10, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 15.134 ns | 0.1243 ns |  1.00 |
| AlgorithmsPow | 2        |  2.780 ns | 0.0170 ns |  0.18 |
|               |          |           |           |       |
| MathPow       | 5        | 15.162 ns | 0.0672 ns |  1.00 |
| AlgorithmsPow | 5        |  5.932 ns | 0.0447 ns |  0.39 |
|               |          |           |           |       |
| MathPow       | 12       | 15.056 ns | 0.0649 ns |  1.00 |
| AlgorithmsPow | 12       |  8.860 ns | 0.1715 ns |  0.59 |
|               |          |           |           |       |
| MathPow       | 23       | 15.188 ns | 0.0829 ns |  1.00 |
| AlgorithmsPow | 23       |  9.094 ns | 0.0323 ns |  0.60 |
*/
