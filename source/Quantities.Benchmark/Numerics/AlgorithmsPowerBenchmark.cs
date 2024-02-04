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

BenchmarkDotNet v0.13.12, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | Ratio |
|-------------- |--------- |----------:|----------:|------:|
| MathPow       | 2        | 14.779 ns | 0.0418 ns |  1.00 |
| AlgorithmsPow | 2        |  2.997 ns | 0.0113 ns |  0.20 |
|               |          |           |           |       |
| MathPow       | 5        | 15.207 ns | 0.0110 ns |  1.00 |
| AlgorithmsPow | 5        |  5.935 ns | 0.0178 ns |  0.39 |
|               |          |           |           |       |
| MathPow       | 12       | 15.029 ns | 0.0273 ns |  1.00 |
| AlgorithmsPow | 12       |  9.869 ns | 0.0387 ns |  0.66 |
|               |          |           |           |       |
| MathPow       | 23       | 14.696 ns | 0.0329 ns |  1.00 |
| AlgorithmsPow | 23       |  8.850 ns | 0.0203 ns |  0.60 |
*/
