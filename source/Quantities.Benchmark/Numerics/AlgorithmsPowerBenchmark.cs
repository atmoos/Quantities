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

BenchmarkDotNet v0.13.8, Arch Linux
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.111
  [Host]     : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.11 (7.0.1123.46301), X64 RyuJIT AVX2


| Method        | Exponent | Mean      | Error     | StdDev    | Ratio |
|-------------- |--------- |----------:|----------:|----------:|------:|
| MathPow       | 2        | 14.347 ns | 0.1007 ns | 0.0893 ns |  1.00 |
| AlgorithmsPow | 2        |  3.102 ns | 0.0156 ns | 0.0131 ns |  0.22 |
|               |          |           |           |           |       |
| MathPow       | 5        | 15.047 ns | 0.0715 ns | 0.0634 ns |  1.00 |
| AlgorithmsPow | 5        |  5.501 ns | 0.0142 ns | 0.0133 ns |  0.37 |
|               |          |           |           |           |       |
| MathPow       | 12       | 15.132 ns | 0.0691 ns | 0.0577 ns |  1.00 |
| AlgorithmsPow | 12       |  7.231 ns | 0.0364 ns | 0.0323 ns |  0.48 |
|               |          |           |           |           |       |
| MathPow       | 23       | 15.140 ns | 0.1057 ns | 0.0826 ns |  1.00 |
| AlgorithmsPow | 23       |  9.933 ns | 0.0332 ns | 0.0294 ns |  0.66 |
*/
